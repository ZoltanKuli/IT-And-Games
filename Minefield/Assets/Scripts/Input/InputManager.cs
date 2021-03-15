using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera;
    private Vector3 cameraMovementOffset;
    private Vector3 cameraZoomOffset;
    private Vector3 cameraRotationOffset;

    [SerializeField]
    private LayerMask groundMask;

    private Action<Vector3Int> onMouseClickAction;
    private Action<Vector3Int> onMouseHoldAction;
    private Action onMouseUpAction;

    private void Update() {
        InvokeMouseClickActionIfApplicable();
        InvokeMouseUpActionIfApplicable();
        InvokeMouseHoldActionIfApplicable();
        UpdateCameraMovementOffset();
        UpdateCameraZoomOffset();
    }

    private Vector3Int? RaycastGround() {
        RaycastHit raycastHit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, groundMask)) {
            Vector3Int positionInt = Vector3Int.RoundToInt(raycastHit.point);
            return positionInt;
        }

        return null;
    }

    private void InvokeMouseClickActionIfApplicable() {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseClickAction?.Invoke(position.Value);
        }
    }

    private void InvokeMouseHoldActionIfApplicable() {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseHoldAction?.Invoke(position.Value);
        }
    }

    private void InvokeMouseUpActionIfApplicable() {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            onMouseUpAction?.Invoke();
        }
    }

    private void UpdateCameraMovementOffset() {
        cameraMovementOffset = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void UpdateCameraZoomOffset() {
        cameraZoomOffset = new Vector3(0, 0, 0);
        cameraRotationOffset = new Vector3(0, 0, 0);

        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            cameraZoomOffset = new Vector3(0, 1, -1);
            cameraRotationOffset = new Vector3(1, 0, 0);
        } else if (0 < Input.GetAxis("Mouse ScrollWheel")) {
            cameraZoomOffset = new Vector3(0, -1, 1);
            cameraRotationOffset = new Vector3(-1, 0, 0);
        }
    }

    public void AssignMethodToOnMouseClickAction(Action<Vector3Int> action) {
        onMouseClickAction += action;
    }

    public void AssignMethodToOnMouseHoldAction(Action<Vector3Int> action) {
        onMouseHoldAction += action;
    }

    public void AssignMethodToOnMouseUpAction(Action action) {
        onMouseUpAction += action;
    }

    public void ResetMouseActions() {
        onMouseClickAction = null;
        onMouseHoldAction = null;
        onMouseUpAction = null;
    }

    public Vector3 GetCameraMovementOffset() {
        return cameraMovementOffset;
    }

    public Vector3 GetCameraZoomOffset() {
        return cameraZoomOffset;
    }

    public Vector3 GetCameraRotationOffset() {
        return cameraRotationOffset;
    }
}
