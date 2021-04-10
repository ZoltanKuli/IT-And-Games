using UnityEngine;

public class GarbageCan : Structure {

    public GarbageCan(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, 
        float yAngle, int areaWidth, int areaLength, WorldManager worldManager)
        : base(prefab, origoPosition, prefabOffset, yAngle, 
            areaWidth, areaLength, 0, 0, 0, 0, worldManager) {
    }
}
