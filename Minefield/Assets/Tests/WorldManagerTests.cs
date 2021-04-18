using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {
    public class WorldManagerTest {

        [SetUp]
        public void SetUp() {
            SceneManager.LoadScene("Minefield");
        }

        [UnityTest]
        public IEnumerator ShouldBuildRoadOnEmptyField() {
            WorldManager worldManager = GetWorldManager();

            Assert.IsTrue(worldManager.GetFieldAtPosition(Vector3Int.zero) is EmptyField);
            worldManager.BuildNewRoad(Vector3Int.zero);
            Assert.IsTrue(worldManager.GetFieldAtPosition(Vector3Int.zero) is Road);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyRoad() {
            WorldManager worldManager = GetWorldManager();

            worldManager.BuildNewRoad(Vector3Int.zero);
            Assert.IsTrue(worldManager.GetFieldAtPosition(Vector3Int.zero) is Road);
            worldManager.Destroy(Vector3Int.zero);
            Assert.IsTrue(worldManager.GetFieldAtPosition(Vector3Int.zero) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceHotDogCar() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);            
            worldManager.BuildNewHotdogCar(new Vector3Int(1, 0, 0));           
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyHotDogCar() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewHotdogCar(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceOlivegardensRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewOlivegardensRestaurant(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyOlivegardensRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewOlivegardensRestaurant(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceKFCRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewKFC(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyKFCRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewKFC(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceTaverneRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewTaverneRestaurant(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyTaverneRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewTaverneRestaurant(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceCafe() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCafe(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyCafe() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCafe(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceCafeRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCafeRestaurant(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyCafeRestaurant() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCafeRestaurant(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceMerryGoRound() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewMerryGoRound(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyMerryGoRound() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewMerryGoRound(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceCircusTent() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCircusTent(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyCircusTent() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCircusTent(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceLondonEye() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewLondonEye(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyLondonEye() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewLondonEye(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceRollerCoaster() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewRollerCoaster(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyRollerCoaster() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewRollerCoaster(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceBasicPark() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewBasicPark(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyBasicPark() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewBasicPark(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceFountainPark() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewFountainPark(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyFountainPark() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewFountainPark(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceHelicopterPark() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewHelicopterPark(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyHelicopterPark() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewHelicopterPark(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceCleanerStation() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCleanerStation(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyCleanerStation() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewCleanerStation(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldPlaceMechanicStation() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewMechanicStation(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is Structure);

            yield return null;
        }

        [UnityTest]
        public IEnumerator ShouldDestroyMechanicStation() {
            WorldManager worldManager = GetWorldManager();
            worldManager.BuildNewRoad(Vector3Int.zero);
            worldManager.BuildNewMechanicStation(new Vector3Int(1, 0, 0));
            worldManager.Destroy(new Vector3Int(1, 0, 0));
            Assert.IsTrue(worldManager.GetFieldAtPosition(new Vector3Int(1, 0, 0)) is EmptyField);

            yield return null;
        }

        private WorldManager GetWorldManager() { 
            return (WorldManager)SceneManager.GetSceneAt(0)
                .GetRootGameObjects()[5].gameObject
                .GetComponent(typeof(WorldManager));
        }
    }
}
