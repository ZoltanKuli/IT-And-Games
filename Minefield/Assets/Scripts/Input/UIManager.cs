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
    private Button barSelectorButton;

    [SerializeField]
    private GameObject barSelectorPanel;

    [SerializeField]
    private Button cafeButton;

    [SerializeField]
    private Button cafeRestaurantButton;

    [SerializeField]
    private Color buttonOutlineColor;

    List<Button> buttons;
    List<GameObject> panels;

    private Action onBuildRoadAction;
    private Action onDestroyAction;

    private Action onCafeBuildAction;
    private Action onCafeRestaurantBuildAction;


    private void Start() {
        buttons = new List<Button> { buildStructureButton, buildRoadButton, destroyButton };
        panels = new List<GameObject> { structurePanel, barSelectorPanel };

        buildStructureButton.onClick.AddListener(() => {
            if(structurePanel.gameObject.activeSelf)
            {
                ToggleDisplayPanel(structurePanel, false);
            } else
            {
                ToggleDisplayPanel(structurePanel, true);
            }
        });

        buildRoadButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildRoadButton);
            onBuildRoadAction?.Invoke();
        });

        destroyButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(destroyButton);
            onDestroyAction?.Invoke();
        });

        barSelectorButton.onClick.AddListener(() =>
        {
            if (barSelectorPanel.gameObject.activeSelf)
            {
                ToggleDisplayPanel(barSelectorPanel, false);
            }
            else
            {
                ToggleDisplayPanel(barSelectorPanel, true);
            }
        });

        cafeButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onCafeBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        cafeRestaurantButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onCafeRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
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

    private void ToggleDisplayPanel(GameObject currentPanel, bool value)
    {
        foreach(GameObject panel in panels) {
            if (panel == currentPanel)
            {
                currentPanel.gameObject.SetActive(value);
            } else
            {
                panel.gameObject.SetActive(false);
            }
        }
    }

    public void AssignMethodToOnBuildRoadAction(Action action) {
        onBuildRoadAction += action;
    }

    public void AssignMethodToOnDestroyAction(Action action) {
        onDestroyAction += action;
    }

    public void AssingMethodToOnCafeBuildAction(Action action)
    {
        onCafeBuildAction += action;
    }

    public void AssingMethodToOnCafeRestaurantBuildAction(Action action)
    {
        onCafeRestaurantBuildAction += action;
    }
}
