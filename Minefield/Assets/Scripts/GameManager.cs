using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private MainCamera mainCamera;

    [SerializeField]
    private UIManager uIManager;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private WorldManager worldManager;

    private void Start() {

        uIManager.AssignMethodToOnBuildRoadAction(BuildNewRoad);
        uIManager.AssignMethodToOnBuildStructureAction(BuildNewStructure);
        uIManager.AssignMethodToOnDestroyAction(Destroy);
    }

    private void BuildNewRoad() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewRoad);
    }

    private void BuildNewStructure() {
        System.Random random = new System.Random();

        Action<Vector3Int> method = null;
        switch (random.Next(0, 9)) {
            case 0:
                method = worldManager.BuildNewHotdogCar;
                break;
            case 1:
                method = worldManager.BuildNewKFC;
                break;
            case 2:
                method = worldManager.BuildNewCafeRestaurant;
                break;
            case 3:
                method = worldManager.BuildNewLondonEye;
                break;
            case 4:
                method = worldManager.BuildNewMerryGoRound;
                break;
            case 5:
                method = worldManager.BuildNewRollerCoaster;
                break;
            case 6:
                method = worldManager.BuildNewBasicPark;
                break;
            case 7:
                method = worldManager.BuildNewFountainPark;
                break;
            case 8:
                method = worldManager.BuildNewHelicopterPark;
                break;
        }

        ResetMouseActionsAndAssignMethodToOnMouseClickAction(method);
    }

    private void Destroy() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.Destroy);
    }

    private void ResetMouseActionsAndAssignMethodToOnMouseClickAction(Action<Vector3Int> action) {
        inputManager.ResetMouseActions();
        inputManager.AssignMethodToOnMouseClickAction(action);
    }

    private void Update() {
        mainCamera.MoveCamera(inputManager.GetCameraMovementOffset());
        mainCamera.ChaneCameraZoom(inputManager.GetCameraZoomOffset(), inputManager.GetCameraRotationOffset());
    }
}
