﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Button buildRoadButton;

    [SerializeField]
    private Button buildMenuButton;

    [SerializeField]
    private Button buildStructureButton;

    [SerializeField]
    private Button destroyButton;

    [SerializeField]
    private GameObject structurePanel;

    [SerializeField]
    private GameObject roadandgarbagecanPanel;

    [SerializeField]
    private GameObject buildMenuPanel;

    [SerializeField]
    private Button roadandgarbagecanSelectionButton;

    [SerializeField]
    private Button crewstationSelectionButton;

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

    [SerializeField]
    private Button garbagecanButton;

    public List<Button> buttons;
    public List<GameObject> panels;

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

    private Action onGarbageCanBuildAction;

    /// <summary>
    /// Connect the menu buttons with the pairing building method.
    /// </summary>
    private void Start() {
        buttons = new List<Button> { buildMenuButton, destroyButton };
        panels = new List<GameObject> { roadandgarbagecanPanel, buildMenuPanel, structurePanel, barSelectorPanel, restaurantSelectorPanel, attractionSelectorPanel, parkSelectorPanel};

        destroyButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(destroyButton);
            onDestroyAction?.Invoke();
        });

        buildMenuButton.onClick.AddListener(() => {
            if (buildMenuPanel.gameObject.activeSelf) {
                ToggleDisplayPanel(buildMenuPanel, false);
            } else {
                ToggleDisplayPanel(buildMenuPanel, true);
            }
        });

        buildRoadButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onBuildRoadAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        garbagecanButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onGarbageCanBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });
        buildStructureButton.onClick.AddListener(() => {
            if (structurePanel.gameObject.activeSelf) {
                ToggleDisplayPanel(structurePanel, false);
            } else {
                ToggleDisplayPanel(structurePanel, true);
            }
        });

        roadandgarbagecanSelectionButton.onClick.AddListener(() => {
            if (roadandgarbagecanPanel.gameObject.activeSelf) {
                ToggleDisplayPanel(roadandgarbagecanPanel, false);
            } else {
                ToggleDisplayPanel(roadandgarbagecanPanel, true);
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
            ModifyButtonOutline(buildMenuButton);
            onCafeBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        cafeRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onCafeRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        hotdogCarButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onHotdogCarBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        kfcRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onKfcRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        olivegardensRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onOlivegardensRestaurantBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        taverneRestaurantButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
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
            ModifyButtonOutline(buildMenuButton);
            onCircusTentBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        londoneyeButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onLondonEyeBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        merrygoroundButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onMerryGoRoundBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        rollercoasterButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onRollerCoasterBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        parkbasicButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onParkBasicBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        parkfountainButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onParkFountainBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });

        parkhelicopterButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyButtonOutline(buildMenuButton);
            onParkHelicopterBuildAction?.Invoke();
            ToggleDisplayPanel(null, false);
        });
        
    }
      
    /// <summary>
    /// Modify button outline.
    /// </summary>
    private void ModifyButtonOutline(Button button) {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = buttonOutlineColor;
        outline.enabled = true;
    }
        
    /// <summary>
    /// Reset button color.
    /// </summary>
    public void ResetButtonColor() {
        foreach (Button button in buttons) {
            button.GetComponent<Outline>().enabled = false;
        }
    }

    /// <summary>
    /// Show the correct panel when clicking one in the menu.
    /// </summary>
    public void ToggleDisplayPanel(GameObject currentPanel, bool value) {
        foreach (GameObject panel in panels) {
            if (panel == currentPanel) {
                currentPanel.gameObject.SetActive(value);
            } else {
                panel.gameObject.SetActive(false);
            }
        }
    }
    
    /// <summary>
    /// Assign method to on build road action.
    /// </summary>
    public void AssignMethodToOnBuildRoadAction(Action action) {
        onBuildRoadAction += action;
    }
       
    /// <summary>
    /// Assign method to on destroy action.
    /// </summary>
    public void AssignMethodToOnDestroyAction(Action action) {
        onDestroyAction += action;
    }
          
    /// <summary>
    /// Assing method to on cafe build action.
    /// </summary>
    public void AssingMethodToOnCafeBuildAction(Action action) {
        onCafeBuildAction += action;
    }
             
    /// <summary>
    /// Assing method to on CafeRestaurant build action.
    /// </summary>
    public void AssingMethodToOnCafeRestaurantBuildAction(Action action) {
        onCafeRestaurantBuildAction += action;
    }
                
    /// <summary>
    /// Assing method to on HotdogCar build action.
    /// </summary>
    public void AssingMethodToOnHotdogCarBuildAction(Action action) {
        onHotdogCarBuildAction += action;
    }
                   
    /// <summary>
    /// Assing method to on KfcRestaurant build action.
    /// </summary>
    public void AssingMethodToOnKfcRestaurantBuildAction(Action action) {
        onKfcRestaurantBuildAction += action;
    }
                      
    /// <summary>
    /// Assing method to on OlivegardenRestaurant build action.
    /// </summary>
    public void AssingMethodToOnOlivegardenRestaurantBuildAction(Action action) {
        onOlivegardensRestaurantBuildAction += action;
    }
                         
    /// <summary>
    /// Assing method to on TaverneRestaurant build action.
    /// </summary>
    public void AssingMethodToOnTaverneRestaurantBuildAction(Action action) {
        onTaverneRestaurantBuildAction += action;
    }
                            
    /// <summary>
    /// Assing method to on CircusTent build action.
    /// </summary>
    public void AssingMethodToOnCircusTentBuildAction(Action action) {
        onCircusTentBuildAction += action;
    }
                               
    /// <summary>
    /// Assing method to on LondonEye build action.
    /// </summary>
    public void AssingMethodToOnLondonEyeBuildAction(Action action) {
        onLondonEyeBuildAction += action;
    }
                                  
    /// <summary>
    /// Assing method to on MerryGoRound build action.
    /// </summary>
    public void AssingMethodToOnMerryGoRoundBuildAction(Action action) {
        onMerryGoRoundBuildAction += action;
    }
                                     
    /// <summary>
    /// Assing method to on RollerCoaster build action.
    /// </summary>
    public void AssingMethodToOnRollerCoasterBuildAction(Action action) {
        onRollerCoasterBuildAction += action;
    }
                                        
    /// <summary>
    /// Assing method to on ParkBasic build action.
    /// </summary>
    public void AssingMethodToOnParkBasicBuildAction(Action action) {
        onParkBasicBuildAction += action;
    }
                                            
    /// <summary>
    /// Assing method to on ParkFountain build action.
    /// </summary>
    public void AssingMethodToOnParkFountainBuildAction(Action action) {
        onParkFountainBuildAction += action;
    }
                                               
    /// <summary>
    /// Assing method to on ParkHelicopter build action.
    /// </summary>
    public void AssingMethodToOnParkHelicopterBuildAction(Action action) {
        onParkHelicopterBuildAction += action;
    }

    /// <summary>
    /// Assing method to on GarbageCan build action.
    /// </summary>
    public void AssingMethodToOnGarbageCanBuildAction(Action action) {
        onGarbageCanBuildAction += action;
    }

}
