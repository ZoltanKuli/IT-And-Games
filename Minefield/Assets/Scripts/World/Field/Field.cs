using UnityEngine;

public abstract class Field {

    protected Vector3Int origoPosition;
    
    public Field(Vector3Int origoPosition) {
        this.origoPosition = origoPosition;
    }

    /// <summary>
    /// Get origo position.
    /// </summary>
    public Vector3Int GetOrigoPosition() {
        return origoPosition;
    }
}
