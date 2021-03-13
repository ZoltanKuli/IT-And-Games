using UnityEngine;

public abstract class Road : PopulatedField {

    public Road(GameObject gameObjectPrefab, Vector3Int origoPosition, float yAngle) : base(gameObjectPrefab, origoPosition) {
        SetRotation(yAngle);
    }

    public void SetRotation(float yAngle) {
        gameObject.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }
}
