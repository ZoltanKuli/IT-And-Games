using UnityEngine;

public class Entrance : PopulatedField {

    public Entrance(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, 
        float yAngle, WorldManager worldManager)
        : base(prefab, origoPosition, prefabOffset, yAngle, 1, 1, worldManager) {
    }
}
