using System;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : Crew {

    public Cleaner(GameObject prefab, Vector3Int crewStationPosition, 
        float speed, int secondsUntilActionIsFinished, WorldManager worldManage, 
        float distancePrecision, float rotationSpeedMultiplier,
        int maximumTravelDistance)
        : base(prefab, crewStationPosition, speed, 
            secondsUntilActionIsFinished, worldManage,
            distancePrecision, rotationSpeedMultiplier,
            maximumTravelDistance) {
    }

    /// <summary>
    /// Do action.
    /// </summary>
    protected override void DoAction() {
        if (isBusy && timeOfFinishingAction <= DateTime.UtcNow) {
            Road roadToDoActionOn = (Road)fieldToDoActionOn;
            roadToDoActionOn.CleanUpGarbage();
            isBusy = false;
            if (worldManager.GetNumberOfRoadsLitteredWithGarbage() == 0) {
                fieldToDoActionOn = crewStation;
                SetPath();
            } else {
                fieldToDoActionOn = null;
            }
        }
    }

    /// <summary>
    /// Set path.
    /// </summary>
    protected override void SetPath() {
        path = new List<Field>();
        path = PathFinder.FindPath(worldManager, worldManager.GetFieldAtPosition(GetPositionRoundedToVector3Int()), fieldToDoActionOn, false);

        if (path.Count == 0 || (maximumTravelDistance < path.Count && fieldToDoActionOn != crewStation)) {
            ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn();
            Debug.Log(path.Count);
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

        Road roadToDoActionOn = (Road)fieldToDoActionOn;
        if (!roadToDoActionOn.IsDestroyed()) {
            worldManager.AddRoadLitteredWithGarbage(roadToDoActionOn);
        }

        isBusy = false;

        base.ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn();
    }
}
