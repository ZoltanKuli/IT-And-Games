using UnityEngine;

public abstract class Structure : PopulatedField {

    protected int width;
    protected int length;

    public Structure(GameObject gameObjectPrefab, Vector3Int origoPosition, int width, int length) : base(gameObjectPrefab, origoPosition) {
        this.width = width;
        this.length = length;
    }

    public int getWidth() {
        return width;
    }

    public int getLength() {
        return length;
    }
}
