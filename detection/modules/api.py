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
    def __init__(self, hub_url: str):
        # Full WebSocket URL with query params, e.g.: "wss://server/hubs/trafficHub?intersectionId=4"
        self.hub_url = hub_url.rstrip('/')
        self.ws = None
        self.connected = False

    def connect(self):
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
        while self.connected:
            try:
                msg = self.ws.recv()
                print("<- SignalR:", msg.rstrip(RS))
            except websocket.WebSocketException:
                self.connected = False

    def send(self, method: str, args: list):
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
        return self.connected


def init_signalr(base_signalr_url: str, intersection_id: str):
    """
    Initialize and start the SignalR WebSocket connection.
    :param base_signalr_url: Base WS(S) URL of your SignalR hub, e.g. "wss://server/hubs"
    :param intersection_id: Intersection identifier to append as query parameter.
    """
    global signalr_client

    full_url = f"{base_signalr_url}/trafficHub?intersectionId={intersection_id}"

    signalr_client = SignalRClient(full_url)
    signalr_client.connect()


def notify_car_detected(base_url: str, traffic_light_id: int, cars_number: int):
    """
    Notify the server about the number of cars detected.
    Uses SignalR WebSocket if connected, otherwise falls back to HTTP.

    :param base_url: Base URL of the HTTP endpoint (http(s)://server)
    :param traffic_light_id: Identifier of the traffic light
    :param cars_number: Number of cars detected
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
