using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    private Field[,] worldMatrix;
    [SerializeField]
    private int width;
    [SerializeField]
    private int length;

    [SerializeField]
    private GameObject straightRoadPrefab;
    [SerializeField]
    private GameObject cornerRoadPrefab;
    [SerializeField]
    private GameObject threeWayRoadPrefab;
    [SerializeField]
    private GameObject fourWayRoadPrefab;

    public void Start() {
        InitializeFieldWorldMatrix();
    }

    private void InitializeFieldWorldMatrix() {
        worldMatrix = new Field[width, length];

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                worldMatrix[i, j] = new EmptyField(new Vector3Int(i, 0, j));
            }
        }
    }

    public void PlaceDownARoad(Vector3Int origoPosition) {
        if (isOrigoPositionInBounds(origoPosition) && worldMatrix[origoPosition.x, origoPosition.z] is EmptyField) {
            List<Field> adjacentFields = GetAdjecentFields(origoPosition);

            List<Field> adjacentNonRoadFields = adjacentFields.FindAll(field => !(field is Road));
            List<Road> adjacentRoads = adjacentFields.FindAll(field => field is Road).ConvertAll(field => (Road)field);

            int numberOfAdjacentNonRoadFields = adjacentNonRoadFields.Count;

            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsFour(origoPosition, numberOfAdjacentNonRoadFields);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsThree(origoPosition, numberOfAdjacentNonRoadFields,
                adjacentFields, adjacentRoads);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentRoads);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsOne(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentNonRoadFields);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsZero(origoPosition, numberOfAdjacentNonRoadFields);

            adjustAdjacentRoads(origoPosition);
        }
    }

    private bool isOrigoPositionInBounds(Vector3Int origoPosition) {
        return 0 <= origoPosition.x && origoPosition.x < width
            && 0 <= origoPosition.z && origoPosition.z < length;
    }

    private void adjustAdjacentRoads(Vector3Int origoPosition) {
        adjustRoad(new Vector3Int(origoPosition.x - 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z + 1));
        adjustRoad(new Vector3Int(origoPosition.x + 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z - 1));
    }

    private void adjustRoad(Vector3Int origoPosition) {
        if (isOrigoPositionInBounds(origoPosition) && worldMatrix[origoPosition.x, origoPosition.z] is Road) {
            List<Field> adjacentFields = GetAdjecentFields(origoPosition);

            List<Field> adjacentNonRoadFields = adjacentFields.FindAll(field => !(field is Road));
            List<Road> adjacentRoads = adjacentFields.FindAll(field => field is Road).ConvertAll(field => (Road)field);

            int numberOfAdjacentNonRoadFields = adjacentNonRoadFields.Count;

            Road currentRoad = (Road)worldMatrix[origoPosition.x, origoPosition.z];
            currentRoad.DestroyGameObject();

            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsFour(origoPosition, numberOfAdjacentNonRoadFields);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsThree(origoPosition, numberOfAdjacentNonRoadFields,
                adjacentFields, adjacentRoads);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentRoads);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsOne(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentNonRoadFields);
            PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsZero(origoPosition, numberOfAdjacentNonRoadFields);
        }
    }

    private void PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsFour(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields) {
        if (numberOfAdjacentNonRoadFields == 4) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, 0);
        }
    }

    private void PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsThree(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields,
        List<Field> adjacentFields, List<Road> adjacentRoads) {
        if (numberOfAdjacentNonRoadFields == 3) {
            int yAngleMultiplier = adjacentFields.IndexOf(adjacentRoads[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsTwo(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields,
        List<Field> adjacentFields, List<Road> adjacentRoads) {
        if (numberOfAdjacentNonRoadFields == 2) {
            int indexOfFirstAdjacentRoad = adjacentFields.IndexOf(adjacentRoads[0]);
            int indexOfSecondAdjacentRoad = adjacentFields.IndexOf(adjacentRoads[1]);

            if ((indexOfFirstAdjacentRoad == 0 && indexOfSecondAdjacentRoad == 2)
                || (indexOfFirstAdjacentRoad == 1 && indexOfSecondAdjacentRoad == 3)) {
                worldMatrix[origoPosition.x, origoPosition.z] = new StraightRoad(straightRoadPrefab, origoPosition, indexOfFirstAdjacentRoad * 90);
            } else {
                int yAngleMultiplier = 0;

                if (indexOfFirstAdjacentRoad == 1 && indexOfSecondAdjacentRoad == 2) {
                    yAngleMultiplier = 1;
                } else if (indexOfFirstAdjacentRoad == 2 && indexOfSecondAdjacentRoad == 3) {
                    yAngleMultiplier = 2;
                } else if (indexOfFirstAdjacentRoad == 0 && indexOfSecondAdjacentRoad == 3) {
                    yAngleMultiplier = 3;
                }

                worldMatrix[origoPosition.x, origoPosition.z] =
                    new CornerRoad(cornerRoadPrefab, origoPosition, yAngleMultiplier * 90);
            }
        }
    }

    private void PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsOne(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields,
        List<Field> adjacentFields, List<Field> adjacentNonRoadFields) {
        if (numberOfAdjacentNonRoadFields == 1) {
            int yAngleMultiplier = adjacentFields.IndexOf(adjacentNonRoadFields[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new ThreeWayRoad(threeWayRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void PlaceDownARoadIfNumberOfAdjacentNonRoadFieldsIsZero(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields) {
        if (numberOfAdjacentNonRoadFields == 0) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new FourWayRoad(fourWayRoadPrefab, origoPosition, 0);
        }
    }

    public List<Field> GetAdjecentFields(Vector3Int origoPosition) {
        List<Field> adjacentFields = new List<Field>();

        if (0 < origoPosition.x) {
            adjacentFields.Add(worldMatrix[origoPosition.x - 1, origoPosition.z]);
        } else {
            adjacentFields.Add(null);
        }

        if (origoPosition.z < length - 1) {
            adjacentFields.Add(worldMatrix[origoPosition.x, origoPosition.z + 1]);
        } else {
            adjacentFields.Add(null);
        }

        if (origoPosition.x < width - 1) {
            adjacentFields.Add(worldMatrix[origoPosition.x + 1, origoPosition.z]);
        } else {
            adjacentFields.Add(null);
        }

        if (0 < origoPosition.z) {
            adjacentFields.Add(worldMatrix[origoPosition.x, origoPosition.z - 1]);
        } else {
            adjacentFields.Add(null);
        }

        return adjacentFields;
    }

    internal IEnumerable<Field> GetWalkableAdjacentFields(Field currentField, bool isAIAgent) {
        throw new NotImplementedException();
    }

    internal float GetCostOfEnteringField(Field adjacentField) {
        throw new NotImplementedException();
    }
}
