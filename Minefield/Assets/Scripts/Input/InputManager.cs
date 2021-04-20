using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Camera mainCamera;
    private Vector3 cameraMovementDirection;
    private Vector3 cameraZoomDirection;
    private Vector3 cameraRotationDirection;

    [SerializeField]
    private LayerMask groundMask;

    private Action<Vector3Int> onMouseClickAction;
    private Action<Vector3Int> onMouseHoldAction;
    private Action onMouseUpAction;

    [SerializeField]
    private UIManager uIManager;

    /// <summary>
    /// Update.
    /// </summary>
    private void Update() {
        InvokeButtonDownIfApplicable();
        InvokeMouseClickActionIfApplicable();
        InvokeMouseUpActionIfApplicable();
        InvokeMouseHoldActionIfApplicable();
        UpdateCameraMovementDirection();
        UpdateCameraZoomAndRotationDirection();
    }

    /// <summary>
    /// Returns the rounded click position.
    /// </summary>
    private Vector3Int? RaycastGround() {
        RaycastHit raycastHit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, groundMask)) {
            Vector3Int positionInt = Vector3Int.RoundToInt(raycastHit.point);
            return positionInt;
        }

        return null;
    }

    /// <summary>
    /// Invoke keyboard key down action if applicable.
    /// </summary>
    private void InvokeButtonDownIfApplicable() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            uIManager.ToggleDisplayPanel(null, false);
            gameManager.ContinueGame();
        }
    }

    /// <summary>
    /// Invoke mouse click action if applicable.
    /// </summary>
    private void InvokeMouseClickActionIfApplicable() {
        if (Input.GetMouseButtonDown(1)) {
            uIManager.ResetButtonColor();
            ResetMouseActions();
        }

        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseClickAction?.Invoke(position.Value);
        }
    }

    /// <summary>
    /// Invoke mouse hold action if applicable.
    /// </summary>
    private void InvokeMouseHoldActionIfApplicable() {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            var position = RaycastGround();
            if (position != null)
                onMouseHoldAction?.Invoke(position.Value);
        }
    }

    /// <summary>
    /// Invoke mouse up action if applicable.
    /// </summary>
    private void InvokeMouseUpActionIfApplicable() {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            onMouseUpAction?.Invoke();
        }
    }

    /// <summary>
    /// Update camera movement direction.
    /// </summary>
    private void UpdateCameraMovementDirection() {
        cameraMovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    /// <summary>
    /// Update camera zoom and rotation direction.
    /// </summary>
    private void UpdateCameraZoomAndRotationDirection() {
        cameraZoomDirection = new Vector3(0, 0, 0);
        cameraRotationDirection = new Vector3(0, 0, 0);

        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            cameraZoomDirection = new Vector3(0, 1, -1);
            cameraRotationDirection = new Vector3(1, 0, 0);
        } else if (0 < Input.GetAxis("Mouse ScrollWheel")) {
            cameraZoomDirection = new Vector3(0, -1, 1);
            cameraRotationDirection = new Vector3(-1, 0, 0);
        }
    }

    /// <summary>
    /// Assign method to on mouse click action.
    /// </summary>
    public void AssignMethodToOnMouseClickAction(Action<Vector3Int> action) {
        onMouseClickAction += action;
    }

    /// <summary>
    /// Assign method to on mouse hold action.
    /// </summary>
    public void AssignMethodToOnMouseHoldAction(Action<Vector3Int> action) {
        onMouseHoldAction += action;
    }

    /// <summary>
    /// Assign method to on mouse up action.
    /// </summary>
    public void AssignMethodToOnMouseUpAction(Action action) {
        onMouseUpAction += action;
    }

    /// <summary>
    /// Reset mouse actions.
    /// </summary>
    public void ResetMouseActions() {
        onMouseClickAction = null;
        onMouseHoldAction = null;
        onMouseUpAction = null;
    }

    /// <summary>
    /// Get camera movement direction.
    /// </summary>
    public Vector3 GetCameraMovementDirection() {
        return cameraMovementDirection;
    }

    /// <summary>
    /// Get camera zoom direction.
    /// </summary>
    public Vector3 GetCameraZoomDirection() {
        return cameraZoomDirection;
    }

    /// <summary>
    /// Get camera rotation direction.
    /// </summary>
    public Vector3 GetCameraRotationDirection() {
        return cameraRotationDirection;
    }
}
