using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Button buildRoadButton;
    [SerializeField]
    private Button buildStructureButton;
    [SerializeField]
    private Button destroyButton;

    [SerializeField]
    private GameObject structurePanel;

    [SerializeField]
    private Color buttonOutlineColor;
    List<Button> buttons;

    private Action onBuildRoadAction;
    private Action onBuildStructureAction;
    private Action onDestroyAction;

    private void Start() {
        buttons = new List<Button> { buildStructureButton, buildRoadButton, destroyButton };

        buildRoadButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildRoadButton);
            onBuildRoadAction?.Invoke();
        });

        buildStructureButton.onClick.AddListener(() => {
            structurePanel.gameObject.SetActive(true);
        });

        destroyButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(destroyButton);
            onDestroyAction?.Invoke();
        });
    }

    private void ModifyButtonOutline(Button button) {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = buttonOutlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor() {
        foreach (Button button in buttons) {
            button.GetComponent<Outline>().enabled = false;
        }
    }

    public void AssignMethodToOnBuildRoadAction(Action action) {
        onBuildRoadAction += action;
    }

    public void AssignMethodToOnBuildStructureAction(Action action) {
        onBuildStructureAction += action;
    }

    public void AssignMethodToOnDestroyAction(Action action) {
        onDestroyAction += action;
    }
}
