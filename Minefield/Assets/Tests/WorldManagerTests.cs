using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {
    public class WorldManagerTest {

        [SetUp]
        public void SetUp() {
            SceneManager.LoadScene("MineField");
        }

        [UnityTest]
        public IEnumerator ShouldBuildRoadOnEmptyField() {
            WorldManager worldManager = GetWorldManager();

            Assert.IsTrue(worldManager.GetFieldAtPosition(Vector3Int.zero) is EmptyField);
            worldManager.BuildNewRoad(Vector3Int.zero);
            Assert.IsTrue(worldManager.GetFieldAtPosition(Vector3Int.zero) is Road);

            yield return null;
        }

        private WorldManager GetWorldManager() { 
            return (WorldManager)SceneManager.GetSceneAt(0)
                .GetRootGameObjects()[5].gameObject
                .GetComponent(typeof(WorldManager));
        }
    }
}
