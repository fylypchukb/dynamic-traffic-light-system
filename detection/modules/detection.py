import cv2


def run_detection(model, frame, confidence):
    """
    Run YOLO detection on a frame.

    Args:
        model (YOLO): YOLO model object.
        frame (numpy.ndarray): Frame to run detection on.
        confidence (float): Confidence threshold for detection.

    Returns:
        list: Detection results.
    """
    results = model(frame)
    detections = []
    for result in results:
        for detection in result.boxes:
            if detection.cls == 0 and detection.conf > confidence:
                detections.append(detection)
                print(f"Car detected with confidence: {detection.conf}")
    return results


def display_frame(results):
    """
    Display the frame with detection results.

    Args:
        results (list): Detection results to display.
    """
    annotated_frame = results[0].plot()
    cv2.imshow("YOLO Inference", annotated_frame)
