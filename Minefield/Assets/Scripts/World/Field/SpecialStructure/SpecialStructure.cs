﻿using UnityEngine;

public class SpecialStructure : PopulatedField {

    public SpecialStructure(GameObject prefab, Vector3Int origoPosition, Vector3 prefabOffset, float yAngle, int areaWidth, int areaLength)
        : base(prefab, origoPosition, prefabOffset, yAngle, areaWidth, areaLength) {
    }
}