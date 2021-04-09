﻿using System.Collections.Generic;
using UnityEngine;

public abstract class Road : PopulatedField {

    private float garbageRange;
    private int maximumNumberOfGarbages;
    List<GameObject> garbagePrefabs;

    private List<GameObject> garbageGameObjects;

    private System.Random random;

    public Road(GameObject prefab, Vector3Int origoPosition, float yAngle, 
        List<GameObject> garbagePrefabs, float garbageRange, int maximumNumberOfGarbageGameObjects) 
        : base(prefab, origoPosition, new Vector3(0, 0, 0), yAngle, 1, 1) {

        this.garbagePrefabs = garbagePrefabs;
        this.garbageRange = garbageRange;
        this.maximumNumberOfGarbages = maximumNumberOfGarbageGameObjects;

        garbageGameObjects = new List<GameObject>();

        random = new System.Random(GetHashCode());
    }

    /// <summary>
    /// Place a garbage game object on the road at a random location.
    /// </summary>
    public void PlaceAGarbageGameObjectOnTheRoadAtARandomLocation() {
        if (garbageGameObjects.Count < maximumNumberOfGarbages) {
            GameObject randomGarbagePrefab = garbagePrefabs[random.Next(garbagePrefabs.Count)];

            float test = (float)(random.NextDouble() * (garbageRange * 2) - garbageRange);

            Vector3 garbageGameObjectPosition = new Vector3(origoPosition.x + test, origoPosition.y,
                origoPosition.z + (float)(random.NextDouble() * (garbageRange * 2) - garbageRange));

            garbageGameObjects.Add(GameObject.Instantiate(randomGarbagePrefab, garbageGameObjectPosition, Quaternion.Euler(0, random.Next(360), 0)));
        }
    }

    /// <summary>
    /// Is littered with garbage.
    /// </summary>
    public bool IsLitteredWithGarbage() {
        return 0 < garbageGameObjects.Count;
    }

    /// <summary>
    /// Clean up garbage.
    /// </summary>
    public void CleanUpGarbage() {
        foreach (GameObject garbageGameObject in garbageGameObjects) {
            GameObject.Destroy(garbageGameObject);
        }

        garbageGameObjects.Clear();
    }

    /// <summary>
    /// Destroy game object.
    /// </summary>
    public override void DestroyGameObject() {
        GameObject.Destroy(gameObject);
        gameObject = null;
        CleanUpGarbage();
        isDestroyed = true;
    }
}
