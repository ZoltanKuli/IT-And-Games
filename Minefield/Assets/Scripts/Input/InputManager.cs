using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask groundMask;

    private Action<Vector3Int> onMouseClick;
    private Action<Vector3Int> onMouseHold;
    private Action onMouseUp;

    private Vector2 cameraMovementVector;

    private void Update() {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckArrowInput();
    }

    private Vector3Int? RaycastGround() {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask)) {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }

        return null;
    }

    private void CheckClickDownEvent() {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseClick?.Invoke(position.Value);
        }
    }

    private void CheckClickUpEvent() {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            onMouseUp?.Invoke();
        }
    }

    private void CheckClickHoldEvent() {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseHold?.Invoke(position.Value);
        }
    }

    public void AssignMethodToOnMouseClick(Action<Vector3Int> action) {
        onMouseClick += action;
    }

    public void AssignMethodToOnMouseHold(Action<Vector3Int> action) {
        onMouseHold += action;
    }

    public void AssignMethodToOnMouseHold(Action action) {
        onMouseUp += action;
    }

    private void CheckArrowInput() {
        cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public Vector2 GetCameraMovementVector() {
        return cameraMovementVector;
    }
}
