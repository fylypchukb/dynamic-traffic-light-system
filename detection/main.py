import cv2
import time
from ultralytics import YOLO
from modules import initialize_camera, load_config, capture_frame, run_detection, display_frame


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
                    results = run_detection(model, frame, detection_confidence)

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
