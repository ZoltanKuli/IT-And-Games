using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC {

    private GameObject gameObject;
    private float originalHeight;
    private float loweringDistanceOnInvisibility;

    private WorldManager worldManager;

    private float distancePrecision;

    private bool isBusy;
    private float moneyOwed;

    private int satisfaction;
    private int minimumSatisfactionOfStaying;
    private int thirst;
    private int maximumThirstOfNotNeedingToDrink;
    private int hunger;
    private int maximumHungerOfNotNeedingToEat;

    private DateTime lastThirstGrowthTime;
    private int secondsUntilThirstGrowth;
    private int thirstGrowthAmount;
    private DateTime lastHungerGrowthTime;
    private int secondsUntilHungerGrowth;
    private int hungerGrowthAmount;

    private Field destinationStructure;
    private List<Field> path;
    private float minimumSpeed;
    private float maximumSpeed;
    private float rotationSpeedMultiplier;

    private System.Random random;

    public NPC(GameObject prefab, Vector3Int position, WorldManager worldManager, float distancePrecision,
        int defaultSatisfaction, int minimumSatisfactionOfStaying, int defaultThirst, int maximumThirstOfNotNeedingToDrink,
        int defaultHunger, int maximumHungerOfNotNeedingToEat, float minimumSpeed, float maximumSpeed, float rotationSpeedMultiplier,
        int secondsUntilThirstGrowth, int thirstGrowthAmount, int secondsUntilHungerGrowth, int hungerGrowthAmount,
        float loweringDistanceOnInvisibility) {
        gameObject = GameObject.Instantiate(prefab, position, Quaternion.identity);
        originalHeight = position.y;

        this.worldManager = worldManager;

        this.distancePrecision = distancePrecision;

        isBusy = false;

        satisfaction = defaultSatisfaction;
        this.minimumSatisfactionOfStaying = minimumSatisfactionOfStaying;
        thirst = defaultThirst;
        this.maximumThirstOfNotNeedingToDrink = maximumThirstOfNotNeedingToDrink;
        hunger = defaultHunger;
        this.maximumHungerOfNotNeedingToEat = maximumHungerOfNotNeedingToEat;

        this.minimumSpeed = minimumSpeed;
        this.maximumSpeed = maximumSpeed;
        this.rotationSpeedMultiplier = rotationSpeedMultiplier;

        lastThirstGrowthTime = DateTime.Now;
        this.secondsUntilThirstGrowth = secondsUntilThirstGrowth;
        this.thirstGrowthAmount = thirstGrowthAmount;
        lastHungerGrowthTime = DateTime.Now;
        this.secondsUntilHungerGrowth = secondsUntilHungerGrowth;
        this.hungerGrowthAmount = hungerGrowthAmount;

        this.loweringDistanceOnInvisibility = loweringDistanceOnInvisibility;

        random = new System.Random(gameObject.GetHashCode());
    }

    /// <summary>
    /// Update.
    /// </summary>
    public void Update() {
        IncreaseThirstAndHunger();

        SetDestinationStructureAndPathIfNotBusy();
        MoveGameObjectToDestinationIfNotAlreadyThere();

        EnterDestinationStructure();

        DestroyGameObjectIfSatisfactionIsLowerThanMinimumSatisfactionOfstaying();
    }

    /// <summary>
    /// Increase thirst and hunger.
    /// </summary>
    private void IncreaseThirstAndHunger() {
        IncreaseThirst();
        IncreaseHunger();
    }

    /// <summary>
    /// Increase thirst.
    /// </summary>
    private void IncreaseThirst() {
        if (lastThirstGrowthTime.AddSeconds(secondsUntilThirstGrowth) <= DateTime.Now) {
            lastThirstGrowthTime = DateTime.Now;

            if ((thirst + thirstGrowthAmount) <= 100) {
                thirst += thirstGrowthAmount;
            } else {
                thirst = 100;
            }
        }
    }

    /// <summary>
    /// Increase hunger.
    /// </summary>
    private void IncreaseHunger() {
        if (lastHungerGrowthTime.AddSeconds(secondsUntilHungerGrowth) <= DateTime.Now) {
            lastThirstGrowthTime = DateTime.Now;

            if ((hunger + hungerGrowthAmount) <= 100) {
                hunger += hungerGrowthAmount;
            } else {
                hunger = 100;
            }
        }
    }

    /// <summary>
    /// Decrease thirst.
    /// </summary>
    public void DecreaseThirst(int decreaseThirstAmount) {
        if (lastThirstGrowthTime.AddSeconds(secondsUntilThirstGrowth) <= DateTime.Now) {
            lastThirstGrowthTime = DateTime.Now;

            if (0 <= (thirst - decreaseThirstAmount)) {
                thirst -= decreaseThirstAmount;
            } else {
                thirst = 0;
            }
        }
    }

    /// <summary>
    /// Decrease hunger.
    /// </summary>
    public void DecreaseHunger(int decreaseHungerAmount) {
        if (lastHungerGrowthTime.AddSeconds(secondsUntilHungerGrowth) <= DateTime.Now) {
            lastThirstGrowthTime = DateTime.Now;

            if (0 <= (hunger - decreaseHungerAmount)) {
                hunger -= decreaseHungerAmount;
            } else {
                hunger = 0;
            }
        }
    }

    /// <summary>
    /// Set destination structure and path if not busy.
    /// </summary>
    private void SetDestinationStructureAndPathIfNotBusy() {
        if (!isBusy) {
            SetDestinationStructure();
            SetPath();

            isBusy = true;
        }
    }

    /// <summary>
    /// Set destination structure.
    /// </summary>
    private void SetDestinationStructure() {
        if (maximumThirstOfNotNeedingToDrink <= thirst) {
            destinationStructure = worldManager.getRandomBar();
        } else if (maximumHungerOfNotNeedingToEat <= hunger) {
            destinationStructure = worldManager.GetRandomRestaurant();
        } else {
            destinationStructure = worldManager.getRandomAttraction();
        }
    }

    /// <summary>
    /// Set path.
    /// </summary>
    private void SetPath() {
        List<Road> orthogonallyAdjacentRoadsAroundDestinationStructure = worldManager.GetOrthogonallyAdjecentRoadsAroundStructure((Structure)destinationStructure);

        path = null;
        foreach (Road orthogonallyAdjacentRoadAroundDestinationStructure in orthogonallyAdjacentRoadsAroundDestinationStructure) {
            path = PathFinder.FindPath(worldManager, worldManager.GetFieldAtPosition(GetPositionRoundedToVector3Int()), orthogonallyAdjacentRoadAroundDestinationStructure, true);

            if (0 < path.Capacity) {
                break;
            }
        }
    }

    /// <summary>
    /// Get position rounded to Vector3Int.
    /// </summary>
    private Vector3Int GetPositionRoundedToVector3Int() {
        return Vector3Int.RoundToInt(gameObject.transform.position);
    }

    /// <summary>
    /// Move game object to destination if not already there.
    /// </summary>
    private void MoveGameObjectToDestinationIfNotAlreadyThere() {
        if (path.Count != 0) {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, path[path.Count - 1].GetOrigoPosition(), GetMovementSpeed() * Time.deltaTime);

            RotateGameObjectTowardsMovementDirection(path[path.Count - 1].GetOrigoPosition());

            if (Vector3.Distance(gameObject.transform.position, path[path.Count - 1].GetOrigoPosition()) <= distancePrecision) {
                path.RemoveAt(path.Count - 1);
            }
        }
    }

    /// <summary>
    /// Enter destination structure.
    /// </summary>
    private void EnterDestinationStructure() {
        if (path.Count == 0 && isBusy) {
            Structure structure = (Structure)destinationStructure;
            structure.Action(this);

            if (Math.Abs(gameObject.transform.position.y - originalHeight) <= distancePrecision) {
                SetIsVisible(false);
            }
        }
    }

    /// <summary>
    /// Rotate game object towards movement direction.
    /// </summary>
    private void RotateGameObjectTowardsMovementDirection(Vector3 target) {
        Vector3 targetDirection = target - gameObject.transform.position;

        Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection, rotationSpeedMultiplier * GetMovementSpeed() * Time.deltaTime, 0.0f);

        gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
    }


    /// <summary>
    /// Get movement speed.
    /// </summary>
    private float GetMovementSpeed() {
        double movementSpeed = random.NextDouble() * (maximumSpeed - minimumSpeed) + minimumSpeed;
        return (float)movementSpeed;
    }

    /// <summary>
    /// Get satisfaction.
    /// </summary>
    public int GetSatisfaction() {
        return satisfaction;
    }

    /// <summary>
    /// Destroy game object.
    /// </summary>
    private void DestroyGameObjectIfSatisfactionIsLowerThanMinimumSatisfactionOfstaying() {
        if (satisfaction < minimumSatisfactionOfStaying) {
            GameObject.Destroy(gameObject);
        }
    }

    /// <summary>
    /// Set is busy.
    /// </summary>
    public void SetIsBusy(bool isBusy) {
        this.isBusy = isBusy;
    }

    /// <summary>
    /// Set is visible.
    /// </summary>
    public void SetIsVisible(bool isVisible) {
        if (isVisible) {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, loweringDistanceOnInvisibility, 0);
        } else {
            gameObject.transform.position = gameObject.transform.position - new Vector3(0, loweringDistanceOnInvisibility, 0);
            Debug.Log("doublefuck" + gameObject.transform.position);
        }

        if (gameObject.transform.position.y > 10) {
            Debug.Log("fuck" + gameObject.transform.position);
        }
    }

    /// <summary>
    /// Increase satisfaction.
    /// </summary>
    public void IncreaseOrDecreaseSatisfaction(int satisfactionIncreaseOrDecreaseAmount) {
        if (0 <= (satisfaction + satisfactionIncreaseOrDecreaseAmount) && (satisfaction + satisfactionIncreaseOrDecreaseAmount) <= 100) {
            satisfaction += satisfactionIncreaseOrDecreaseAmount;
        } else if ((satisfaction + satisfactionIncreaseOrDecreaseAmount) < 0) {
            satisfaction = 0;
        } else if (100 < (satisfaction + satisfactionIncreaseOrDecreaseAmount)) {
            satisfaction = 100;
        }
    }

    /// <summary>
    /// Increase moneyOwed.
    /// </summary>
    public void IncreaseOrDecreaseMoneyOwed(float moneyOwedIncreaseOrDecreaseAmount) {
        moneyOwed += moneyOwedIncreaseOrDecreaseAmount;
    }

    /// <summary>
    /// Get moneyOwed.
    /// </summary>
    public float GetMoneyOwed() {
        return moneyOwed;
    }
}
