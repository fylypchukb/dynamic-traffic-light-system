import cv2


def initialize_camera(camera_source):
    """
    Initialize the camera.

    Args:
        camera_source (int): Index of the camera to use.

    Returns:
        cv2.VideoCapture: Video capture object.
    """
    cap = cv2.VideoCapture(camera_source)
    if not cap.isOpened():
        raise Exception(f"Failed to open camera with source {camera_source}")
    return cap


def capture_frame(cap):
    """
    Capture a frame from the camera.

    Args:
        cap (cv2.VideoCapture): Video capture object.

    Returns:
        numpy.ndarray: Captured frame.
    """
    success, frame = cap.read()
    if not success:
        print("Failed to capture frame")
        return None
    return frame
