using UnityEngine;

public class Park : Structure {

    public Park(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength) {
    }
}
