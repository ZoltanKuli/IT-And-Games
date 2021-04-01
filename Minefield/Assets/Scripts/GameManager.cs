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
    
    /// <summary>
    /// Fill up the menus with their actions.
    /// </summary>
    private void Start() {
        uIManager.AssignMethodToOnBuildRoadAction(BuildNewRoad);
        uIManager.AssignMethodToOnDestroyAction(Destroy);

        uIManager.AssingMethodToOnCafeBuildAction(BuildNewCafe);
        uIManager.AssingMethodToOnCafeRestaurantBuildAction(BuildNewCafeRestaurant);

        uIManager.AssingMethodToOnHotdogCarBuildAction(BuildNewHotdogCar);
        uIManager.AssingMethodToOnKfcRestaurantBuildAction(BuildNewKfcRestaurant);
        uIManager.AssingMethodToOnOlivegardenRestaurantBuildAction(BuildNewOlivegardensRestaurant);
        uIManager.AssingMethodToOnTaverneRestaurantBuildAction(BuildNewTaverneRestaurant);

        uIManager.AssingMethodToOnCircusTentBuildAction(BuildNewCircusTent);
        uIManager.AssingMethodToOnLondonEyeBuildAction(BuildNewLondonEye);
        uIManager.AssingMethodToOnMerryGoRoundBuildAction(BuildNewMerryGoRound);
        uIManager.AssingMethodToOnRollerCoasterBuildAction(BuildNewRollerCoaster);

        uIManager.AssingMethodToOnParkBasicBuildAction(BuildNewParkBasic);
        uIManager.AssingMethodToOnParkFountainBuildAction(BuildNewParkFountain);
        uIManager.AssingMethodToOnParkHelicopterBuildAction(BuildNewParkHelicopter);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewCafe Action.
    /// </summary>
    private void BuildNewCafe() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewCafe);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewCafeRestaurant Action.
    /// </summary>
    private void BuildNewCafeRestaurant() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewCafeRestaurant);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewHotdogCar Action.
    /// </summary>
    private void BuildNewHotdogCar() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewHotdogCar);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewKFC Action.
    /// </summary>
    private void BuildNewKfcRestaurant() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewKFC);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewOlivegardensRestaurant Action.
    /// </summary>
    private void BuildNewOlivegardensRestaurant() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewOlivegardensRestaurant);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewTaverneRestaurant Action.
    /// </summary>
    private void BuildNewTaverneRestaurant() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewTaverneRestaurant);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewRoad Action.
    /// </summary>
    private void BuildNewRoad() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewRoad);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewCircusTent Action.
    /// </summary>
    private void BuildNewCircusTent() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewCircusTent);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewLondonEye Action.
    /// </summary>
    private void BuildNewLondonEye() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewLondonEye);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewMerryGoRound Action.
    /// </summary>
    private void BuildNewMerryGoRound() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewMerryGoRound);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewRollerCoaster Action.
    /// </summary>
    private void BuildNewRollerCoaster() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewRollerCoaster);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewBasicPark Action.
    /// </summary>
    private void BuildNewParkBasic() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewBasicPark);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewFountainPark Action.
    /// </summary>
    private void BuildNewParkFountain() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewFountainPark);
    }

    /// <summary>
    /// Reset the Mouse click to the BuildNewHelicopterPark Action.
    /// </summary>
    private void BuildNewParkHelicopter() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewHelicopterPark);
    }

    /// <summary>
    /// Reset the Mouse click to the Destroy Action.
    /// </summary>
    private void Destroy() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.Destroy);
    }
 
    /// <summary>
    /// Reset the Mouse click to the Action what is given via the parameter.
    /// </summary>
    private void ResetMouseActionsAndAssignMethodToOnMouseClickAction(Action<Vector3Int> action) {
        inputManager.ResetMouseActions();
        inputManager.AssignMethodToOnMouseClickAction(action);
    }

    private void Update() {
        mainCamera.MoveCamera(inputManager.GetCameraMovementOffset());
        mainCamera.ChangeCameraZoom(inputManager.GetCameraZoomOffset(), inputManager.GetCameraRotationOffset());
    }
}
