using System;
using System.Collections.Generic;
using UnityEngine;

public class NPC {

    private GameObject gameObject;
    private Vector3 entrancePosition;
    private float loweringDistanceOnInvisibility;

    private WorldManager worldManager;

    private float distancePrecision;

    private bool isBusy;
    private bool isVisible;
    private int moneyOwed;

    private int satisfaction;
    private int minimumSatisfactionOfStaying;
    private int thirst;
    private int maximumThirstOfNotNeedingToDrink;
    private int hunger;
    private int maximumHungerOfNotNeedingToEat;

    private int secondsUntilThirstGrowth;
    private DateTime timeOfNextThirstGrowth;
    private int thirstGrowthAmount;
    private int secondsUntilHungerGrowth;
    private DateTime timeOfNextHungertGrowth;
    private int hungerGrowthAmount;
    private int thirstDecreaseDissatisfactionAmount;
    private int hungerDecreaseDissatisfactionAmount;

    private Field destinationStructure;
    private List<Field> path;
    private float minimumSpeed;
    private float maximumSpeed;
    private float speed;
    private float rotationSpeedMultiplier;

    private bool hasGarbage;
    private DateTime timeOfGarbageDropping;
    private int minimumSecondsUntilGarbageDisposal;
    private int maximumSecondsUntilGarbageDisposal;
    private int garbageDecreaseDissatisfactionAmount;

    private System.Random random;

    public NPC(GameObject prefab, Vector3Int entrancePosition, WorldManager worldManager, float distancePrecision,
        int defaultSatisfaction, int minimumSatisfactionOfStaying, int defaultThirst, int maximumThirstOfNotNeedingToDrink,
        int defaultHunger, int maximumHungerOfNotNeedingToEat, float minimumSpeed, float maximumSpeed, float rotationSpeedMultiplier,
        int secondsUntilThirstGrowth, int thirstGrowthAmount, int secondsUntilHungerGrowth, int hungerGrowthAmount,
        float loweringDistanceOnInvisibility, int thirstDecreaseDissatisfactionAmount, int hungerDecreaseDissatisfactionAmount, 
        int minimumSecondsUntilGarbageDisposal, int maximumSecondsUntilGarbageDisposal, int garbageDecreaseDissatisfactionAmount) {
        gameObject = GameObject.Instantiate(prefab, entrancePosition, Quaternion.identity);
        this.entrancePosition = entrancePosition;
        isVisible = true;
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

        this.secondsUntilThirstGrowth = secondsUntilThirstGrowth;
        timeOfNextThirstGrowth = DateTime.UtcNow.AddSeconds(secondsUntilThirstGrowth);
        this.thirstGrowthAmount = thirstGrowthAmount;
        this.secondsUntilHungerGrowth = secondsUntilHungerGrowth;
        timeOfNextHungertGrowth = DateTime.UtcNow.AddSeconds(secondsUntilHungerGrowth);
        this.hungerGrowthAmount = hungerGrowthAmount;

        this.loweringDistanceOnInvisibility = loweringDistanceOnInvisibility;

        this.thirstDecreaseDissatisfactionAmount = thirstDecreaseDissatisfactionAmount;
        this.hungerDecreaseDissatisfactionAmount = hungerDecreaseDissatisfactionAmount;

        hasGarbage = false;
        this.minimumSecondsUntilGarbageDisposal = minimumSecondsUntilGarbageDisposal;
        this.maximumSecondsUntilGarbageDisposal = maximumSecondsUntilGarbageDisposal;
        this.garbageDecreaseDissatisfactionAmount = garbageDecreaseDissatisfactionAmount;

        random = new System.Random(GetHashCode());
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
        if (distancePrecision < Vector3.Distance(gameObject.transform.position, entrancePosition)) {
            IncreaseThirst();
            IncreaseHunger();
        } else {
            timeOfNextThirstGrowth = DateTime.UtcNow.AddSeconds(secondsUntilThirstGrowth);
            timeOfNextHungertGrowth = DateTime.UtcNow.AddSeconds(secondsUntilHungerGrowth);
        }
    }

    /// <summary>
    /// Increase thirst.
    /// </summary>
    private void IncreaseThirst() {
        if (timeOfNextThirstGrowth <= DateTime.UtcNow) {
            timeOfNextThirstGrowth = DateTime.UtcNow.AddSeconds(secondsUntilThirstGrowth);

            if ((thirst + thirstGrowthAmount) <= 100) {
                thirst += thirstGrowthAmount;
            } else {
                thirst = 100;
            }

            IncreaseOrDecreaseSatisfaction(-thirstDecreaseDissatisfactionAmount);
        }
    }

