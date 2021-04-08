using System;
using System.Collections.Generic;
using UnityEngine;

public class Structure : PopulatedField {

    protected NPC npcInside;
    protected Queue<NPC> queu;

    protected float moneyOwedIncreaseAmount;
    protected int satisfactionIncreaseAmount;
    protected int secondsBetweenActions;
    protected int maxQueueLength;
    protected DateTime lastActionTime;

    public Structure(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength,
        float moneyOwedIncreaseAmount, int satisfactionIncreaseAmount, int secondsBetweenActions, int maxQueueLength)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength) {

        this.moneyOwedIncreaseAmount = moneyOwedIncreaseAmount;
        this.satisfactionIncreaseAmount = satisfactionIncreaseAmount;
        this.secondsBetweenActions = secondsBetweenActions;
        this.maxQueueLength = maxQueueLength;

        queu = new Queue<NPC>();
        lastActionTime = DateTime.Now;
    }

    /// <summary>
    /// Action.
    /// </summary>
    public void Action(NPC npc) {
        AddNPCToQueueIfNotAlreadyInIt(npc);

        ActionWhenLastActionWithSecondsBetweenActionsIsSoonerThanNow();

        RemoveNPCInsideIfIsDestroyed();
        RemoveNPCsFromQueueIfIsDestroyed();
    }

    /// <summary>
    /// Action when last action with seconds between actions is sooner than now.
    /// </summary>
    protected void ActionWhenLastActionWithSecondsBetweenActionsIsSoonerThanNow() {
        if (lastActionTime <= DateTime.Now || npcInside == null) {
            RemoveNPCInside();

            LetFirstNPCInQueueInside();

            lastActionTime = DateTime.Now.AddSeconds(secondsBetweenActions);
        }
    }

    /// <summary>
    /// Add npc to queue if not already in it.
    /// </summary>
    protected void AddNPCToQueueIfNotAlreadyInIt(NPC npc) {
        if (!queu.Contains(npc) && npc != npcInside) {
            if (maxQueueLength <= queu.Count) {
                npc.SetIsBusy(false);
                npc.IncreaseOrDecreaseSatisfaction(-satisfactionIncreaseAmount);
                return;
            }

            npc.IncreaseOrDecreaseSatisfaction(-queu.Count);
            npc.SetIsVisible(false);
            queu.Enqueue(npc);
        }
    }

    /// <summary>
    /// Let first npc in queue inside.
    /// </summary>
    protected void LetFirstNPCInQueueInside() {
        if (0 < queu.Count) {
            npcInside = queu.Dequeue();
        }
    }

    /// <summary>
    /// Remove npc inside if is destroyed.
    /// </summary>
    protected void RemoveNPCInsideIfIsDestroyed() {
        if (isDestroyed && npcInside != null) {
            npcInside.SetIsBusy(false);
            npcInside.SetIsVisible(true);

            npcInside.IncreaseOrDecreaseSatisfaction(-satisfactionIncreaseAmount);

            npcInside = null;
        }
    }

    /// <summary>
    /// Remove npc inside.
    /// </summary>
    protected void RemoveNPCInside() {
        if (npcInside != null) {
            npcInside.SetIsBusy(false);
            npcInside.SetIsVisible(true);

            npcInside.IncreaseOrDecreaseSatisfaction(satisfactionIncreaseAmount);
            npcInside.IncreaseOrDecreaseMoneyOwed(moneyOwedIncreaseAmount);

            npcInside = null;
        }
    }

    /// <summary>
    /// Remove npcs from queue if is destroyed.
    /// </summary>
    protected void RemoveNPCsFromQueueIfIsDestroyed() {
        if (isDestroyed) {
            while (queu.Count != 0) {
                NPC npcInQueue = queu.Dequeue();

                npcInQueue.SetIsBusy(false);
                npcInQueue.SetIsVisible(true);

                npcInQueue.IncreaseOrDecreaseSatisfaction(-satisfactionIncreaseAmount);
            }
        }
    }
}
