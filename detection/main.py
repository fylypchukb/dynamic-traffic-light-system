﻿import cv2
import time
from ultralytics import YOLO
from modules import initialize_camera, load_config, capture_frame, run_detection, display_frame, notify_car_detected


def main():
    """
        Main function to load configuration, initialize camera, capture frames,
        run YOLO detection, and display frames if enabled.
    """
    # Load configuration
    config = load_config("config.ini")

    # Extract configuration parameters
    camera_source = int(config.get("Settings", "camera_source", fallback=0))
    frame_interval = float(config.get("Settings", "frame_interval", fallback=1))
    show_frame = config.getboolean("Settings", "show_frame", fallback=False)
    detection_confidence = float(config.get("Settings", "detection_confidence", fallback=0.7))
    traffic_light_id = config.get("Settings", "traffic_light_id")
    server_base_url = config.get("Connection", "server_base_url")

    if not traffic_light_id or not server_base_url:
        raise ValueError("Traffic light ID and server base URL must be provided in the configuration.")

    # Load the YOLO model
    model = YOLO(config.get("Settings", "model_path", fallback="yolo11s.pt"))

    # Initialize camera
    cap = initialize_camera(camera_source)

    last_frame_time = 0

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

                    if detections:
                        notify_car_detected(server_base_url, traffic_light_id, len(detections))

                    # Display frame if enabled
                    if show_frame:
                        display_frame(results)

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
