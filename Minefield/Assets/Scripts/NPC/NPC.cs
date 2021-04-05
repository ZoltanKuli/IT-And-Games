using System.Collections.Generic;
using UnityEngine;

public class NPC {

    private GameObject gameObject;

    private WorldManager worldManager;

    private float distancePrecision;

    private bool isBusy;

    private int satisfaction;
    private int minimumSatisfactionOfStaying;
    private int thirst;
    private int maximumThirstOfNotNeedingToDrink;
    private int hunger;
    private int maximumHungerOfNotNeedingToEat;

    private Field destinationStructure;
    private List<Field> path;
    private float minimumSpeed;
    private float maximumSpeed;
    private float rotationSpeedMultiplier;

    private static System.Random random;

    static NPC() {
        random = new System.Random();
    }

    public NPC(GameObject prefab, Vector3Int position, WorldManager worldManager, float distancePrecision,
        int defaultSatisfaction, int minimumSatisfactionOfStaying, int defaultThirst, int maximumThirstOfNotNeedingToDrink,
        int defaultHunger, int maximumHungerOfNotNeedingToEat, float minimumSpeed, float maximumSpeed, float rotationSpeedMultiplier) {
        gameObject = GameObject.Instantiate(prefab, position, Quaternion.identity);

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
    }

    /// <summary>
    /// Update.
    /// </summary>
    public void Update() {
        SetDestinationStructureAndPathIfNotBusy();
        MoveGameObjectToDestinationIfNotAlreadyThere();

        DestroyGameObjectIfSatisfactionIsLowerThanMinimumSatisfactionOfstaying();
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
}
