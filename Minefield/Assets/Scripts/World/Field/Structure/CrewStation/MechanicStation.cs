using UnityEngine;

public class MechanicStation : CrewStation {

    public MechanicStation(GameObject prefab, Vector3Int origoPosition,
        Vector3 prefabOffset, float yAngle, WorldManager worldManager,
        GameObject crewPrefab, float crewSpeed, int secondsUntilActionIsFinished, 
        float distancePrecision, float rotationSpeedMultiplier, int maximumTravelDistance)
        : base(prefab, origoPosition, prefabOffset, yAngle, worldManager,
            new Mechanic(crewPrefab, origoPosition, crewSpeed, secondsUntilActionIsFinished, 
                worldManager, distancePrecision, rotationSpeedMultiplier, maximumTravelDistance)) {
        crew.SetCrewStation(this);
    }
}
