﻿using UnityEngine;

public abstract class PopulatedField : Field {

    protected GameObject gameObject;

    protected int areaWidth;
    protected int areaLength;

    public PopulatedField(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength) : base(origoPosition) {
        gameObject = GameObject.Instantiate(prefab, origoPosition + prefabOffset, Quaternion.identity);
        SetRotation(yAngle);

        this.areaWidth = areaWidth;
        this.areaLength = areaLength;
    }

    public void SetRotation(float yAngle) {
        gameObject.transform.rotation = Quaternion.Euler(0, yAngle, 0);
    }

    public void DestroyGameObject() {
        GameObject.Destroy(gameObject);
    }

    public int getWidth() {
        return areaWidth;
    }

    public int getLength() {
        return areaLength;
    }
}