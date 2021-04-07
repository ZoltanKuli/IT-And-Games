using System;
using UnityEngine;

public class Bar : Structure {

    private int decreaseThirstAmount;

    public Bar(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength,
        float moneyOwedIncreaseAmount, int satisfactionIncreaseAmount, int secondsBetweenActions, int decreaseThirstAmount)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength,
            moneyOwedIncreaseAmount, satisfactionIncreaseAmount, secondsBetweenActions) {
        this.decreaseThirstAmount = decreaseThirstAmount;
    }

    public new void Action(NPC npc) {
        if (!queu.Contains(npc)) {
            npc.IncreaseOrDecreaseSatisfaction(-queu.Count);
            queu.Enqueue(npc);
        }

        if (lastActionTime.AddSeconds(secondsBetweenActions) <= DateTime.Now) {
            if (npcInside != null) {
                npcInside.SetIsBusy(false);
                npcInside.SetIsVisible(true);
                npcInside.IncreaseOrDecreaseSatisfaction(satisfactionIncreaseAmount);
                npcInside.DecreaseThirst(decreaseThirstAmount);
                npcInside.IncreaseOrDecreaseMoneyOwed(moneyOwedIncreaseAmount);
            }

            npcInside = null;
            if (0 < queu.Count) {
                npcInside = queu.Dequeue();
            }

            lastActionTime = DateTime.Now;
        }
    }
}
