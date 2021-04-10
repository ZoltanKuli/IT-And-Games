using System;
using UnityEngine;

public class Mechanic : Crew {

    public Mechanic(GameObject prefab, Vector3Int crewStationPosition, 
        float speed, int secondsUntilActionIsFinished, WorldManager worldManager, 
        float distancePrecision, float rotationSpeedMultiplier,
        int maximumTravelDistance)
        : base(prefab, crewStationPosition, speed, 
            secondsUntilActionIsFinished, worldManager,
            distancePrecision, rotationSpeedMultiplier,
            maximumTravelDistance) {
    }

    /// <summary>
    /// Do action.
    /// </summary>
    protected override void DoAction() {
        Structure structureToDoActionOn = (Structure)fieldToDoActionOn;
        if (isBusy && timeOfFinishingAction <= DateTime.UtcNow || (structureToDoActionOn != null && structureToDoActionOn.IsDestroyed())) {
            structureToDoActionOn.Fix();
            isBusy = false;
            if (worldManager.GetNumberOfOutOfOrderStructures() == 0) {
                fieldToDoActionOn = crewStation;
                SetPath();
            } else {
                fieldToDoActionOn = null;
            }
        }
    }

    /// <summary>
    /// Readd field to do action on to world manager if field is not destroyed and reset field to do action on.
    /// </summary>
    protected override void ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn() {
        if (fieldToDoActionOn == null) {
            return;
        }

        if (fieldToDoActionOn == crewStation) {
            fieldToDoActionOn = null;
            return;
        }

        Structure structureToDoActionOn = (Structure)fieldToDoActionOn;
        if (!structureToDoActionOn.IsDestroyed()) {
            worldManager.AddOutOfOrderStructure(structureToDoActionOn);
        }

        isBusy = false;

        base.ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn();
    }
}
