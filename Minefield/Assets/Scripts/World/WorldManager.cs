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
    private GameObject hotdogCarPrefab;
    [SerializeField]
    private int hotdogCarPrefabAreaWidth;
    [SerializeField]
    private int hotdogCarPrefabAreaLength;
    [SerializeField]
    private Vector3 hotdogCarPrefabPositionOffset;
    [SerializeField]
    private float hotdogCarPrefabYRotation;

    [SerializeField]
    private GameObject kfcPrefab;
    [SerializeField]
    private int kfcAreaWidth;
    [SerializeField]
    private int kfcAreaLength;
    [SerializeField]
    private Vector3 kfcPositionOffset;
    [SerializeField]
    private float kfcYRotation;

    [SerializeField]
    private GameObject cafeRestaurantPrefab;
    [SerializeField]
    private int cafeRestaurantAreaWidth;
    [SerializeField]
    private int cafeRestaurantAreaLength;
    [SerializeField]
    private Vector3 cafeRestaurantPositionOffset;
    [SerializeField]
    private float cafeRestaurantYRotation;

    [SerializeField]
    private LayerMask natureMask;

    public void Start() {
        InitializeFieldWorldMatrix();

        // Only for testing purposes
        PlaceDownNewRoad(new Vector3Int(0, 0, 51));
        PlaceDownNewRoad(new Vector3Int(0, 0, 52));
        PlaceDownNewRoad(new Vector3Int(0, 0, 53));
        PlaceDownNewRoad(new Vector3Int(0, 0, 54));
        PlaceDownNewRoad(new Vector3Int(0, 0, 55));
        PlaceDownNewRoad(new Vector3Int(0, 0, 56));

        PlaceDownNewRoad(new Vector3Int(4, 0, 51));
        PlaceDownNewRoad(new Vector3Int(4, 0, 52));
        PlaceDownNewRoad(new Vector3Int(3, 0, 53));
        PlaceDownNewRoad(new Vector3Int(3, 0, 54));
        PlaceDownNewRoad(new Vector3Int(1, 0, 55));
        PlaceDownNewRoad(new Vector3Int(1, 0, 56));


        PlaceDownNewRoad(new Vector3Int(8, 0, 51));
        PlaceDownNewRoad(new Vector3Int(8, 0, 52));

        BuildNewHotdogCar(new Vector3Int(1, 0, 57));
        // ***
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
            List<Field> orthogonallyAdjacentFields = GetOrthogonallyAdjecentFields(origoPosition);

            List<Field> orthogonallyAdjacentNonRoadFields = orthogonallyAdjacentFields.FindAll(field => !(field is Road));
            List<Road> orthogonallyAdjacentRoads = orthogonallyAdjacentFields.FindAll(field => field is Road).ConvertAll(field => (Road)field);

            int numberOfOrthogonallyAdjacentNonRoadFields = orthogonallyAdjacentNonRoadFields.Count;

            SetNatureGameObjectsVisibilityOfField(origoPosition, false);

            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsFour(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsThree(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
                orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsOne(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsZero(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);

            adjustOrthogonallyAdjacentRoads(origoPosition);
        }
    }

    private void adjustOrthogonallyAdjacentRoads(Vector3Int origoPosition) {
        adjustRoad(new Vector3Int(origoPosition.x - 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z + 1));
        adjustRoad(new Vector3Int(origoPosition.x + 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z - 1));
    }

    private void adjustRoad(Vector3Int origoPosition) {
        if (isFieldInBounds(origoPosition)
            && worldMatrix[origoPosition.x, origoPosition.z] is Road) {
            List<Field> orthogonallyAdjacentFields = GetOrthogonallyAdjecentFields(origoPosition);

            List<Field> orthogonallyAdjacentNonRoadFields = orthogonallyAdjacentFields.FindAll(field => !(field is Road));
            List<Road> orthogonallyAdjacentRoads = orthogonallyAdjacentFields.FindAll(field => field is Road).ConvertAll(field => (Road)field);

            int numberOfOrthogonallyAdjacentNonRoadFields = orthogonallyAdjacentNonRoadFields.Count;

            Road currentRoad = (Road)worldMatrix[origoPosition.x, origoPosition.z];
            currentRoad.DestroyGameObject();

            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsFour(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsThree(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
                orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsOne(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentNonRoadFields);
            PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsZero(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);
        }
    }

    private void PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsFour(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 4) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, 0);
        }
    }

    private void PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsThree(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
        List<Field> orthogonallyAdjacentFields, List<Road> adjacentRoads) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 3) {
            int yAngleMultiplier = orthogonallyAdjacentFields.IndexOf(adjacentRoads[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsTwo(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
        List<Field> orthogonallyAdjacentFields, List<Road> adjacentRoads) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 2) {
            int indexOfFirstOrthogonallyAdjacentRoad = orthogonallyAdjacentFields.IndexOf(adjacentRoads[0]);
            int indexOfSecondOrthogonallyAdjacentRoad = orthogonallyAdjacentFields.IndexOf(adjacentRoads[1]);

            if ((indexOfFirstOrthogonallyAdjacentRoad == 0 && indexOfSecondOrthogonallyAdjacentRoad == 2)
                || (indexOfFirstOrthogonallyAdjacentRoad == 1 && indexOfSecondOrthogonallyAdjacentRoad == 3)) {
                worldMatrix[origoPosition.x, origoPosition.z] = new StraightRoad(straightRoadPrefab, origoPosition, indexOfFirstOrthogonallyAdjacentRoad * 90);
            } else {
                int yAngleMultiplier = 0;

                if (indexOfFirstOrthogonallyAdjacentRoad == 1 && indexOfSecondOrthogonallyAdjacentRoad == 2) {
                    yAngleMultiplier = 1;
                } else if (indexOfFirstOrthogonallyAdjacentRoad == 2 && indexOfSecondOrthogonallyAdjacentRoad == 3) {
                    yAngleMultiplier = 2;
                } else if (indexOfFirstOrthogonallyAdjacentRoad == 0 && indexOfSecondOrthogonallyAdjacentRoad == 3) {
                    yAngleMultiplier = 3;
                }

                worldMatrix[origoPosition.x, origoPosition.z] =
                    new CornerRoad(cornerRoadPrefab, origoPosition, yAngleMultiplier * 90);
            }
        }
    }

    private void PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsOne(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
        List<Field> orthogonallyAdjacentFields, List<Field> adjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 1) {
            int yAngleMultiplier = orthogonallyAdjacentFields.IndexOf(adjacentNonRoadFields[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new ThreeWayRoad(threeWayRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void PlaceDownNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsZero(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 0) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new FourWayRoad(fourWayRoadPrefab, origoPosition, 0);
        }
    }

    public List<Field> GetOrthogonallyAdjecentFields(Vector3Int origoPosition) {
        List<Field> orthogonallyAdjacentFields = new List<Field>();

        if (0 < origoPosition.x) {
            orthogonallyAdjacentFields.Add(worldMatrix[origoPosition.x - 1, origoPosition.z]);
        } else {
            orthogonallyAdjacentFields.Add(null);
        }

        if (origoPosition.z < worldMatrixLength - 1) {
            orthogonallyAdjacentFields.Add(worldMatrix[origoPosition.x, origoPosition.z + 1]);
        } else {
            orthogonallyAdjacentFields.Add(null);
        }

        if (origoPosition.x < worldMatrixWidth - 1) {
            orthogonallyAdjacentFields.Add(worldMatrix[origoPosition.x + 1, origoPosition.z]);
        } else {
            orthogonallyAdjacentFields.Add(null);
        }

        if (0 < origoPosition.z) {
            orthogonallyAdjacentFields.Add(worldMatrix[origoPosition.x, origoPosition.z - 1]);
        } else {
            orthogonallyAdjacentFields.Add(null);
        }

        return orthogonallyAdjacentFields;
    }

    public void DestroyRoad(Vector3Int origoPosition) {
        if (isFieldInBounds(origoPosition)
            && worldMatrix[origoPosition.x, origoPosition.z] is Road) {
            Road currentRoad = (Road)worldMatrix[origoPosition.x, origoPosition.z];
            currentRoad.DestroyGameObject();

            worldMatrix[origoPosition.x, origoPosition.z] =
                new EmptyField(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z));

            SetNatureGameObjectsVisibilityOfField(origoPosition, true);

            adjustOrthogonallyAdjacentRoads(origoPosition);
        }
    }

    public void BuildNewHotdogCar(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, hotdogCarPrefabAreaWidth, hotdogCarPrefabAreaLength)) {
            Restaurant hotdogCar = new Restaurant(hotdogCarPrefab, origoPosition, hotdogCarPrefabPositionOffset,
                hotdogCarPrefabYRotation, hotdogCarPrefabAreaWidth, hotdogCarPrefabAreaLength);

            BuildNewStructure(hotdogCar, origoPosition, hotdogCarPrefabAreaWidth, hotdogCarPrefabAreaLength);
        }
    }

    public void BuildNewKFC(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, kfcAreaWidth, kfcAreaLength)) {
            Restaurant kfc = new Restaurant(kfcPrefab, origoPosition, kfcPositionOffset,
                kfcYRotation, kfcAreaWidth, kfcAreaLength);

            BuildNewStructure(kfc, origoPosition, kfcAreaWidth, kfcAreaLength);
        }
    }

    public void BuildNewCafeRestaurant(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, cafeRestaurantAreaWidth, cafeRestaurantAreaLength)) {
            Restaurant cafeRestaurant = new Restaurant(cafeRestaurantPrefab, origoPosition, cafeRestaurantPositionOffset,
                cafeRestaurantYRotation, cafeRestaurantAreaWidth, cafeRestaurantAreaLength);

            BuildNewStructure(cafeRestaurant, origoPosition, cafeRestaurantAreaWidth, cafeRestaurantAreaLength);
        }
    }

    public void BuildNewStructure(Structure structure, Vector3Int origoPosition, int areaWidth, int areaLength) {
        Debug.Log(origoPosition);

        for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
            for (int j = origoPosition.z; j < origoPosition.z + areaLength; j++) {
                worldMatrix[i, j] = structure;
            }
        }

        SetNatureGameObjectsVisibilityOfArea(origoPosition, areaWidth, areaLength, false);
    }

    public void DestroyStructure(Vector3Int origoPosition) {
        Field field = worldMatrix[origoPosition.x, origoPosition.z];

        if (field is Structure) {
            Structure structure = (Structure)field;
            structure.DestroyGameObject();

            for (int i = 0; i < worldMatrixWidth; i++) {
                for (int j = 0; j < worldMatrixLength; j++) {
                    if (structure == worldMatrix[i, j]) {
                        Vector3Int position = new Vector3Int(i, 0, j);
                        SetNatureGameObjectsVisibilityOfField(position, true);
                        worldMatrix[i, j] = new EmptyField(position);
                    }
                }
            }
        }
    }

    private bool CanAreaBePopulatedWithStructure(Vector3Int origoPosition, int areaWidth, int areaLength) {
        return isAreaInBounds(origoPosition, areaWidth, areaLength)
            && DoesAreaConsistOfOnlyEmptyField(origoPosition, areaWidth, areaLength)
            && DoesAreaHaveOrthogonallyAdjecentRoadNextToIt(origoPosition, areaWidth, areaLength);
    }

    private bool DoesAreaHaveOrthogonallyAdjecentRoadNextToIt(Vector3Int origoPosition, int areaWidth, int areaLength) {
        if (0 < origoPosition.z) {
            for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
                if (worldMatrix[i, origoPosition.z - 1] is Road) {
                    return true;
                }
            }
        }

        if (origoPosition.x + areaWidth < worldMatrixWidth - 1) {
            for (int i = origoPosition.z; i < origoPosition.z + areaLength; i++) {
                if (worldMatrix[origoPosition.x + areaWidth, i] is Road) {
                    return true;
                }
            }
        }

        if (origoPosition.z + areaLength < worldMatrixLength - 1) {
            for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
                if (worldMatrix[i, origoPosition.z + areaLength] is Road) {
                    return true;
                }
            }
        }

        if (0 < origoPosition.x) {
            for (int i = origoPosition.z; i < origoPosition.z + areaLength; i++) {
                if (worldMatrix[origoPosition.x - 1, i] is Road) {
                    return true;
                }
            }
        }


        return false;
    }

    private bool DoesAreaConsistOfOnlyEmptyField(Vector3Int origoPosition, int areaWidth, int areaLength) {
        for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
            for (int j = origoPosition.z; j < origoPosition.z + areaLength; j++) {
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

    private void SetNatureGameObjectsVisibilityOfArea(Vector3Int origoPosition, int areaWidth, int areaLength, bool isVisible) {
        for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
            for (int j = origoPosition.z; j < origoPosition.z + areaLength; j++) {
                SetNatureGameObjectsVisibilityOfField(new Vector3Int(i, origoPosition.y, j), isVisible);
            }
        }
    }

    private void SetNatureGameObjectsVisibilityOfField(Vector3Int origoPosition, bool isVisible) {
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
