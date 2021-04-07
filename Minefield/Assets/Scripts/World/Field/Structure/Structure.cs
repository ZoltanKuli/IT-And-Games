using System;
using System.Collections.Generic;
using UnityEngine;

public class Structure : PopulatedField {

    protected NPC npcInside;
    protected Queue<NPC> queu;

    protected float moneyOwedIncreaseAmount;
    protected int satisfactionIncreaseAmount;
    protected int secondsBetweenActions;
    protected DateTime lastActionTime;

    public Structure(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength,
        float moneyOwedIncreaseAmount, int satisfactionIncreaseAmount, int secondsBetweenActions)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength) {

        this.moneyOwedIncreaseAmount = moneyOwedIncreaseAmount;
        this.satisfactionIncreaseAmount = satisfactionIncreaseAmount;
        this.secondsBetweenActions = secondsBetweenActions;

        queu = new Queue<NPC>();
        lastActionTime = DateTime.Now;
    }

    public void Action(NPC npc) {
        if (!queu.Contains(npc)) {
            npc.IncreaseOrDecreaseSatisfaction(-queu.Count);
            queu.Enqueue(npc);
        }

        if (lastActionTime.AddSeconds(secondsBetweenActions) <= DateTime.Now) {
            if (npcInside != null) {
                npcInside.SetIsBusy(false);
                npcInside.SetIsVisible(true);
                npcInside.IncreaseOrDecreaseSatisfaction(satisfactionIncreaseAmount);
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
