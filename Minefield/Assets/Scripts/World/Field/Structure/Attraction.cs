﻿using UnityEngine;

public class Attraction : Structure {

    public Attraction(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength,
        float moneyOwedIncreaseAmount, int satisfactionIncreaseAmount, int secondsBetweenActions, int maxQueueLength)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength,
            moneyOwedIncreaseAmount, satisfactionIncreaseAmount, secondsBetweenActions, 
            maxQueueLength) {
    }
}
