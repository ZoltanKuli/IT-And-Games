using UnityEngine;

public class MainCamera : MonoBehaviour {
   
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float lowerXBound;
    [SerializeField]
    private float lowerZBound;
    [SerializeField]
    private float upperXBound;
    [SerializeField]
    private float upperZBound;

    [SerializeField]
    private float zoomSpeed;
    [SerializeField]
    private float cameraZoomAmountY;
    [SerializeField]
    private float cameraZoomAmountZ;
    [SerializeField]
    private float cameraRotationAmountX;

    [SerializeField]
    private float lowerZoomBound;
    [SerializeField]
    private float upperZoomBound;
    
    /// <summary>
    /// Initialize mainCamera.
    /// </summary>
    public void Start() {
        mainCamera = GetComponent<Camera>();
    }

    /// <summary>
    /// Update camera position.
    /// </summary>
    public void UpdateCameraPosition(Vector3 cameraMovementDirection) {
        Vector3 cameraMovementOffset = Quaternion.Euler(mainCamera.transform.rotation.x,
            mainCamera.transform.rotation.y, mainCamera.transform.rotation.z)
            * cameraMovementDirection;

        Vector3 originalCameraPosition = mainCamera.transform.position;
        Vector3 newCameraPosition = originalCameraPosition + cameraMovementOffset;

        mainCamera.transform.position = Vector3.Lerp(originalCameraPosition, ClampNewCameraPosition(newCameraPosition), movementSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Clamp new camera position.
    /// </summary>
    private Vector3 ClampNewCameraPosition(Vector3 newCameraPosition) {
        Vector3 clampedCameraPosition = new Vector3(newCameraPosition.x, newCameraPosition.y, newCameraPosition.z);
        
        if (clampedCameraPosition.x <= lowerXBound) {
            clampedCameraPosition.x = lowerXBound;
        }

        if (clampedCameraPosition.z <= lowerZBound) {
            clampedCameraPosition.z = lowerZBound;
        }

        if (upperXBound <= clampedCameraPosition.x) {
            clampedCameraPosition.x = upperXBound;
        }

        if (upperZBound <= clampedCameraPosition.z) {
            clampedCameraPosition.z = upperZBound;
        }

        return clampedCameraPosition;
    }

    /// <summary>
    /// Update camera zoom and rotation.
    /// </summary>
    public void UpdateCameraZoomAndRotation(Vector3 cameraZoomDirection, Vector3 cameraRotationDirection) {
        Vector3 cameraZoomOffset = new Vector3(cameraZoomDirection.x, cameraZoomDirection.y * cameraZoomAmountY, cameraZoomDirection.z * cameraZoomAmountZ);
        cameraZoomOffset = Quaternion.Euler(mainCamera.transform.rotation.x,
            mainCamera.transform.rotation.y, mainCamera.transform.rotation.z)
            * cameraZoomOffset;
        Vector3 cameraRotationOffset = new Vector3(cameraRotationDirection.x * cameraRotationAmountX, cameraRotationDirection.y, cameraRotationDirection.z);

        Vector3 originalCameraZoom = mainCamera.transform.position;
        Vector3 newCameraZoom = originalCameraZoom + cameraZoomOffset;

        Vector3 originalCameraRotation = mainCamera.transform.eulerAngles;
        Vector3 newCameraRotation = originalCameraRotation + cameraRotationOffset;

        if (lowerZoomBound < newCameraZoom.y && newCameraZoom.y < upperZoomBound) {
            mainCamera.transform.position = Vector3.Lerp(originalCameraZoom, newCameraZoom, zoomSpeed * Time.deltaTime);
            mainCamera.transform.eulerAngles = Vector3.Lerp(originalCameraRotation, newCameraRotation, zoomSpeed * Time.deltaTime);
        }
    }
}
