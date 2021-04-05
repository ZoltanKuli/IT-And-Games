using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

    private Field[,] worldMatrix;
    [SerializeField]
    private int worldMatrixWidth;
    [SerializeField]
    private int worldMatrixLength;
    [SerializeField]
    private float npcNumberDevisior;

    private List<Restaurant> restaurants;
    private List<Bar> bars;
    private List<Attraction> attractions;
    private List<Park> parks;
    private List<GarbageCan> garbageCans;

    private System.Random random;

    [SerializeField]
    private LayerMask natureMask;

    private Entrance entrance;
    [SerializeField]
    private GameObject entrancePrefab;
    [SerializeField]
    private Vector3 entrancePositionOffset;
    [SerializeField]
    private float entrancePrefabYRotation;

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

    [SerializeField]
    private GameObject garbageCanPrefab;
    [SerializeField]
    private int garbageCanAreaWidth;
    [SerializeField]
    private int garbageCanAreaLength;
    [SerializeField]
    private Vector3 garbageCanPositionOffset;
    [SerializeField]
    private float garbageCanYRotation;

    /// <summary>
    /// Initialize world.
    /// </summary>
    public void Start() {
        InitializeFieldWorldMatrix();

        random = new System.Random();

        BuildNewEntrance();

        restaurants = new List<Restaurant>();
        bars = new List<Bar>();
        attractions = new List<Attraction>();
        parks = new List<Park>();
        garbageCans = new List<GarbageCan>();

        for (int i = 1; i < 21; i++) {
            BuildNewRoad(new Vector3Int(i, 0, 50));
        }

        BuildNewRoad(new Vector3Int(20, 0, 49));
        BuildNewRoad(new Vector3Int(20, 0, 48));
        BuildNewRoad(new Vector3Int(21, 0, 48));
        BuildNewRoad(new Vector3Int(22, 0, 48));
        BuildNewRoad(new Vector3Int(23, 0, 48));
        BuildNewRoad(new Vector3Int(23, 0, 49));
        BuildNewRoad(new Vector3Int(23, 0, 48));

        for (int i = 23; i < 97; i++) {
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

    /// <summary>
    /// Initialize FieldWorldMatrix.
    /// </summary>
    private void InitializeFieldWorldMatrix() {
        worldMatrix = new Field[worldMatrixWidth, worldMatrixLength];

        for (int i = 0; i < worldMatrixWidth; i++) {
            for (int j = 0; j < worldMatrixLength; j++) {
                worldMatrix[i, j] = new EmptyField(new Vector3Int(i, 0, j));
            }
        }
    }

    public int GetMaximumNPCNumberBasedOnWorldMatrixSize() {
        return (int)Mathf.Ceil((worldMatrixWidth * worldMatrixLength) / npcNumberDevisior);
    }

    /// <summary>
    /// Build new entrance.
    /// </summary>
    private void BuildNewEntrance() {
        Vector3Int entranceOrigoPosition = new Vector3Int(random.Next(worldMatrixWidth), 0, random.Next(worldMatrixLength));
        entranceOrigoPosition = new Vector3Int(0, 0, 50);
        entrance = new Entrance(entrancePrefab, entranceOrigoPosition, entrancePositionOffset, entrancePrefabYRotation);
        worldMatrix[entranceOrigoPosition.x, entranceOrigoPosition.z] = entrance;
    }

    /// <summary>
    /// Get entrance.
    /// </summary>
    public Entrance GetEntrance() {
        return entrance;
    }

    /// <summary>
    /// Build a new road according to its surroundings. (e.g. Crossroads, Turns)
    /// </summary>
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

    /// <summary>
    /// Adjust orthogonally adjacent roads.
    /// </summary>
    private void adjustOrthogonallyAdjacentRoads(Vector3Int origoPosition) {
        adjustRoad(new Vector3Int(origoPosition.x - 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z + 1));
        adjustRoad(new Vector3Int(origoPosition.x + 1, origoPosition.y, origoPosition.z));
        adjustRoad(new Vector3Int(origoPosition.x, origoPosition.y, origoPosition.z - 1));
    }

    /// <summary>
    /// Adjust road.
    /// </summary>
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

    /// <summary>
    /// Build new road if number of orthogonally adjacent non road fields is four.
    /// </summary>
    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsFour(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 4) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, 0);
        }
    }

    /// <summary>
    /// Build new road if number of orthogonally adjacent non road fields is three.
    /// </summary>
    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsThree(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
        List<Field> orthogonallyAdjacentFields, List<Road> adjacentRoads) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 3) {
            int yAngleMultiplier = orthogonallyAdjacentFields.IndexOf(adjacentRoads[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new StraightRoad(straightRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    /// <summary>
    /// Build new road if number of orthogonally adjacent non road fields is two.
    /// </summary>
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

    /// <summary>
    /// Build new road if number of orthogonally adjacent non road fields is one.
    /// </summary>
    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsOne(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields,
        List<Field> orthogonallyAdjacentFields, List<Field> adjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 1) {
            int yAngleMultiplier = orthogonallyAdjacentFields.IndexOf(adjacentNonRoadFields[0]);

            worldMatrix[origoPosition.x, origoPosition.z] =
                new ThreeWayRoad(threeWayRoadPrefab, origoPosition, yAngleMultiplier * 90);
        }
    }

    /// <summary>
    /// Build new road if number of orthogonally adjacent non road fields is zero.
    /// </summary>
    private void BuildNewRoadIfNumberOfOrthogonallyAdjacentNonRoadFieldsIsZero(Vector3Int origoPosition, int numberOfOrthogonallyAdjacentNonRoadFields) {
        if (numberOfOrthogonallyAdjacentNonRoadFields == 0) {
            worldMatrix[origoPosition.x, origoPosition.z] =
                new FourWayRoad(fourWayRoadPrefab, origoPosition, 0);
        }
    }

    /// <summary>
    /// Get walkable adjacent fields.
    /// </summary>
    public List<Field> GetWalkableAdjacentFields(Field field, bool isAIAgent) {
        return GetOrthogonallyAdjecentFields(field.GetOrigoPosition())
             .FindAll(orthogonallyAdjacentField => IsFieldWalkable(orthogonallyAdjacentField, isAIAgent));
    }

    /// <summary>
    /// Is field walkable.
    /// </summary>
    public static bool IsFieldWalkable(Field field, bool isAIAgent) {
        if (isAIAgent) {
            return field is Road || field is Entrance;
        }

        return field is EmptyField || field is Road;
    }

    /// <summary>
    /// Get orthogonally adjecent fields.
    /// </summary>
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

    /// <summary>
    /// Build new HotdogCar.
    /// </summary>
    public void BuildNewHotdogCar(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, hotdogCarPrefabAreaWidth, hotdogCarPrefabAreaLength)) {
            Restaurant hotdogCar = new Restaurant(hotdogCarPrefab, origoPosition, hotdogCarPrefabPositionOffset,
                hotdogCarPrefabYRotation, hotdogCarPrefabAreaWidth, hotdogCarPrefabAreaLength);

            BuildNewStructure(hotdogCar, origoPosition, hotdogCarPrefabAreaWidth, hotdogCarPrefabAreaLength);

            restaurants.Add(hotdogCar);
        }
    }

    /// <summary>
    /// Build new KFC.
    /// </summary>
    public void BuildNewKFC(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, kfcAreaWidth, kfcAreaLength)) {
            Restaurant kfc = new Restaurant(kfcPrefab, origoPosition, kfcPositionOffset,
                kfcYRotation, kfcAreaWidth, kfcAreaLength);

            BuildNewStructure(kfc, origoPosition, kfcAreaWidth, kfcAreaLength);

            restaurants.Add(kfc);
        }
    }

    /// <summary>
    /// Build new OlivegardensRestaurant.
    /// </summary>
    public void BuildNewOlivegardensRestaurant(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, olivegardensRestaurantAreaWidth, olivegardensRestaurantAreaLength)) {
            Restaurant olivegardensRestaurant = new Restaurant(olivegardensRestaurantPrefab, origoPosition, olivegardensRestaurantPositionOffset,
                olivegardensRestaurantYRotation, olivegardensRestaurantAreaWidth, olivegardensRestaurantAreaLength);

            BuildNewStructure(olivegardensRestaurant, origoPosition, olivegardensRestaurantAreaWidth, olivegardensRestaurantAreaLength);

            restaurants.Add(olivegardensRestaurant);
        }
    }

    /// <summary>
    /// Build new TaverneRestaurant.
    /// </summary>
    public void BuildNewTaverneRestaurant(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, taverneRestaurantAreaWidth, taverneRestaurantAreaLength)) {
            Restaurant taverneRestaurant = new Restaurant(taverneRestaurantPrefab, origoPosition, taverneRestaurantPositionOffset,
                taverneRestaurantYRotation, taverneRestaurantAreaWidth, taverneRestaurantAreaLength);

            BuildNewStructure(taverneRestaurant, origoPosition, taverneRestaurantAreaWidth, taverneRestaurantAreaLength);

            restaurants.Add(taverneRestaurant);
        }
    }

    /// <summary>
    /// Build new CafeRestaurant.
    /// </summary>
    public void BuildNewCafeRestaurant(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, cafeRestaurantAreaWidth, cafeRestaurantAreaLength)) {
            Bar cafeRestaurant = new Bar(cafeRestaurantPrefab, origoPosition, cafeRestaurantPositionOffset,
                cafeRestaurantYRotation, cafeRestaurantAreaWidth, cafeRestaurantAreaLength);

            BuildNewStructure(cafeRestaurant, origoPosition, cafeRestaurantAreaWidth, cafeRestaurantAreaLength);

            bars.Add(cafeRestaurant);
        }
    }

    /// <summary>
    /// Build new Cafe.
    /// </summary>
    public void BuildNewCafe(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, cafeAreaWidth, cafeAreaLength)) {
            Bar cafe = new Bar(cafePrefab, origoPosition, cafePositionOffset,
                cafeYRotation, cafeAreaWidth, cafeAreaLength);

            BuildNewStructure(cafe, origoPosition, cafeAreaWidth, cafeAreaLength);

            bars.Add(cafe);
        }
    }

    /// <summary>
    /// Build new LondonEye.
    /// </summary>
    public void BuildNewLondonEye(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, londonEyeAreaWidth, londonEyeAreaLength)) {
            Attraction londonEye = new Attraction(londonEyePrefab, origoPosition, londonEyePositionOffset,
                londonEyeYRotation, londonEyeAreaWidth, londonEyeAreaLength);

            BuildNewStructure(londonEye, origoPosition, londonEyeAreaWidth, londonEyeAreaLength);

            attractions.Add(londonEye);
        }
    }

    /// <summary>
    /// Build new MerryGoRound.
    /// </summary>
    public void BuildNewMerryGoRound(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, merryGoRoundAreaWidth, merryGoRoundAreaLength)) {
            Attraction merryGoRound = new Attraction(merryGoRoundPrefab, origoPosition, merryGoRoundPositionOffset,
                merryGoRoundYRotation, merryGoRoundAreaWidth, merryGoRoundAreaLength);

            BuildNewStructure(merryGoRound, origoPosition, merryGoRoundAreaWidth, merryGoRoundAreaLength);

            attractions.Add(merryGoRound);
        }
    }

    /// <summary>
    /// Build new RollerCoaster.
    /// </summary>
    public void BuildNewRollerCoaster(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, rollerCoasterAreaWidth, rollerCoasterAreaLength)) {
            Attraction rollerCoaster = new Attraction(rollerCoasterPrefab, origoPosition, rollerCoasterPositionOffset,
                rollerCoasterYRotation, rollerCoasterAreaWidth, rollerCoasterAreaLength);

            BuildNewStructure(rollerCoaster, origoPosition, rollerCoasterAreaWidth, rollerCoasterAreaLength);

            attractions.Add(rollerCoaster);
        }
    }

    /// <summary>
    /// Build new CircusTent.
    /// </summary>
    public void BuildNewCircusTent(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, circusTentAreaWidth, circusTentAreaLength)) {
            Attraction circusTent = new Attraction(circusTentPrefab, origoPosition, circusTentPositionOffset,
                circusTentYRotation, circusTentAreaWidth, circusTentAreaLength);

            BuildNewStructure(circusTent, origoPosition, circusTentAreaWidth, circusTentAreaLength);

            attractions.Add(circusTent);
        }
    }

    /// <summary>
    /// Build new BasicPark.
    /// </summary>
    public void BuildNewBasicPark(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, basicParkAreaWidth, basicParkAreaLength)) {
            Park basicPark = new Park(basicParkPrefab, origoPosition, basicParkPositionOffset,
                basicParkYRotation, basicParkAreaWidth, basicParkAreaLength);

            BuildNewStructure(basicPark, origoPosition, basicParkAreaWidth, basicParkAreaLength);

            parks.Add(basicPark);
        }
    }

    /// <summary>
    /// Build new FountainPark.
    /// </summary>
    public void BuildNewFountainPark(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, fountainParkAreaWidth, fountainParkAreaLength)) {
            Park fountainPark = new Park(fountainParkPrefab, origoPosition, fountainParkPositionOffset,
                fountainParkYRotation, fountainParkAreaWidth, fountainParkAreaLength);

            BuildNewStructure(fountainPark, origoPosition, fountainParkAreaWidth, fountainParkAreaLength);

            parks.Add(fountainPark);
        }
    }

    /// <summary>
    /// Build new HelicopterPark.
    /// </summary>
    public void BuildNewHelicopterPark(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, helicopterParkAreaWidth, helicopterParkAreaLength)) {
            Park helicopterPark = new Park(helicopterParkPrefab, origoPosition, helicopterParkPositionOffset,
                helicopterParkYRotation, helicopterParkAreaWidth, helicopterParkAreaLength);

            BuildNewStructure(helicopterPark, origoPosition, helicopterParkAreaWidth, helicopterParkAreaLength);

            parks.Add(helicopterPark);
        }
    }
 
    /// <summary>
    /// Build new GarbageCan.
    /// </summary>
    public void BuildNewGarbageCan(Vector3Int origoPosition) {
        if (CanAreaBePopulatedWithStructure(origoPosition, garbageCanAreaWidth, garbageCanAreaLength)) {
            GarbageCan garbageCan = new GarbageCan(garbageCanPrefab, origoPosition, garbageCanPositionOffset,
                garbageCanYRotation, garbageCanAreaWidth, garbageCanAreaLength);

            BuildNewStructure(garbageCan, origoPosition, garbageCanAreaWidth, garbageCanAreaLength);
        }
    }

    /// <summary>
    /// Build new Structure.
    /// </summary>
    private void BuildNewStructure(Structure structure, Vector3Int origoPosition, int areaWidth, int areaLength) {
        for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
            for (int j = origoPosition.z; j < origoPosition.z + areaLength; j++) {
                worldMatrix[i, j] = structure;
            }
        }

        SetNatureGameObjectsVisibilityOfArea(origoPosition, areaWidth, areaLength, false);
    }

    /// <summary>
    /// Can area be populated with structure.
    /// </summary>
    private bool CanAreaBePopulatedWithStructure(Vector3Int origoPosition, int areaWidth, int areaLength) {
        return isAreaInBounds(origoPosition, areaWidth, areaLength)
            && DoesAreaConsistOfOnlyEmptyFields(origoPosition, areaWidth, areaLength)
            && DoesAreaHaveOrthogonallyAdjecentRoadNextToIt(origoPosition, areaWidth, areaLength);
    }

    /// <summary>
    /// Does area have orthogonally adjecent road next to it.
    /// </summary>
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

    /// <summary>
    /// Get orthogonally adjecent roads around structure.
    /// </summary>
    public List<Road> GetOrthogonallyAdjecentRoadsAroundStructure(Structure structure) {
        List<Road> orthogonallyAdjacentRoadsAroundStructure = new List<Road>(); 

        if (0 < structure.GetOrigoPosition().z) {
            for (int i = structure.GetOrigoPosition().x; i < structure.GetOrigoPosition().x + structure.GetWidth(); i++) {
                if (worldMatrix[i, structure.GetOrigoPosition().z - 1] is Road) {
                    orthogonallyAdjacentRoadsAroundStructure.Add(
                        (Road) worldMatrix[i, structure.GetOrigoPosition().z - 1]);
                }
            }
        }

        if (structure.GetOrigoPosition().x + structure.GetWidth() < worldMatrixWidth - 1) {
            for (int i = structure.GetOrigoPosition().z; i < structure.GetOrigoPosition().z + structure.GetLength(); i++) {
                if (worldMatrix[structure.GetOrigoPosition().x + structure.GetWidth(), i] is Road) {
                    orthogonallyAdjacentRoadsAroundStructure.Add(
                        (Road)worldMatrix[structure.GetOrigoPosition().x + structure.GetWidth(), i]);
                }
            }
        }

        if (structure.GetOrigoPosition().z + structure.GetLength() < worldMatrixLength - 1) {
            for (int i = structure.GetOrigoPosition().x; i < structure.GetOrigoPosition().x + structure.GetWidth(); i++) {
                if (worldMatrix[i, structure.GetOrigoPosition().z + structure.GetWidth()] is Road) {
                    orthogonallyAdjacentRoadsAroundStructure.Add(
                        (Road)worldMatrix[i, structure.GetOrigoPosition().z + structure.GetWidth()]);
                }
            }
        }

        if (0 < structure.GetOrigoPosition().x) {
            for (int i = structure.GetOrigoPosition().z; i < structure.GetOrigoPosition().z + structure.GetLength(); i++) {
                if (worldMatrix[structure.GetOrigoPosition().x - 1, i] is Road) {
                    orthogonallyAdjacentRoadsAroundStructure.Add(
                        (Road)worldMatrix[structure.GetOrigoPosition().x - 1, i]);
                }
            }
        }


        return orthogonallyAdjacentRoadsAroundStructure;
    }

    /// <summary>
    /// Does area consist of only empty fields.
    /// </summary>
    private bool DoesAreaConsistOfOnlyEmptyFields(Vector3Int origoPosition, int areaWidth, int areaLength) {
        for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
            for (int j = origoPosition.z; j < origoPosition.z + areaLength; j++) {
                if (!(worldMatrix[i, j] is EmptyField)) {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Destroy the PopulatedField with the given origo position. 
    /// </summary>
    public void Destroy(Vector3Int origoPosition) {
        DestroyStructure(origoPosition);
        DestroyRoad(origoPosition);
    }

    /// <summary>
    /// Destroy road.
    /// </summary>
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

    /// <summary>
    /// Destroy structure.
    /// </summary>
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

            RemoveStructureFromListBasedOnType(structure);
        }
    }

    /// <summary>
    /// Remove structure from list based on type.
    /// </summary>
    private void RemoveStructureFromListBasedOnType(Structure structure) {
        if (structure is Restaurant) {
            restaurants.Remove((Restaurant)structure);
        } else if (structure is Bar) {
            bars.Remove((Bar)structure);
        } else if (structure is Attraction) {
            attractions.Remove((Attraction)structure);
        } else if (structure is Park) {
            parks.Remove((Park)structure);
        } else if (structure is GarbageCan) {
            garbageCans.Remove((GarbageCan)structure);
        }
    }

    /// <summary>
    /// Is field in bounds.
    /// </summary>
    private bool isFieldInBounds(Vector3Int origoPosition) {
        return isAreaInBounds(origoPosition, 1, 1);
    }

    /// <summary>
    /// Is area in bounds.
    /// </summary>
    private bool isAreaInBounds(Vector3Int origoPosition, int areaWidth, int areaLength) {
        return 0 <= origoPosition.x && origoPosition.x + areaWidth < worldMatrixWidth
            && 0 <= origoPosition.z && origoPosition.z + areaLength < worldMatrixLength;
    }

    /// <summary>
    /// Set nature game objects visibility of area.
    /// </summary>
    private void SetNatureGameObjectsVisibilityOfArea(Vector3Int origoPosition, int areaWidth, int areaLength, bool isVisible) {
        for (int i = origoPosition.x; i < origoPosition.x + areaWidth; i++) {
            for (int j = origoPosition.z; j < origoPosition.z + areaLength; j++) {
                SetNatureGameObjectsVisibilityOfField(new Vector3Int(i, origoPosition.y, j), isVisible);
            }
        }
    }

    /// <summary>
    /// Set nature game objects visibility of field.
    /// </summary>
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

    /// <summary>
    /// Get field at position.
    /// </summary>
    public Field GetFieldAtPosition(Vector3Int position) {
        return worldMatrix[position.x, position.z];
    }

    /// <summary>
    /// Get random restaurant.
    /// </summary>
    public Restaurant GetRandomRestaurant() {
        return restaurants[random.Next(restaurants.Count)];
    }

    /// <summary>
    /// Get random bar.
    /// </summary>
    public Bar getRandomBar() {
        return bars[random.Next(bars.Count)];
    }

    /// <summary>
    /// Get random attraction.
    /// </summary>
    public Attraction getRandomAttraction() {
        return attractions[random.Next(attractions.Count)];
    }
}