    /// <summary>
    /// Increase hunger.
    /// </summary>
    private void IncreaseHunger() {
        if (timeOfNextHungertGrowth <= DateTime.UtcNow) {
            timeOfNextHungertGrowth = DateTime.UtcNow.AddSeconds(secondsUntilHungerGrowth);

            if ((hunger + hungerGrowthAmount) <= 100) {
                hunger += hungerGrowthAmount;
            } else {
                hunger = 100;
            }

            IncreaseOrDecreaseSatisfaction(-hungerDecreaseDissatisfactionAmount);
        }
    }

    /// <summary>
    /// Decrease thirst.
    /// </summary>
    public void DecreaseThirst(int decreaseThirstAmount) {
        if (0 <= (thirst - decreaseThirstAmount)) {
            thirst -= decreaseThirstAmount;
        } else {
            thirst = 0;
        }
    }

    /// <summary>
    /// Decrease hunger.
    /// </summary>
    public void DecreaseHunger(int decreaseHungerAmount) {
        if (0 <= (hunger - decreaseHungerAmount)) {
            hunger -= decreaseHungerAmount;
        } else {
            hunger = 0;
        }
    }

    /// <summary>
    /// Set destination structure and path if not busy.
    /// </summary>
    private void SetDestinationStructureAndPathIfNotBusy() {
        if (!isBusy) {
            SetDestinationStructure();
            TeleportBackToSpawnIfNotStandingOnARoadOrTheEntrance();
            SetPath();
        }
    }

    /// <summary>
    /// Set destination structure.
    /// </summary>
    private void SetDestinationStructure() {
        Field tmpDestinationStructure = destinationStructure;
        int structuresCount = worldManager.GetStructuresCount();

        for (int i = 0; i < structuresCount && destinationStructure == tmpDestinationStructure; i++) {
            if (maximumThirstOfNotNeedingToDrink <= thirst) {
                tmpDestinationStructure = worldManager.GetRandomBar();
            } else if (maximumHungerOfNotNeedingToEat <= hunger) {
                tmpDestinationStructure = worldManager.GetRandomRestaurant();
            } else {
                tmpDestinationStructure = worldManager.GetRandomAttraction();
            }
        }

        destinationStructure = tmpDestinationStructure;
    }

    /// <summary>
    /// Set path.
    /// </summary>
    private void SetPath() {
        List<Road> orthogonallyAdjacentRoadsAroundDestinationStructure = worldManager.GetOrthogonallyAdjecentRoadsAroundStructure((Structure)destinationStructure);

        path = new List<Field>();
        while (orthogonallyAdjacentRoadsAroundDestinationStructure.Count != 0) {
            Road orthogonallyAdjacentRoadAroundDestinationStructure = orthogonallyAdjacentRoadsAroundDestinationStructure[random.Next(orthogonallyAdjacentRoadsAroundDestinationStructure.Count)];
            orthogonallyAdjacentRoadsAroundDestinationStructure.Remove(orthogonallyAdjacentRoadAroundDestinationStructure);

            path = PathFinder.FindPath(worldManager, worldManager.GetFieldAtPosition(GetPositionRoundedToVector3Int()), orthogonallyAdjacentRoadAroundDestinationStructure, true);

            if (0 < path.Capacity) {
                isBusy = true;
                speed = GetMovementSpeed();
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
            if ((path[path.Count - 1] != worldManager.GetFieldAtPosition(path[path.Count - 1].GetOrigoPosition()))
                || destinationStructure != worldManager.GetFieldAtPosition(destinationStructure.GetOrigoPosition())) {
                path = new List<Field>();
                isBusy = false;
                return;
            }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, path[path.Count - 1].GetOrigoPosition(), speed * Time.deltaTime);
            RotateGameObjectTowardsMovementDirection(path[path.Count - 1].GetOrigoPosition());

            if (path[path.Count - 1] is Road) {
                DisposeOfGarbage((Road)path[path.Count - 1]);
            }

            if (Vector3.Distance(gameObject.transform.position, path[path.Count - 1].GetOrigoPosition()) <= distancePrecision) {
                CallUpdateOnParksIfOrthogonallyAdjacent(path[path.Count - 1].GetOrigoPosition());

                if (path[path.Count - 1] is Road) {
                    DecreaseSatisfactionIfThereIsGarbageOnTheRoad((Road)path[path.Count - 1]);
                }

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
            structure.Update(this);
        }
    }

    /// <summary>
    /// Rotate game object towards movement direction.
    /// </summary>
    private void RotateGameObjectTowardsMovementDirection(Vector3 target) {
        Vector3 targetDirection = target - gameObject.transform.position;

        Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection, rotationSpeedMultiplier * speed * Time.deltaTime, 0.0f);

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
    /// Get thirst.
    /// </summary>
    public int GetThirst() {
        return thirst;
    }

    /// <summary>
    /// Get hunger.
    /// </summary>
    public int GetHunger() {
        return hunger;
    }

    /// <summary>
    /// Destroy game object.
    /// </summary>
    private void DestroyGameObjectIfSatisfactionIsLowerThanMinimumSatisfactionOfstaying() {
        if (isVisible && satisfaction < minimumSatisfactionOfStaying) {
            path = new List<Field>();
            isBusy = false;
            GameObject.Destroy(gameObject);
            gameObject = null;
        }
    }

    /// <summary>
    /// Is destroyed.
    /// </summary>
    public bool IsDestroyed() {
        return gameObject == null;
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
            TeleportBackToSpawnIfNotStandingOnARoadOrTheEntrance();
        } else {
            gameObject.transform.position = gameObject.transform.position - new Vector3(0, loweringDistanceOnInvisibility, 0);
        }

        this.isVisible = isVisible;
    }

