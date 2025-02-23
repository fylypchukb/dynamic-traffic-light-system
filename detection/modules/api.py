import requests


def notify_car_detected(base_url, traffic_light_id, cars_number):
    """
    Notify the server about the number of cars detected.
    :param base_url: Base URL of the server.
    :param traffic_light_id: Identifier of the traffic light.
    :param cars_number: Number of cars detected.
    :return: None
    """

    url = f"{base_url}/calculate-green-light"
    payload = {
        "trafficLightId": traffic_light_id,
        "carsNumber": cars_number
    }
    try:
        response = requests.post(url, json=payload)
        response.raise_for_status()
        print("Notification sent successfully.")
    except requests.RequestException as e:
        print(f"Failed to send notification: {e}")
