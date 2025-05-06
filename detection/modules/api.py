import json
import uuid
from datetime import datetime, timezone

import requests
import websocket

import ssl
from websocket import create_connection

# SignalR record separator
RS = "\x1e"

# Global client instance
signalr_client = None


class SignalRClient:
    """
    A client wrapper for SignalR WebSocket connections.

    Handles connection setup, message sending, and background listening
    for incoming messages from the SignalR hub.
    """

    def __init__(self, hub_url: str):
        self.hub_url = hub_url.rstrip('/')
        self.ws = None
        self.connected = False

    def connect(self):
        """
        Establish a WebSocket connection to the SignalR hub and send the initial handshake.

        Also starts a background thread to listen for incoming messages.
        """
        self.ws = create_connection(
            self.hub_url,
            sslopt={
                "cert_reqs": ssl.CERT_NONE,
                "check_hostname": False
            }
        )
        self.connected = True

        handshake = json.dumps({"protocol": "json", "version": 1}) + RS
        self.ws.send(handshake)
        self.ws.recv()  # ack

        # Start listener thread
        import threading
        threading.Thread(target=self._listen, daemon=True).start()

    def _listen(self):
        """
        Internal method that continuously listens for messages from the server.

        Runs in a separate thread. If a connection error occurs, sets `connected` to False.
        """
        while self.connected:
            try:
                msg = self.ws.recv()
                print("<- SignalR:", msg.rstrip(RS))
            except websocket.WebSocketException:
                self.connected = False

    def send(self, method: str, args: list):
        """
        Send a message to the SignalR hub with the given method name and arguments.

        :param method: The method name (target) defined on the SignalR hub.
        :param args: List of arguments to send with the invocation.
        :raises RuntimeError: If the WebSocket is not connected.
        """
        if not self.connected:
            raise RuntimeError("SignalR socket is not connected")
        invocation = {
            "type": 1,
            "target": method,
            "arguments": args
        }
        message = json.dumps(invocation, separators=(",", ":")) + RS
        self.ws.send(message.encode("utf-8"))

    def is_connected(self) -> bool:
        """
        Check if the SignalR WebSocket connection is currently active.

        :return: True if connected, False otherwise.
        """
        return self.connected


def init_signalr(base_signalr_url: str, intersection_id: str):
    """
    Initialize and start the SignalR WebSocket connection.

    :param base_signalr_url: Base WebSocket URL of the SignalR hub, e.g., "wss://server/hubs".
    :param intersection_id: Unique identifier of the intersection to include as a query parameter.
    """
    global signalr_client

    full_url = f"{base_signalr_url}/trafficHub?intersectionId={intersection_id}"

    signalr_client = SignalRClient(full_url)
    signalr_client.connect()


def notify_car_detected(base_url: str, traffic_light_id: int, cars_number: int):
    """
    Notify the server of the number of detected cars at a traffic light.

    Sends data via SignalR WebSocket if connected, otherwise falls back to HTTP POST.

    :param base_url: Base URL of the server, used for HTTP fallback.
    :param traffic_light_id: ID of the traffic light where detection occurred.
    :param cars_number: Number of cars detected at that traffic light.
    """
    correlation_id = str(uuid.uuid4())
    detection_time = datetime.now(timezone.utc).isoformat()
    payload = {
        "trafficLightId": traffic_light_id,
        "carsNumber": cars_number,
        "correlationId": correlation_id,
        "detectionTime": detection_time
    }

    # Attempt SignalR WebSocket
    if signalr_client and signalr_client.is_connected():
        try:
            signalr_client.send("SendTrafficFlowUpdate", [payload])
            print(f"[{detection_time}] Sent via SignalR. CorrelationId: {correlation_id}")
            return
        except Exception as ex:
            print(f"[{detection_time}] SignalR send failed: {ex}, falling back to HTTP")

    # Fallback to HTTP
    try:
        resp = requests.post(
            f"{base_url}/trafficFlow/calculate-green-light",
            json=payload,
            verify=False
        )
        resp.raise_for_status()
        print(f"[{detection_time}] Sent via HTTP fallback. CorrelationId: {correlation_id}")
    except requests.RequestException as e:
        print(f"[{detection_time}] HTTP notification failed: {e}")
