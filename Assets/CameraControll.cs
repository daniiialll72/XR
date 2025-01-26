using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 3.0f; // Speed of camera rotation
    public float zoomSpeed = 10.0f; // Speed of camera zoom
    public float minZoom = 10f; // Minimum zoom distance
    public float maxZoom = 100f; // Maximum zoom distance

    private float currentZoom = 50f; // Initial zoom distance

    void Update()
    {
        // Camera rotation based on mouse movement
        float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed;
        float verticalInput = -Input.GetAxis("Mouse Y") * rotationSpeed;

        // Rotate the camera
        transform.Rotate(Vector3.up, horizontalInput, Space.World);
        transform.Rotate(Vector3.right, verticalInput, Space.Self);

        // Camera zooming with the mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scrollInput * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); // Clamping the zoom between min and max

        // Adjust the camera's position based on the zoom level
        Camera.main.fieldOfView = currentZoom;
    }
}
