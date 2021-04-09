using System.Collections.Generic;
using UnityEngine;

public class ThreeWayRoad : Road {

    public ThreeWayRoad(GameObject prefab, Vector3Int origoPosition, float yAngle, 
        List<GameObject> garbagePrefabs, float garbageRange, int maximumNumberOfGarbages) 
        : base(prefab, origoPosition, yAngle, garbagePrefabs, garbageRange, maximumNumberOfGarbages) {
    }
}
