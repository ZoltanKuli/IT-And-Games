using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private WorldManager worldManager;

    private void Update() {
        cameraMovement.MoveCamera(inputManager.GetCameraMovementVector());
    }

    private void Start() {
        inputManager.AssignMethodToOnMouseClick(worldManager.PlaceDownARoad);
    }
}
