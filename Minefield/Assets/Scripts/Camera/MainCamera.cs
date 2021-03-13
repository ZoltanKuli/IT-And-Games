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

    public void Start() {
        mainCamera = GetComponent<Camera>();
    }

    public void MoveCamera(Vector3 cameraMovementOffset) {
        cameraMovementOffset = Quaternion.Euler(mainCamera.transform.rotation.x,
            mainCamera.transform.rotation.y, mainCamera.transform.rotation.z)
            * cameraMovementOffset;

        Vector3 originalCameraPosition = mainCamera.transform.position;
        Vector3 newCameraPosition = originalCameraPosition + cameraMovementOffset;

        if ((newCameraPosition.x < lowerXBound
            || newCameraPosition.z < lowerZBound)
            || (upperXBound < newCameraPosition.x
            || upperZBound < newCameraPosition.z)) {
            newCameraPosition = originalCameraPosition - cameraMovementOffset;
        }

        mainCamera.transform.position = Vector3.Lerp(originalCameraPosition, newCameraPosition, movementSpeed * Time.deltaTime);
    }

    public void ChaneCameraZoom(Vector3 cameraZoomOffset, Vector3 cameraRotationOffset) {
        cameraZoomOffset = new Vector3(cameraZoomOffset.x, cameraZoomOffset.y * cameraZoomAmountY, cameraZoomOffset.z * cameraZoomAmountZ);
        cameraRotationOffset = new Vector3(cameraRotationOffset.x * cameraRotationAmountX, cameraRotationOffset.y, cameraRotationOffset.z);
        cameraZoomOffset = Quaternion.Euler(mainCamera.transform.rotation.x,
            mainCamera.transform.rotation.y, mainCamera.transform.rotation.z)
            * cameraZoomOffset;

        Vector3 originalCameraZoom = mainCamera.transform.position;

        Vector3 newCameraZoom = originalCameraZoom + cameraZoomOffset;

        Vector3 originalCameraRotation = mainCamera.transform.eulerAngles;
        Vector3 newCameraRotation = originalCameraRotation + cameraRotationOffset;

        if ((newCameraZoom.x < lowerXBound
            || newCameraZoom.z < lowerZBound)
            || (upperXBound < newCameraZoom.x
            || upperZBound < newCameraZoom.z)) {
            newCameraZoom = new Vector3(newCameraZoom.x, newCameraZoom.y, originalCameraZoom.z);
        }

        if (lowerZoomBound < newCameraZoom.y && newCameraZoom.y < upperZoomBound) {
            mainCamera.transform.position = Vector3.Lerp(originalCameraZoom, newCameraZoom, zoomSpeed * Time.deltaTime);
            mainCamera.transform.eulerAngles = Vector3.Lerp(originalCameraRotation, newCameraRotation, zoomSpeed * Time.deltaTime);
        }
    }
}
