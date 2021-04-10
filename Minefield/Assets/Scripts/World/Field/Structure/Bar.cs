using System;
using UnityEngine;

public class Bar : Structure {

    private int decreaseThirstAmount;

    public Bar(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength,
        int moneyOwedIncreaseAmount, int satisfactionIncreaseAmount, int secondsBetweenActions, int decreaseThirstAmount, 
        int maxQueueLength, WorldManager worldManager)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength,
            moneyOwedIncreaseAmount, satisfactionIncreaseAmount, secondsBetweenActions,
            maxQueueLength, worldManager) {
        this.decreaseThirstAmount = decreaseThirstAmount;
    }

    /// <summary>
    /// Remove npc inside.
    /// </summary>
    protected override void RemoveNPCInside() {
        if (npcInside != null) {
            npcInside.SetIsBusy(false);
            npcInside.SetIsVisible(true);

            npcInside.IncreaseOrDecreaseSatisfaction(satisfactionIncreaseAmount);
            npcInside.DecreaseThirst(decreaseThirstAmount);
            npcInside.IncreaseOrDecreaseMoneyOwed(moneyOwedIncreaseAmount);
            npcInside.AddGarbage();

            npcInside = null;
        }
    }
}
