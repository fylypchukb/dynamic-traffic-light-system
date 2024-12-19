import cv2
import time
from ultralytics import YOLO

# Load the YOLO model
model = YOLO("yolo11s.pt")

# Open the camera feed (0 for the default camera)
cap = cv2.VideoCapture(0)

# Initialize the timer
last_frame_time = 0

frame_interval = 1  # One frame per second
show_frame = False

# Loop through the video frames
while cap.isOpened():
    # Get the current time
    current_time = time.time()

    # Read a frame from the camera if enough time has passed
    if current_time - last_frame_time >= frame_interval:
        success, frame = cap.read()
        last_frame_time = current_time

        if success:
            # Run YOLO inference on the frame
            results = model(frame)

            # Check if any cars are detected with confidence > 70%
            for result in results:
                for detection in result.boxes:
                    if detection.cls == 0 and detection.conf > 0.7:  # Class ID for 'car'
                        print("Car detected with confidence:", detection.conf)

            # Display the frame with detections if enabled
            if show_frame:
                annotated_frame = results[0].plot()
                cv2.imshow("YOLO Inference", annotated_frame)

            # Check for key press regardless of display status
            if cv2.waitKey(1) & 0xFF == ord("q"):
                break
        else:
            # Break the loop if the end of the video is reached
            break

# Ensure proper release and closure even if window is not shown
cap.release()
if show_frame:
    cv2.destroyAllWindows()
