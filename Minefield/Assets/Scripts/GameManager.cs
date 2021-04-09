using System;
using System.Collections.Generic;
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

    [SerializeField]
    private int playersBalance;

    private Entrance entrance;

    [SerializeField]
    private int entranceCost;

    private List<NPC> npcs;
    private float averageNPCSatisfaction;
    private float averageNPCThirst;
    private float averageNPCHunger;
    [SerializeField]
    private float minimumAverageNPCSatisfactionOfNPCsSpawn;
    private int maximumNPCNumber;
    [SerializeField]
    private GameObject stevePrefab;
    [SerializeField]
    private float npcDistancePrecision;
    [SerializeField]
    private int npcDefaultSatisfactionMinimum;
    [SerializeField]
    private int npcDefaultSatisfactionMaximum;
    [SerializeField]
    private int npcMinimumSatisfactionOfStayingMinimum;
    [SerializeField]
    private int npcMinimumSatisfactionOfStayingMaximum;
    [SerializeField]
    private int npcDefaultThirstMinimum;
    [SerializeField]
    private int npcDefaultThirstMaximum;
    [SerializeField]
    private int npcMaximumThirstOfNotNeedingToDrinkMinimum;
    [SerializeField]
    private int npcMaximumThirstOfNotNeedingToDrinkMaximum;
    [SerializeField]
    private int npcDefaultHungerMinimum;
    [SerializeField]
    private int npcDefaultHungerMaximum;
    [SerializeField]
    private int npcMaximumHungerOfNotNeedingToEatMinimum;
    [SerializeField]
    private int npcMaximumHungerOfNotNeedingToEatMaximum;
    [SerializeField]
    private float npcMinimumSpeed;
    [SerializeField]
    private float npcMaximumSpeed;
    [SerializeField]
    private float npcRotationSpeedMultiplier;
    [SerializeField]
    private int secondsUntilThirstGrowthMinimum;
    [SerializeField]
    private int secondsUntilThirstGrowthMaximum;
    [SerializeField]
    private int thirstGrowthAmountMinimum;
    [SerializeField]
    private int thirstGrowthAmountMaximum;
    [SerializeField]
    private int secondsUntilHungerGrowthMinimum;
    [SerializeField]
    private int secondsUntilHungerGrowthMaximum;
    [SerializeField]
    private int hungerGrowthAmountMinimum;
    [SerializeField]
    private int hungerGrowthAmountMaximum;
    [SerializeField]
    private float loweringDistanceOnInvisibility;
    [SerializeField]
    private int thirstDecreaseDissatisfactionAmountMinimum;
    [SerializeField]
    private int thirstDecreaseDissatisfactionAmountMaximum;
    [SerializeField]
    private int hungerDecreaseDissatisfactionAmountMinimum;
    [SerializeField]
    private int hungerDecreaseDissatisfactionAmountMaximum;
    [SerializeField]
    private int minimumSecondsUntilGarbageDisposal;
    [SerializeField]
    private int maximumSecondsUntilGarbageDisposal;
    [SerializeField]
    private int garbageDecreaseDissatisfactionAmountMinimum;
    [SerializeField]
    private int garbageDecreaseDissatisfactionAmountMaximum;

    private System.Random random;

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

        uIManager.AssingMethodToOnGarbageCanBuildAction(BuildNewGarbageCan);

        entrance = worldManager.GetEntrance();

        mainCamera.transform.position = new Vector3(entrance.GetOrigoPosition().x,
            mainCamera.transform.position.y, entrance.GetOrigoPosition().z);

        npcs = new List<NPC>();

        averageNPCSatisfaction = 100;
        averageNPCThirst = 100;
        averageNPCHunger = 100;

        maximumNPCNumber = worldManager.GetMaximumNPCNumberBasedOnWorldMatrixSize();

        random = new System.Random();
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
    /// Reset the Mouse click to the BuildNewGarbageCan Action.
    /// </summary>
    private void BuildNewGarbageCan() {
        ResetMouseActionsAndAssignMethodToOnMouseClickAction(worldManager.BuildNewGarbageCan);
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

    /// <summary>
    /// Update.
    /// </summary>
    private void Update() {
        mainCamera.UpdateCameraPosition(inputManager.GetCameraMovementDirection());
        mainCamera.UpdateCameraZoomAndRotation(inputManager.GetCameraZoomDirection(), inputManager.GetCameraRotationDirection());

        SpawnNPC();

        UpdateNPCS();

        UpdateAverageNPCSatisfaction();
        UpdateAverageNPCThirst();
        UpdateAverageNPCHunnger();
        UpdatePlayersBalanceFromNPCSMoneyOwed();

        Debug.Log("NPC Number:" + npcs.Count 
            + "; Average Satisfaction: " + averageNPCSatisfaction
            + "; Player's Balance: " + playersBalance
            + "; Average Thirst: " + averageNPCThirst 
            + "; Average Hunger: " + averageNPCHunger);
    }

    /// <summary>
    /// Update average npc satisfaction.
    /// </summary>
    private void UpdateAverageNPCSatisfaction() {
        averageNPCSatisfaction = 0;

        if (0 < npcs.Count) {
            foreach (NPC npc in npcs) {
                averageNPCSatisfaction += npc.GetSatisfaction();
            }

            averageNPCSatisfaction /= npcs.Count;
        }
    }

    /// <summary>
    /// Update average npc thirst.
    /// </summary>
    private void UpdateAverageNPCThirst() {
        averageNPCThirst = 0;

        if (0 < npcs.Count) {
            foreach (NPC npc in npcs) {
                averageNPCThirst += npc.GetThirst();
            }

            averageNPCThirst /= npcs.Count;
        }
    }

    /// <summary>
    /// Update average npc hunger.
    /// </summary>
    private void UpdateAverageNPCHunnger() {
        averageNPCHunger = 0;

        if (0 < npcs.Count) {
            foreach (NPC npc in npcs) {
                averageNPCHunger += npc.GetHunger();
            }

            averageNPCHunger /= npcs.Count;
        }
    }

    /// <summary>
    /// Update player's balance from npcs money owed.
    /// </summary>
    private void UpdatePlayersBalanceFromNPCSMoneyOwed() {
        foreach (NPC npc in npcs) {
            IncreaseOrDecreasePlayersBalance(npc.GetMoneyOwed());
            npc.IncreaseOrDecreaseMoneyOwed(-npc.GetMoneyOwed());
        }
    }

    /// <summary>
    /// Spawn npc.
    /// </summary>
    private void SpawnNPC() {
        if (minimumAverageNPCSatisfactionOfNPCsSpawn <= averageNPCSatisfaction && npcs.Count < maximumNPCNumber) {
            npcs.Add(new NPC(stevePrefab, entrance.GetOrigoPosition(), worldManager, npcDistancePrecision,
            random.Next(npcDefaultSatisfactionMinimum, npcDefaultSatisfactionMaximum),
            random.Next(npcMinimumSatisfactionOfStayingMinimum, npcMinimumSatisfactionOfStayingMaximum),
            random.Next(npcDefaultThirstMinimum, npcDefaultThirstMaximum),
            random.Next(npcMaximumThirstOfNotNeedingToDrinkMinimum, npcMaximumThirstOfNotNeedingToDrinkMaximum),
            random.Next(npcDefaultHungerMinimum, npcDefaultHungerMaximum),
            random.Next(npcMaximumHungerOfNotNeedingToEatMinimum, npcMaximumHungerOfNotNeedingToEatMaximum), 
            npcMinimumSpeed, npcMaximumSpeed, npcRotationSpeedMultiplier,
            random.Next(secondsUntilThirstGrowthMinimum, secondsUntilThirstGrowthMaximum),
            random.Next(thirstGrowthAmountMinimum, thirstGrowthAmountMaximum),
            random.Next(secondsUntilHungerGrowthMinimum, secondsUntilHungerGrowthMaximum),
            random.Next(hungerGrowthAmountMinimum, hungerGrowthAmountMaximum),
            loweringDistanceOnInvisibility,
            random.Next(thirstDecreaseDissatisfactionAmountMinimum, thirstDecreaseDissatisfactionAmountMaximum),
            random.Next(hungerDecreaseDissatisfactionAmountMinimum, hungerDecreaseDissatisfactionAmountMaximum),
            minimumSecondsUntilGarbageDisposal, maximumSecondsUntilGarbageDisposal,
            random.Next(garbageDecreaseDissatisfactionAmountMinimum, garbageDecreaseDissatisfactionAmountMaximum)));

            IncreaseOrDecreasePlayersBalance(entranceCost);
        }
    }

    /// <summary>
    /// Update npcs.
    /// </summary>
    private void UpdateNPCS() {
        for (int i = 0; i < npcs.Count; i++) {
            NPC npc = npcs[i];
            if (!npc.IsDestroyed()) {
                npc.Update();
            } else {
                npcs.RemoveAt(i);
                i--;
            }
        }
    }

    /// <summary>
    /// Inncrease or decrease player's balance.
    /// </summary>
    public void IncreaseOrDecreasePlayersBalance(int increaseOrDecreaseAmount) {
        playersBalance += increaseOrDecreaseAmount;
    }

    /// <summary>
    /// Get player's balance.
    /// </summary>
    public int GetPlayersBalance() {
        return playersBalance;
    }
}
