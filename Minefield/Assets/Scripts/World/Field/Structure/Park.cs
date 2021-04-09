using System;
using UnityEngine;

public class Park : Structure {

    public Park(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, 
        int areaWidth, int areaLength, int satisfactionIncreaseAmount, int secondsBetweenActions)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength,
            0, satisfactionIncreaseAmount, secondsBetweenActions, 0) {

        lastActionTime = DateTime.UtcNow.AddSeconds(secondsBetweenActions);
    }

    /// <summary>
    /// Update.
    /// </summary>
    public override void Update(NPC npc) {
        AddNPCToQueueIfNotAlreadyInIt(npc);

        ActionWhenLastActionWithSecondsBetweenActionsIsSoonerThanNow();
    }

    /// <summary>
    /// Add npc to queue if not already in it.
    /// </summary>
    protected override void AddNPCToQueueIfNotAlreadyInIt(NPC npc) {
        if (!queu.Contains(npc)) {
            queu.Enqueue(npc);

            npc.IncreaseOrDecreaseSatisfaction(satisfactionIncreaseAmount);
        }
    }

    /// <summary>
    /// Action when last action with seconds between actions is sooner than now.
    /// </summary>
    protected override void ActionWhenLastActionWithSecondsBetweenActionsIsSoonerThanNow() {
        if (lastActionTime <= DateTime.Now) {
            RemoveNPCFromQueu();

            lastActionTime = DateTime.Now.AddSeconds(secondsBetweenActions);
        }
    }

    /// <summary>
    /// Remove npc from queue.
    /// </summary>
    protected void RemoveNPCFromQueu() {
        queu.Dequeue();
    }

    /// <summary>
    /// Destroy game object.
    /// </summary>
    public override void DestroyGameObject() {
        GameObject.Destroy(gameObject);
        gameObject = null;
        RemoveNPCsFromQueue();
        isDestroyed = true;
    }

    /// <summary>
    /// Remove npcs from queue.
    /// </summary>
    protected void RemoveNPCsFromQueue() {
        while (queu.Count != 0) {
            queu.Dequeue();
        }
    }
}
