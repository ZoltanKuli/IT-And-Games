using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    private MainCamera mainCamera;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private WorldManager worldManager;

    private void Update() {
        mainCamera.MoveCamera(inputManager.GetCameraMovementOffset());
        mainCamera.ChaneCameraZoom(inputManager.GetCameraZoomOffset(), inputManager.GetCameraRotationOffset());
    }

    private void Start() {
        inputManager.AssignMethodToOnMouseClickAction(worldManager.PlaceDownNewRoad);
    }
}
