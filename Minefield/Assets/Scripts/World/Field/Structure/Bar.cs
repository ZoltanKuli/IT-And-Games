using UnityEngine;

public class Bar : Structure {

    public Bar(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength) {
    }
}