    /// <summary>
    /// Teleport back to spawn if not standing on a road ar the entrance.
    /// </summary>
    private void TeleportBackToSpawnIfNotStandingOnARoadOrTheEntrance() {
        if (!(worldManager.GetFieldAtPosition(Vector3Int.RoundToInt(gameObject.transform.position)) is Road)
            && !(worldManager.GetFieldAtPosition(Vector3Int.RoundToInt(gameObject.transform.position)) is Entrance)) {
            gameObject.transform.position = new Vector3(entrancePosition.x, entrancePosition.y, entrancePosition.z);
            path = new List<Field>();
            isBusy = false;
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
    public void IncreaseOrDecreaseMoneyOwed(int moneyOwedIncreaseOrDecreaseAmount) {
        moneyOwed += moneyOwedIncreaseOrDecreaseAmount;
    }

    /// <summary>
    /// Get moneyOwed.
    /// </summary>
    public int GetMoneyOwed() {
        return moneyOwed;
    }

    /// <summary>
    /// Call update on parks if orthogonally adjacent.
    /// </summary>
    private void CallUpdateOnParksIfOrthogonallyAdjacent(Vector3Int origoPosition) {
        List<Field> orthogonallyAdjecentFields = worldManager.GetOrthogonallyAdjecentFields(origoPosition);

        foreach(Field orthogonallyAdjecentField in orthogonallyAdjecentFields) {
            if (orthogonallyAdjecentField is Park) {
                Park orthogonallyAdjecentPark = (Park)orthogonallyAdjecentField;
                orthogonallyAdjecentPark.Update(this);
            }
        }
    }

    /// <summary>
    /// Add garbage.
    /// </summary>
    public void AddGarbage() {
        hasGarbage = true;
        timeOfGarbageDropping = DateTime.Now.AddSeconds(random.Next(minimumSecondsUntilGarbageDisposal, maximumSecondsUntilGarbageDisposal)); ;
    }

    /// <summary>
    /// Dispose of garbage.
    /// </summary>
    private void DisposeOfGarbage(Road road) {
        if (hasGarbage && timeOfGarbageDropping <= DateTime.Now) {
            GarbageCan garbageCan = GetOrthogonallyAdjacentGarbageCanIfThereIsAny(road.GetOrigoPosition());

            if (garbageCan == null) {
                road.PlaceAGarbageGameObjectOnTheRoadAtARandomLocation();
            }

            hasGarbage = false;
        }
    }

    /// <summary>
    /// Get orthogonally adjacent garbage can if there is any.
    /// </summary>
    private GarbageCan GetOrthogonallyAdjacentGarbageCanIfThereIsAny(Vector3Int origoPosition) {
        List<Field> orthogonallyAdjecentFields = worldManager.GetOrthogonallyAdjecentFields(origoPosition);

        foreach (Field orthogonallyAdjecentField in orthogonallyAdjecentFields) {
            if (orthogonallyAdjecentField is GarbageCan) {
                return (GarbageCan)orthogonallyAdjecentField;
            }
        }

        return null;
    }

    /// <summary>
    /// Decrease satisfaction if there is garbage on the road.
    /// </summary>
    private void DecreaseSatisfactionIfThereIsGarbageOnTheRoad(Road road) {
        if (road.IsLitteredWithGarbage()) {
            IncreaseOrDecreaseSatisfaction(-garbageDecreaseDissatisfactionAmount);
        }
    }
}
