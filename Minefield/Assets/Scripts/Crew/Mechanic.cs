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
}
