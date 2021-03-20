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
        uIManager.AssignMethodToOnDestroyAction(Destroy);

        uIManager.AssingMethodToOnCafeBuildAction(BuildNewCafe);
        uIManager.AssingMethodToOnCafeRestaurantBuildAction(BuildNewCafeRestaurant);
    }

    private void BuildNewCafe()
    {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewCafe);
    }

    private void BuildNewCafeRestaurant()
    {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewCafeRestaurant);
    }

    private void BuildNewRoad() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewRoad);
    }

    private void BuildNewStructure() {
        System.Random random = new System.Random();

        Action<Vector3Int> method = null;
        switch (random.Next(0, 13)) {
            case 0:
                method = worldManager.BuildNewHotdogCar;
                break;
            case 1:
                method = worldManager.BuildNewKFC;
                break;
            case 2:
                method = worldManager.BuildNewOlivegardensRestaurant;
                break;
            case 3:
                method = worldManager.BuildNewTaverneRestaurant;
                break;
            case 4:
                method = worldManager.BuildNewCafeRestaurant;
                break;
            case 5:
                method = worldManager.BuildNewCafe;
                break;
            case 6:
                method = worldManager.BuildNewLondonEye;
                break;
            case 7:
                method = worldManager.BuildNewMerryGoRound;
                break;
            case 8:
                method = worldManager.BuildNewRollerCoaster;
                break;
            case 9:
                method = worldManager.BuildNewCircusTent;
                break;
            case 10:
                method = worldManager.BuildNewBasicPark;
                break;
            case 11:
                method = worldManager.BuildNewFountainPark;
                break;
            case 12:
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
