using System.Collections.Generic;
using UnityEngine;

public class FourWayRoad : Road {

    public FourWayRoad(GameObject prefab, Vector3Int origoPosition, float yAngle, 
        List<GameObject> garbagePrefabs, float garbageRange, int maximumNumberOfGarbages) 
        : base(prefab, origoPosition, yAngle, garbagePrefabs, garbageRange, maximumNumberOfGarbages) {
    }
}
