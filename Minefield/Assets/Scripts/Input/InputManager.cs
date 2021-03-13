using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask groundMask;

    private Action<Vector3Int> onMouseClickAction;
    private Action<Vector3Int> onMouseHoldAction;
    private Action onMouseUpAction;

    private Vector3 cameraMovementVector;

    private void Update() {
        InvokeMouseClickActionIfApplicable();
        InvokeMouseUpActionIfApplicable();
        InvokeMouseHoldActionIfApplicable();
        UpdateCameraMovementVector();
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

    private void UpdateCameraMovementVector() {
        cameraMovementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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

    public void ResetMousActions() {
        onMouseClickAction = null;
        onMouseHoldAction = null;
        onMouseUpAction = null;
    }

    public Vector3 GetCameraMovementVector() {
        return cameraMovementVector;
    }
}
