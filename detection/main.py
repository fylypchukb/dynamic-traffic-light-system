import cv2
import time
from ultralytics import YOLO
from modules import initialize_camera, load_config, capture_frame, run_detection, display_frame, notify_car_detected, init_signalr


def main():
    """
        Main function to load configuration, initialize camera, capture frames,
        run YOLO detection, and display frames if enabled.
    """
    # Load configuration
    config = load_config("config.ini")

    # Extract configuration parameters
    frame_interval = float(config.get("Settings", "frame_interval", fallback=1))
    show_frame = config.getboolean("Settings", "show_frame", fallback=False)
    detection_confidence = float(config.get("Settings", "detection_confidence", fallback=0.7))
    camera_source = int(config.get("Settings", "camera_source"))
    traffic_light_id = int(config.get("Settings", "traffic_light_id"))
    intersection_id = config.get("Settings", "intersection_id")
    request_interval = float(config.get("Settings", "request_interval", fallback=5))
    server_base_url = config.get("Connection", "server_base_url")
    signalr_url = config.get("Connection", "signalr_url")

    if not traffic_light_id or not server_base_url or camera_source is None:
        raise ValueError("Camera source, traffic light ID and server base URL must be provided in the configuration.")

    # Load the YOLO model
    model = YOLO(config.get("Settings", "model_path", fallback="yolo11s.pt"))

    init_signalr(signalr_url, intersection_id)

    # Initialize camera
    cap = initialize_camera(camera_source)

    last_frame_time = 0
    last_request_time = 0

    try:
        while cap.isOpened():
            current_time = time.time()

            # Capture a frame if enough time has passed
            if current_time - last_frame_time >= frame_interval:
                frame = capture_frame(cap)
                last_frame_time = current_time

                if frame is not None:
                    # Run YOLO detection
                    results, detections = run_detection(model, frame, detection_confidence)

                    # Display frame if enabled
                    if show_frame:
                        display_frame(results)

                    if detections and current_time - last_request_time >= request_interval:
                        notify_car_detected(server_base_url, traffic_light_id, len(detections))
                        last_request_time = current_time

                    # Check for quit command
                    if cv2.waitKey(1) & 0xFF == ord("q"):
                        break
    except Exception as e:
        print(f"An error occurred: {e}")
    finally:
        cap.release()
        if show_frame:
            cv2.destroyAllWindows()


if __name__ == "__main__":
    main()
