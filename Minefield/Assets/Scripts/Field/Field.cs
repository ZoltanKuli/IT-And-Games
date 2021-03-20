using UnityEngine;

public abstract class Field {

    protected Vector3Int origoPosition;
    
    public Field(Vector3Int origoPosition) {
        this.origoPosition = origoPosition;
    }

    public Vector3Int getOrigoPosition() {
        return origoPosition;
    }
}
