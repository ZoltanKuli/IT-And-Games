using UnityEngine;

public class Park : Structure {

    public Park(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength,
        float moneyOwedIncreaseAmount, int satisfactionIncreaseAmount, int secondsBetweenActions)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength,
            moneyOwedIncreaseAmount, satisfactionIncreaseAmount, secondsBetweenActions) {
    }
}
