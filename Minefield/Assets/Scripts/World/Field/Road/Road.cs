using UnityEngine;

public abstract class Road : PopulatedField {

    public Road(GameObject prefab, Vector3Int origoPosition, float yAngle) 
        : base(prefab, origoPosition, new Vector3(0, 0, 0), yAngle, 1, 1) { 
    }
}
