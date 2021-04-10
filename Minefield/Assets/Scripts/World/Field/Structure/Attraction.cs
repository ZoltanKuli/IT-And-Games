using UnityEngine;

public class Attraction : Structure {

    public Attraction(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, 
        float yAngle, int areaWidth, int areaLength, int moneyOwedIncreaseAmount, 
        int satisfactionIncreaseAmount, int secondsBetweenActions, 
        int maxQueueLength, WorldManager worldManager,
        int minimumNotBreakingSeconds, int maximumNotBreakingSeconds,
        int satisfactionDecreaseAmount)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength,
            moneyOwedIncreaseAmount, satisfactionIncreaseAmount, secondsBetweenActions, 
            maxQueueLength, worldManager, minimumNotBreakingSeconds, maximumNotBreakingSeconds,
            satisfactionDecreaseAmount) {
    }
}
