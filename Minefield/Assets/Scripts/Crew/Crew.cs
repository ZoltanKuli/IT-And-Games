using System;
using System.Collections.Generic;
using UnityEngine;

public class Crew {

    protected WorldManager worldManager;

    protected GameObject gameObject;
    protected CrewStation crewStation;

    protected bool isBusy;

    protected Field fieldToDoActionOn;
    protected List<Field> path;
    protected float speed;
    protected float rotationSpeedMultiplier;
    protected float distancePrecision;
    protected int maximumTravelDistance;

    protected int secondsUntilActionIsFinished;
    protected DateTime timeOfFinishingAction;

    protected System.Random random;

    public Crew(GameObject crewPrefab, Vector3Int crewStationPosition,
        float speed, int secondsUntilActionIsFinished, WorldManager worldManager, 
        float distancePrecision, float rotationSpeedMultiplier,
        int maximumTravelDistance) {
        this.worldManager = worldManager;

        gameObject = GameObject.Instantiate(crewPrefab, crewStationPosition, Quaternion.identity);

        isBusy = false;

        fieldToDoActionOn = null;
        path = new List<Field>();
        this.speed = speed;
        this.rotationSpeedMultiplier = rotationSpeedMultiplier;
        this.distancePrecision = distancePrecision;
        this.maximumTravelDistance = maximumTravelDistance;

        this.secondsUntilActionIsFinished = secondsUntilActionIsFinished;

        random = new System.Random(GetHashCode());
    }

    /// <summary>
    /// Set crew station.
    /// </summary>
    public void SetCrewStation(CrewStation crewStation) {
        this.crewStation = crewStation;
    }

    /// <summary>
    /// Has field to do action on.
    /// </summary>
    public bool HasFieldToDoActionOn() {
        return isBusy || fieldToDoActionOn != null || (fieldToDoActionOn == crewStation && worldManager.GetFieldAtPosition(Vector3Int.RoundToInt(gameObject.transform.position)) != crewStation);
    }

    /// <summary>
    /// Update.
    /// </summary>
    public void Update() {
        MoveGameObjectToTheFieldToDoActionOnIfNotAlreadyThere();

        TeleportBackToSpawnIfNotStandingOnARoadOrItsCrewStation();

        DoAction();
    }

    /// <summary>
    /// Do action.
    /// </summary>
    protected virtual void DoAction() { 
    }

    /// <summary>
    /// Move game object to the field to do action on if not already there.
    /// </summary>
    protected virtual void MoveGameObjectToTheFieldToDoActionOnIfNotAlreadyThere() {
        if (path.Count != 0) {
            if ((path[path.Count - 1] != worldManager.GetFieldAtPosition(path[path.Count - 1].GetOrigoPosition()))
                || fieldToDoActionOn != worldManager.GetFieldAtPosition(fieldToDoActionOn.GetOrigoPosition())) {
                path = new List<Field>();
                ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn();
                return;
            }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, path[path.Count - 1].GetOrigoPosition(), speed * Time.deltaTime);
            RotateGameObjectTowardsMovementDirection(path[path.Count - 1].GetOrigoPosition());

            if (Vector3.Distance(gameObject.transform.position, path[path.Count - 1].GetOrigoPosition()) <= distancePrecision) {
                path.RemoveAt(path.Count - 1);
            }

            if (path.Count == 0 && fieldToDoActionOn != crewStation) {
                isBusy = true;
                timeOfFinishingAction = DateTime.UtcNow.AddSeconds(secondsUntilActionIsFinished);
            }
        } else if (fieldToDoActionOn == crewStation) {
            fieldToDoActionOn = null;
        }
    }

    /// <summary>
    /// Rotate game object towards movement direction.
    /// </summary>
    protected void RotateGameObjectTowardsMovementDirection(Vector3 target) {
        Vector3 targetDirection = target - gameObject.transform.position;

        Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection, rotationSpeedMultiplier * speed * Time.deltaTime, 0.0f);

        gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    /// <summary>
    /// Set field to do action on.
    /// </summary>
    public void SetFieldToDoActionOn(Field fieldToDoActionOn) {
        this.fieldToDoActionOn = fieldToDoActionOn;
        TeleportBackToSpawnIfNotStandingOnARoadOrItsCrewStation();
        SetPath();
    }

    /// <summary>
    /// Teleport back to spawn if not standing on a road or its crew station.
    /// </summary>
    protected void TeleportBackToSpawnIfNotStandingOnARoadOrItsCrewStation() {
        if (!(worldManager.GetFieldAtPosition(Vector3Int.RoundToInt(gameObject.transform.position)) is Road)
            && (worldManager.GetFieldAtPosition(Vector3Int.RoundToInt(gameObject.transform.position)) != crewStation)) {
            gameObject.transform.position = new Vector3(crewStation.GetOrigoPosition().x, crewStation.GetOrigoPosition().y, crewStation.GetOrigoPosition().z);
            path = new List<Field>();
            ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn();
        }
    }

    /// <summary>
    /// Set path.
    /// </summary>
    protected virtual void SetPath() {
        List<Road> orthogonallyAdjacentRoadsAroundDestinationStructure = worldManager.GetOrthogonallyAdjecentRoadsAroundStructure((Structure)fieldToDoActionOn);

        path = new List<Field>();
        while (orthogonallyAdjacentRoadsAroundDestinationStructure.Count != 0) {
            Road orthogonallyAdjacentRoadAroundDestinationStructure = orthogonallyAdjacentRoadsAroundDestinationStructure[random.Next(orthogonallyAdjacentRoadsAroundDestinationStructure.Count)];
            orthogonallyAdjacentRoadsAroundDestinationStructure.Remove(orthogonallyAdjacentRoadAroundDestinationStructure);

            path = PathFinder.FindPath(worldManager, worldManager.GetFieldAtPosition(GetPositionRoundedToVector3Int()), orthogonallyAdjacentRoadAroundDestinationStructure, false);

            if (0 < path.Count && (path.Count <= maximumTravelDistance || fieldToDoActionOn == crewStation)) {
                break;
            } else {
                path = new List<Field>();
            }
        }

        if (path.Count == 0) {
            ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn();
        }
    }

    /// <summary>
    /// Get position rounded to Vector3Int.
    /// </summary>
    protected Vector3Int GetPositionRoundedToVector3Int() {
        return Vector3Int.RoundToInt(gameObject.transform.position);
    }

    /// <summary>
    /// Destroy game object.
    /// </summary>
    public virtual void DestroyGameObject() {
        ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn();
        GameObject.Destroy(gameObject);
        gameObject = null;
    }

    /// <summary>
    /// Readd field to do action on to world manager if field is not destroyed and reset field to do action on.
    /// </summary>
    protected virtual void ReaddFieldToDoActionOnToWorldManagerIfFieldIsNotDestroyedAndResetFieldToDoActionOn() {
        fieldToDoActionOn = crewStation;
        SetPath();
    }
}
