using System;
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

    private Vector3 cameraMovementVector;

    private void Update() {
        InvokeClickDownEventIfApplicable();
        InvokeClickUpEventIfApplicable();
        InvokeClickHoldEventIfApplicable();
        UpdateCameraMovementVector();
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

    private void InvokeClickDownEventIfApplicable() {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseClick?.Invoke(position.Value);
        }
    }

    private void InvokeClickUpEventIfApplicable() {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            onMouseUp?.Invoke();
        }
    }

    private void InvokeClickHoldEventIfApplicable() {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseHold?.Invoke(position.Value);
        }
    }

    private void UpdateCameraMovementVector() {
        cameraMovementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public void AssignMethodToOnMouseClick(Action<Vector3Int> action) {
        onMouseClick += action;
    }

    public void AssignMethodToOnMouseHold(Action<Vector3Int> action) {
        onMouseHold += action;
    }

    public void AssignMethodToOnMouseUp(Action action) {
        onMouseUp += action;
    }

    public Vector3 GetCameraMovementVector() {
        return cameraMovementVector;
    }
}
