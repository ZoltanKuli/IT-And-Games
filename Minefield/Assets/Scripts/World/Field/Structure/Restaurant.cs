using System;
using UnityEngine;

public class Restaurant : Structure {

    private int decreaseHungerAmount;

    public Restaurant(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength,
        int moneyOwedIncreaseAmount, int satisfactionIncreaseAmount, int secondsBetweenActions, int decreaseHungerAmount, 
        int maxQueueLength, WorldManager worldManager, int minimumNotBreakingSeconds, int maximumNotBreakingSeconds,
        int satisfactionDecreaseAmount)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength,
            moneyOwedIncreaseAmount, satisfactionIncreaseAmount, secondsBetweenActions,
            maxQueueLength, worldManager, minimumNotBreakingSeconds, maximumNotBreakingSeconds, 
            satisfactionDecreaseAmount) {
        this.decreaseHungerAmount = decreaseHungerAmount;
    }

    /// <summary>
    /// Remove npc inside.
    /// </summary>
    protected override void RemoveNPCInside() {
        if (npcInside != null) {
            npcInside.SetIsBusy(false);
            npcInside.SetIsVisible(true);

            npcInside.IncreaseOrDecreaseSatisfaction(satisfactionIncreaseAmount);
            npcInside.DecreaseHunger(decreaseHungerAmount);
            npcInside.IncreaseOrDecreaseMoneyOwed(moneyOwedIncreaseAmount);
            npcInside.AddGarbage();

            npcInside = null;
        }
    }
}
