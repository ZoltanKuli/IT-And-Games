using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    private Field[,] worldMatrix;
    [SerializeField]
    private int worldMatrixWidth;
    [SerializeField]
    private int worldMatrixLength;

    [SerializeField]
    private GameObject straightRoadPrefab;
    [SerializeField]
    private GameObject cornerRoadPrefab;
    [SerializeField]
    private GameObject threeWayRoadPrefab;
    [SerializeField]
    private GameObject fourWayRoadPrefab;

    [SerializeField]
    private GameObject structurePrefab;


    [SerializeField]
    private LayerMask natureMask;

    public void Start() {
        InitializeFieldWorldMatrix();
        /*PlaceDownNewRoad(new Vector3Int(0, 0, 1));
        PlaceDownNewRoad(new Vector3Int(0, 0, 2));
        PlaceDownNewRoad(new Vector3Int(0, 0, 3));
        PlaceDownNewRoad(new Vector3Int(0, 0, 4));
        PlaceDownNewRoad(new Vector3Int(0, 0, 5));
        PlaceDownNewRoad(new Vector3Int(0, 0, 6));

        PlaceDownNewRoad(new Vector3Int(1, 0, 1));
        PlaceDownNewRoad(new Vector3Int(1, 0, 2));
        PlaceDownNewRoad(new Vector3Int(1, 0, 3));
        PlaceDownNewRoad(new Vector3Int(1, 0, 4));
        PlaceDownNewRoad(new Vector3Int(1, 0, 5));
        PlaceDownNewRoad(new Vector3Int(1, 0, 6));*/
    }

    private void InitializeFieldWorldMatrix() {
        worldMatrix = new Field[worldMatrixWidth, worldMatrixLength];

        for (int i = 0; i < worldMatrixWidth; i++) {
            for (int j = 0; j < worldMatrixLength; j++) {
                worldMatrix[i, j] = new EmptyField(new Vector3Int(i, 0, j));
            }
        }
    }

    public void PlaceDownNewRoad(Vector3Int origoPosition) {
        if (isFieldInBounds(origoPosition)
            && worldMatrix[origoPosition.x, origoPosition.z] is EmptyField) {
            List<Field> adjacentFields = GetAdjecentFields(origoPosition);

            List<Field> adjacentNonRoadFields = adjacentFields.FindAll(field => !(field is Road));
            List<Road> adjacentRoads = adjacentFields.FindAll(field => field is Road).ConvertAll(field => (Road)field);

            int numberOfAdjacentNonRoadFields = adjacentNonRoadFields.Count;

            SetNatureGameObjectsVisibility(origoPosition, false);

            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsFour(origoPosition, numberOfAdjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsThree(origoPosition, numberOfAdjacentNonRoadFields,
                adjacentFields, adjacentRoads);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentRoads);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsOne(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsZero(origoPosition, numberOfAdjacentNonRoadFields);

            adjustAdjacentRoads(origoPosition);
        }
    }

    private void adjustAdjacentRoads(Vector3Int origoPosition) {
        adjustRoad(new Vector3Int(origoPosition.x - 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z + 1));
        adjustRoad(new Vector3Int(origoPosition.x + 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z - 1));
    }

    private void adjustRoad(Vector3Int origoPosition) {
        if (isFieldInBounds(origoPosition)
            && worldMatrix[origoPosition.x, origoPosition.z] is Road) {
            List<Field> adjacentFields = GetAdjecentFields(origoPosition);

            List<Field> adjacentNonRoadFields = adjacentFields.FindAll(field => !(field is Road));
            List<Road> adjacentRoads = adjacentFields.FindAll(field => field is Road).ConvertAll(field => (Road)field);

            int numberOfAdjacentNonRoadFields = adjacentNonRoadFields.Count;

            Road currentRoad = (Road)worldMatrix[origoPosition.x, origoPosition.z];
            currentRoad.DestroyGameObject();

            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsFour(origoPosition, numberOfAdjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsThree(origoPosition, numberOfAdjacentNonRoadFields,
                adjacentFields, adjacentRoads);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentRoads);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsOne(origoPosition, numberOfAdjacentNonRoadFields,
               adjacentFields, adjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsZero(origoPosition, numberOfAdjacentNonRoadFields);
        }
    }

    private void PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsFour(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields) {
        if (numberOfAdjacentNonRoadFields == 4) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, 0);
        }
    }

    private void PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsThree(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields,
        List<Field> adjacentFields, List<Road> adjacentRoads) {
        if (numberOfAdjacentNonRoadFields == 3) {
            int yAngleMultiplier = adjacentFields.IndexOf(adjacentRoads[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsTwo(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields,
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

    private void PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsOne(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields,
        List<Field> adjacentFields, List<Field> adjacentNonRoadFields) {
        if (numberOfAdjacentNonRoadFields == 1) {
            int yAngleMultiplier = adjacentFields.IndexOf(adjacentNonRoadFields[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new ThreeWayRoad(threeWayRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void PlaceDownNewRoadIfNumberOfAdjacentNonRoadFieldsIsZero(Vector3Int origoPosition, int numberOfAdjacentNonRoadFields) {
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

        if (origoPosition.z < worldMatrixLength - 1) {
            adjacentFields.Add(worldMatrix[origoPosition.x, origoPosition.z + 1]);
        } else {
            adjacentFields.Add(null);
        }

        if (origoPosition.x < worldMatrixWidth - 1) {
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

    public void DestroyRoad(Vector3Int origoPosition) {
        if (isFieldInBounds(origoPosition)
            && worldMatrix[origoPosition.x, origoPosition.z] is Road) {
            Road currentRoad = (Road)worldMatrix[origoPosition.x, origoPosition.z];
            currentRoad.DestroyGameObject();

            worldMatrix[origoPosition.x, origoPosition.z] =
                new EmptyField(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z));

            SetNatureGameObjectsVisibility(origoPosition, true);

            adjustAdjacentRoads(origoPosition);
        }
    }

    public void BuildNewStructure(Vector3Int origoPosition) {
        Debug.Log(CanAreaBePopulated(origoPosition, 2, 2));
    }

    private bool CanAreaBePopulated(Vector3Int origoPosition, int areaWidth, int areaLength) {
        return isAreaInBounds(origoPosition, areaWidth, areaLength)
            && DoesAreaConsistOfOnlyEmptyField(origoPosition, areaWidth, areaLength)
            && DoesAreaHaveRoadNextToIt(origoPosition, areaWidth, areaLength);
    }

    private bool DoesAreaHaveRoadNextToIt(Vector3Int origoPosition, int areaWidth, int areaLength) {
        if (0 < origoPosition.x) {
            for (int i = origoPosition.z; i < areaWidth; i++) {
                if (worldMatrix[origoPosition.x - 1, i] is Road) {
                    return true;
                }
            }
        }

        if (0 < origoPosition.z) {
            for (int i = origoPosition.x; i < areaLength; i++) {
                if (worldMatrix[i, origoPosition.z - 1] is Road) {
                    return true;
                }
            }
        }

        if (origoPosition.x + areaWidth + 1 < worldMatrixWidth) {
            for (int i = origoPosition.x; i < areaLength; i++) {
                if (worldMatrix[origoPosition.x + 1, i] is Road) {
                    return true;
                }
            }
        }

        if (origoPosition.z + areaLength + 1 < worldMatrixLength) {
            for (int i = origoPosition.x; i < areaLength; i++) {
                if (worldMatrix[i, origoPosition.z + 1] is Road) {
                    return true;
                }
            }
        }

        return true;
    }

    private bool DoesAreaConsistOfOnlyEmptyField(Vector3Int origoPosition, int areaWidth, int areaLength) {
        for (int i = origoPosition.x; i < areaWidth; i++) {
            for (int j = origoPosition.z; j < areaLength; j++) {
                if (!(worldMatrix[i, j] is EmptyField)) {
                    return false;
                }
            }
        }

        return true;
    }

    private bool isFieldInBounds(Vector3Int origoPosition) {
        return isAreaInBounds(origoPosition, 1, 1);
    }

    private bool isAreaInBounds(Vector3Int origoPosition, int areaWidth, int areaLength) {
        return 0 <= origoPosition.x && origoPosition.x + areaWidth < worldMatrixWidth
            && 0 <= origoPosition.z && origoPosition.z + areaLength < worldMatrixLength;
    }

    private void SetNatureGameObjectsVisibility(Vector3Int origoPosition, bool isVisible) {
        RaycastHit[] raycastHits = Physics.BoxCastAll(origoPosition + new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.5f, 0.5f),
            transform.up, Quaternion.identity, 1f, natureMask);

        foreach (var raycastHit in raycastHits) {
            raycastHit.collider.gameObject.GetComponent<MeshRenderer>().enabled = isVisible;
        }
    }

    internal IEnumerable<Field> GetWalkableAdjacentFields(Field currentField, bool isAIAgent) {
        throw new NotImplementedException();
    }

    internal float GetCostOfEnteringField(Field adjacentField) {
        throw new NotImplementedException();
    }
}
