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
    private LayerMask natureMask;

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
    private GameObject olivegardensRestaurantPrefab;
    [SerializeField]
    private int olivegardensRestaurantAreaWidth;
    [SerializeField]
    private int olivegardensRestaurantAreaLength;
    [SerializeField]
    private Vector3 olivegardensRestaurantPositionOffset;
    [SerializeField]
    private float olivegardensRestaurantYRotation;

    [SerializeField]
    private GameObject taverneRestaurantPrefab;
    [SerializeField]
    private int taverneRestaurantAreaWidth;
    [SerializeField]
    private int taverneRestaurantAreaLength;
    [SerializeField]
    private Vector3 taverneRestaurantPositionOffset;
    [SerializeField]
    private float taverneRestaurantYRotation;

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
    private GameObject cafePrefab;
    [SerializeField]
    private int cafeAreaWidth;
    [SerializeField]
    private int cafeAreaLength;
    [SerializeField]
    private Vector3 cafePositionOffset;
    [SerializeField]
    private float cafeYRotation;

    [SerializeField]
    private GameObject londonEyePrefab;
    [SerializeField]
    private int londonEyeAreaWidth;
    [SerializeField]
    private int londonEyeAreaLength;
    [SerializeField]
    private Vector3 londonEyePositionOffset;
    [SerializeField]
    private float londonEyeYRotation;

    [SerializeField]
    private GameObject merryGoRoundPrefab;
    [SerializeField]
    private int merryGoRoundAreaWidth;
    [SerializeField]
    private int merryGoRoundAreaLength;
    [SerializeField]
    private Vector3 merryGoRoundPositionOffset;
    [SerializeField]
    private float merryGoRoundYRotation;

    [SerializeField]
    private GameObject rollerCoasterPrefab;
    [SerializeField]
    private int rollerCoasterAreaWidth;
    [SerializeField]
    private int rollerCoasterAreaLength;
    [SerializeField]
    private Vector3 rollerCoasterPositionOffset;
    [SerializeField]
    private float rollerCoasterYRotation;

    [SerializeField]
    private GameObject circusTentPrefab;
    [SerializeField]
    private int circusTentAreaWidth;
    [SerializeField]
    private int circusTentAreaLength;
    [SerializeField]
    private Vector3 circusTentPositionOffset;
    [SerializeField]
    private float circusTentYRotation;

    [SerializeField]
    private GameObject basicParkPrefab;
    [SerializeField]
    private int basicParkAreaWidth;
    [SerializeField]
    private int basicParkAreaLength;
    [SerializeField]
    private Vector3 basicParkPositionOffset;
    [SerializeField]
    private float basicParkYRotation;

    [SerializeField]
    private GameObject fountainParkPrefab;
    [SerializeField]
    private int fountainParkAreaWidth;
    [SerializeField]
    private int fountainParkAreaLength;
    [SerializeField]
    private Vector3 fountainParkPositionOffset;
    [SerializeField]
    private float fountainParkYRotation;

    [SerializeField]
    private GameObject helicopterParkPrefab;
    [SerializeField]
    private int helicopterParkAreaWidth;
    [SerializeField]
    private int helicopterParkAreaLength;
    [SerializeField]
    private Vector3 helicopterParkPositionOffset;
    [SerializeField]
    private float helicopterParkYRotation;

    public void Start() {
        InitializeFieldWorldMatrix();

        for (int i = 0; i < 97; i++) {
            BuildNewRoad(new Vector3Int(i, 0, 50));
        }

        BuildNewHotdogCar(new Vector3Int(1, 0, 51));
        BuildNewKFC(new Vector3Int(3, 0, 51));
        BuildNewOlivegardensRestaurant(new Vector3Int(9, 0, 51));
        BuildNewTaverneRestaurant(new Vector3Int(13, 0, 51));

        BuildNewCafeRestaurant(new Vector3Int(17, 0, 51));
        BuildNewCafe(new Vector3Int(20, 0, 51));

        BuildNewBasicPark(new Vector3Int(23, 0, 51));
        BuildNewFountainPark(new Vector3Int(26, 0, 51));
        BuildNewHelicopterPark(new Vector3Int(30, 0, 51));

        BuildNewMerryGoRound(new Vector3Int(37, 0, 51));
        BuildNewLondonEye(new Vector3Int(40, 0, 51));
        BuildNewCircusTent(new Vector3Int(53, 0, 51));
        BuildNewRollerCoaster(new Vector3Int(67, 0, 51));
    }

    private void InitializeFieldWorldMatrix() {
        worldMatrix = new Field[worldMatrixWidth, worldMatrixLength];

        for (int i = 0; i < worldMatrixWidth; i++) {
            for (int j = 0; j < worldMatrixLength; j++) {
                worldMatrix[i, j] = new EmptyField(new Vector3Int(i, 0, j));
            }
        }
    }

    public void BuildNewRoad(Vector3Int origoPosition) {
        if (isFieldInBounds(origoPosition)
            && worldMatrix[origoPosition.x, origoPosition.z] is EmptyField) {
            List<Field> orthogonallyAdjacentFields = GetOrthogonallyAdjecentFields(origoPosition);

            List<Field> orthogonallyAdjacentNonRoadFields = orthogonallyAdjacentFields.FindAll(field => !(field is Road));
            List<Road> orthogonallyAdjacentRoads = orthogonallyAdjacentFields.FindAll(field => field is Road).ConvertAll(field => (Road)field);

            int numberOfOrthogonallyAdjacentNonRoadFields = orthogonallyAdjacentNonRoadFields.Count;

            SetNatureGameObjectsVisibilityOfField(origoPosition, false);

            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsFour(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsThree(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
                orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsOne(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentNonRoadFields);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsZero(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);

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

            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsFour(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsThree(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
                orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsTwo(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentRoads);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsOne(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields,
               orthogonallyAdjacentFields, orthogonallyAdjacentNonRoadFields);
            BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsZero(origoPosition, numberOfOrthogonallyAdjacentNonRoadFields);
        }
    }

    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsFour(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 4) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, 0);
        }
    }

    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsThree(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
        List<Field> orthogonallyAdjacentFields, List<Road> adjacentRoads) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 3) {
            int yAngleMultiplier = orthogonallyAdjacentFields.IndexOf(adjacentRoads[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsTwo(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
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

    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsOne(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
        List<Field> orthogonallyAdjacentFields, List<Field> adjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 1) {
            int yAngleMultiplier = orthogonallyAdjacentFields.IndexOf(adjacentNonRoadFields[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new ThreeWayRoad(threeWayRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsZero(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 0) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new FourWayRoad(fourWayRoadPrefab, origoPosition, 0);
        }
    }

    public List<Field> GetWalkableAdjacentFields(Field field, bool isAIAgent) {
        return GetOrthogonallyAdjecentFields(field.getOrigoPosition())
             .FindAll(orthogonallyAdjacentField => IsFieldWalkable(field, isAIAgent));
    }

    public static bool IsFieldWalkable(Field field, bool isAIAgent) {
        if (isAIAgent) {
            return field is Road;
        }

        return field is EmptyField || field is Road;
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

    public float GetCostOfEnteringField(Field orthogonallyAdjacentField) {
        return 1;
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

    public void BuildNewOlivegardensRestaurant(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, olivegardensRestaurantAreaWidth, olivegardensRestaurantAreaLength)) {
            Restaurant olivegardensRestaurant = new Restaurant(olivegardensRestaurantPrefab, origoPosition, olivegardensRestaurantPositionOffset,
                olivegardensRestaurantYRotation, olivegardensRestaurantAreaWidth, olivegardensRestaurantAreaLength);

            BuildNewStructure(olivegardensRestaurant, origoPosition, olivegardensRestaurantAreaWidth, olivegardensRestaurantAreaLength);
        }
    }

    public void BuildNewTaverneRestaurant(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, taverneRestaurantAreaWidth, taverneRestaurantAreaLength)) {
            Restaurant taverneRestaurant = new Restaurant(taverneRestaurantPrefab, origoPosition, taverneRestaurantPositionOffset,
                taverneRestaurantYRotation, taverneRestaurantAreaWidth, taverneRestaurantAreaLength);

            BuildNewStructure(taverneRestaurant, origoPosition, taverneRestaurantAreaWidth, taverneRestaurantAreaLength);
        }
    }

    public void BuildNewCafeRestaurant(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, cafeRestaurantAreaWidth, cafeRestaurantAreaLength)) {
            Bar cafeRestaurant = new Bar(cafeRestaurantPrefab, origoPosition, cafeRestaurantPositionOffset,
                cafeRestaurantYRotation, cafeRestaurantAreaWidth, cafeRestaurantAreaLength);

            BuildNewStructure(cafeRestaurant, origoPosition, cafeRestaurantAreaWidth, cafeRestaurantAreaLength);
        }
    }

    public void BuildNewCafe(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, cafeAreaWidth, cafeAreaLength)) {
            Bar cafe = new Bar(cafePrefab, origoPosition, cafePositionOffset,
                cafeYRotation, cafeAreaWidth, cafeAreaLength);

            BuildNewStructure(cafe, origoPosition, cafeAreaWidth, cafeAreaLength);
        }
    }

    public void BuildNewLondonEye(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, londonEyeAreaWidth, londonEyeAreaLength)) {
            Attraction londonEye = new Attraction(londonEyePrefab, origoPosition, londonEyePositionOffset,
                londonEyeYRotation, londonEyeAreaWidth, londonEyeAreaLength);

            BuildNewStructure(londonEye, origoPosition, londonEyeAreaWidth, londonEyeAreaLength);
        }
    }

    public void BuildNewMerryGoRound(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, merryGoRoundAreaWidth, merryGoRoundAreaLength)) {
            Attraction merryGoRound = new Attraction(merryGoRoundPrefab, origoPosition, merryGoRoundPositionOffset,
                merryGoRoundYRotation, merryGoRoundAreaWidth, merryGoRoundAreaLength);

            BuildNewStructure(merryGoRound, origoPosition, merryGoRoundAreaWidth, merryGoRoundAreaLength);
        }
    }

    public void BuildNewRollerCoaster(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, rollerCoasterAreaWidth, rollerCoasterAreaLength)) {
            Attraction rollerCoaster = new Attraction(rollerCoasterPrefab, origoPosition, rollerCoasterPositionOffset,
                rollerCoasterYRotation, rollerCoasterAreaWidth, rollerCoasterAreaLength);

            BuildNewStructure(rollerCoaster, origoPosition, rollerCoasterAreaWidth, rollerCoasterAreaLength);
        }
    }

    public void BuildNewCircusTent(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, circusTentAreaWidth, circusTentAreaLength)) {
            Attraction circusTent = new Attraction(circusTentPrefab, origoPosition, circusTentPositionOffset,
                circusTentYRotation, circusTentAreaWidth, circusTentAreaLength);

            BuildNewStructure(circusTent, origoPosition, circusTentAreaWidth, circusTentAreaLength);
        }
    }

    public void BuildNewBasicPark(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, basicParkAreaWidth, basicParkAreaLength)) {
            Park basicPark = new Park(basicParkPrefab, origoPosition, basicParkPositionOffset,
                basicParkYRotation, basicParkAreaWidth, basicParkAreaLength);

            BuildNewStructure(basicPark, origoPosition, basicParkAreaWidth, basicParkAreaLength);
        }
    }

    public void BuildNewFountainPark(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, fountainParkAreaWidth, fountainParkAreaLength)) {
            Park fountainPark = new Park(fountainParkPrefab, origoPosition, fountainParkPositionOffset,
                fountainParkYRotation, fountainParkAreaWidth, fountainParkAreaLength);

            BuildNewStructure(fountainPark, origoPosition, fountainParkAreaWidth, fountainParkAreaLength);
        }
    }

    public void BuildNewHelicopterPark(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, helicopterParkAreaWidth, helicopterParkAreaLength)) {
            Park helicopterPark = new Park(helicopterParkPrefab, origoPosition, helicopterParkPositionOffset,
                helicopterParkYRotation, helicopterParkAreaWidth, helicopterParkAreaLength);

            BuildNewStructure(helicopterPark, origoPosition, helicopterParkAreaWidth, helicopterParkAreaLength);
        }
    }

    private void BuildNewStructure(Structure structure, Vector3Int origoPosition, int areaWidth, int areaLength) {
        Debug.Log(origoPosition);

        for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
            for (int j = origoPosition.z; j < origoPosition.z + areaLength; j++) {
                worldMatrix[i, j] = structure;
            }
        }

        SetNatureGameObjectsVisibilityOfArea(origoPosition, areaWidth, areaLength, false);
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

    public void Destroy(Vector3Int origoPosition) {
        DestroyStructure(origoPosition);
        DestroyRoad(origoPosition);
    }

    private void DestroyRoad(Vector3Int origoPosition) {
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

    private void DestroyStructure(Vector3Int origoPosition) {
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
        RaycastHit[] raycastHits = Physics.BoxCastAll(origoPosition + new Vector3(0, 0.5f, 0),
            new Vector3(0.5f, 0.5f, 0.5f), transform.up, Quaternion.identity, 1f, natureMask);

        foreach (var raycastHit in raycastHits) {
            raycastHit.collider.gameObject.GetComponent<MeshRenderer>().enabled = isVisible;
        }

        if (isVisible) {
            if ((0 < origoPosition.x) && !(worldMatrix[origoPosition.x - 1, origoPosition.z] is EmptyField)) {
                SetNatureGameObjectsVisibilityOfField(origoPosition + new Vector3Int(-1, 0, 0), false);
            }

            if ((0 < origoPosition.z) && !(worldMatrix[origoPosition.x, origoPosition.z - 1] is EmptyField)) {
                SetNatureGameObjectsVisibilityOfField(origoPosition + new Vector3Int(0, 0, -1), false);
            }

            if ((origoPosition.x + 1 < worldMatrixWidth) && !(worldMatrix[origoPosition.x + 1, origoPosition.z] is EmptyField)) {
                SetNatureGameObjectsVisibilityOfField(origoPosition + new Vector3Int(1, 0, 0), false);
            }

            if ((origoPosition.z + 1 < worldMatrixLength) && !(worldMatrix[origoPosition.x, origoPosition.z + 1] is EmptyField)) {
                SetNatureGameObjectsVisibilityOfField(origoPosition + new Vector3Int(0, 0, 1), false);
            }
        }
    }
}
