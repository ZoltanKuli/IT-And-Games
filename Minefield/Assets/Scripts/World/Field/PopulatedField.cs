using UnityEngine;

public abstract class PopulatedField : Field {

    protected GameObject gameObject;

    public PopulatedField(GameObject gameObjectPrefab, Vector3Int origoPosition) : base(origoPosition) {
        gameObject = GameObject.Instantiate(gameObjectPrefab, origoPosition, Quaternion.identity);
    }

    public void DestroyGameObject() {
        GameObject.Destroy(gameObject);
    }
}
