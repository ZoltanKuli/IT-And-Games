using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private CameraMovement cameraMovement;
    [SerializeField]
    private InputManager inputManager;

    private void Start() {
        inputManager.AssignMethodToOnMouseClick(HandleMouseClick);
    }

    private void HandleMouseClick(Vector3Int position) {
        Debug.Log(position);
    }

    private void Update() {
        cameraMovement.MoveCamera(new Vector3(inputManager.GetCameraMovementVector().x, 0, inputManager.GetCameraMovementVector().y));
    }
}
