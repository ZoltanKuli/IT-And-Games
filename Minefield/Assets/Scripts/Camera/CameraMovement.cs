using UnityEngine;

public class CameraMovement : MonoBehaviour {
   
    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private float cameraMovementSpeed;

    public void Start() {
        gameCamera = GetComponent<Camera>();
    }

    public void MoveCamera(Vector3 inputVector) {
        var movementVector = Quaternion.Euler(0, 30, 0) * inputVector;
        gameCamera.transform.position += movementVector * Time.deltaTime * cameraMovementSpeed;
    }
}
