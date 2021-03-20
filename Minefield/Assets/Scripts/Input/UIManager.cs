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
    private Button restaurantSelectionButton;

    [SerializeField]
    private Button attractionSelectionButton;

    [SerializeField]
    private Button parkSelectionButton;

    [SerializeField]
    private GameObject barSelectorPanel;

    [SerializeField]
    private Button cafeButton;

    [SerializeField]
    private Button cafeRestaurantButton;

    [SerializeField]
    private GameObject restaurantSelectorPanel;

    [SerializeField]
    private Button hotdogCarButton;

    [SerializeField]
    private Button kfcRestaurantButton;

    [SerializeField]
    private Button olivegardensRestaurantButton;

    [SerializeField]
    private Button taverneRestaurantButton;

    [SerializeField]
    private GameObject attractionSelectorPanel;

    [SerializeField]
    private Button circustentButton;

    [SerializeField]
    private Button londoneyeButton;

    [SerializeField]
    private Button merrygoroundButton;

    [SerializeField]
    private Button rollercoasterButton;

    [SerializeField]
    private GameObject parkSelectorPanel;

    [SerializeField]
    private Button parkbasicButton;

    [SerializeField]
    private Button parkfountainButton;

    [SerializeField]
    private Button parkhelicopterButton;

    [SerializeField]
    private Color buttonOutlineColor;

    List<Button> buttons;
    List<GameObject> panels;

    private Action onBuildRoadAction;
    private Action onDestroyAction;

    private Action onCafeBuildAction;
    private Action onCafeRestaurantBuildAction;


    private Action onHotdogCarBuildAction;
    private Action onKfcRestaurantBuildAction;
    private Action onOlivegardensRestaurantBuildAction;
    private Action onTaverneRestaurantBuildAction;

    private Action onCircusTentBuildAction;
    private Action onLondonEyeBuildAction;
    private Action onMerryGoRoundBuildAction;
    private Action onRollerCoasterBuildAction;

    private Action onParkBasicBuildAction;
    private Action onParkFountainBuildAction;
    private Action onParkHelicopterBuildAction;


    private void Start() {
        buttons = new List<Button> { buildStructureButton, buildRoadButton, destroyButton };
        panels = new List<GameObject> { structurePanel, barSelectorPanel, restaurantSelectorPanel, attractionSelectorPanel, parkSelectorPanel };

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

        buildStructureButton.onClick.AddListener(() => {
            if (structurePanel.gameObject.activeSelf) {
                ToggleDisplayPanel(structurePanel, false);
            } else {
                ToggleDisplayPanel(structurePanel, true);
            }
        });

        barSelectorButton.onClick.AddListener(() => {
            if (barSelectorPanel.gameObject.activeSelf) {
                ToggleDisplayPanel(barSelectorPanel, false);
            } else {
                ToggleDisplayPanel(barSelectorPanel, true);
            }
        });

        restaurantSelectionButton.onClick.AddListener(() => {
            if (restaurantSelectorPanel.gameObject.activeSelf) {
                ToggleDisplayPanel(restaurantSelectorPanel, false);
            } else {
                ToggleDisplayPanel(restaurantSelectorPanel, true);
            }
        });

        parkSelectionButton.onClick.AddListener(() => {
            if (parkSelectorPanel.gameObject.activeSelf) {
                ToggleDisplayPanel(parkSelectorPanel, false);
            } else {
                ToggleDisplayPanel(parkSelectorPanel, true);
            }
        });

        cafeButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onCafeBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        cafeRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onCafeRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        hotdogCarButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onHotdogCarBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        kfcRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onKfcRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        olivegardensRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onOlivegardensRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        taverneRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onTaverneRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        attractionSelectionButton.onClick.AddListener(() => {
            if (attractionSelectorPanel.gameObject.activeSelf) {
                ToggleDisplayPanel(attractionSelectorPanel, false);
            } else {
                ToggleDisplayPanel(attractionSelectorPanel, true);
            }
        });

        circustentButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onCircusTentBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        londoneyeButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onLondonEyeBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        merrygoroundButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onMerryGoRoundBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        rollercoasterButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onRollerCoasterBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        parkbasicButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onParkBasicBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        parkfountainButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onParkFountainBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        parkhelicopterButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildStructureButton);
            onParkHelicopterBuildAction?.Invoke();
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

    private void ToggleDisplayPanel(GameObject currentPanel, bool value) {
        foreach (GameObject panel in panels) {
            if (panel == currentPanel) {
                currentPanel.gameObject.SetActive(value);
            } else {
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

    public void AssingMethodToOnCafeBuildAction(Action action) {
        onCafeBuildAction += action;
    }

    public void AssingMethodToOnCafeRestaurantBuildAction(Action action) {
        onCafeRestaurantBuildAction += action;
    }

    public void AssingMethodToOnHotdogCarBuildAction(Action action) {
        onHotdogCarBuildAction += action;
    }

    public void AssingMethodToOnKfcRestaurantBuildAction(Action action) {
        onKfcRestaurantBuildAction += action;
    }

    public void AssingMethodToOnOlivegardenRestaurantBuildAction(Action action) {
        onOlivegardensRestaurantBuildAction += action;
    }

    public void AssingMethodToOnTaverneRestaurantBuildAction(Action action) {
        onTaverneRestaurantBuildAction += action;
    }

    public void AssingMethodToOnCircusTentBuildAction(Action action) {
        onCircusTentBuildAction += action;
    }

    public void AssingMethodToOnLondonEyeBuildAction(Action action) {
        onLondonEyeBuildAction += action;
    }

    public void AssingMethodToOnMerryGoRoundBuildAction(Action action) {
        onMerryGoRoundBuildAction += action;
    }

    public void AssingMethodToOnRollerCoasterBuildAction(Action action) {
        onRollerCoasterBuildAction += action;
    }

    public void AssingMethodToOnParkBasicBuildAction(Action action) {
        onParkBasicBuildAction += action;
    }

    public void AssingMethodToOnParkFountainBuildAction(Action action) {
        onParkFountainBuildAction += action;
    }

    public void AssingMethodToOnParkHelicopterBuildAction(Action action) {
        onParkHelicopterBuildAction += action;
    }
}
