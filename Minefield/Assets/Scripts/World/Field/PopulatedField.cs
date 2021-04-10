using UnityEngine;

public abstract class PopulatedField : Field {

    protected GameObject gameObject;
    protected bool isDestroyed;

    protected int areaWidth;
    protected int areaLength;

    protected WorldManager worldManager;

    public PopulatedField(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, 
        float yAngle, int areaWidth, int areaLength, WorldManager worldManager) : base(origoPosition) {
        gameObject = GameObject.Instantiate(prefab, origoPosition + prefabOffset, Quaternion.identity);
        SetRotation(yAngle);

        isDestroyed = false;

        this.areaWidth = areaWidth;
        this.areaLength = areaLength;

        this.worldManager = worldManager;
    }

    /// <summary>
    /// Set rotation.
    /// </summary>
    public void SetRotation(float yAngle) {
        gameObject.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }

    /// <summary>
    /// Destroy game object.
    /// </summary>
    public virtual void DestroyGameObject() {
        GameObject.Destroy(gameObject);
        gameObject = null;
        isDestroyed = true;
    }

    /// <summary>
    /// Is destroyed.
    /// </summary>
    public bool IsDestroyed() {
        return isDestroyed;
    }

    /// <summary>
    /// Get width.
    /// </summary>
    public int GetWidth() {
        return areaWidth;
    }

    /// <summary>
    /// Get length.
    /// </summary>
    public int GetLength() {
        return areaLength;
    }
}
