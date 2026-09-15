using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace QuestTableLab.Tests.PlayMode
{
    public sealed class RoomFixedContentTests
    {
        private const string SceneName = "SampleScene";

        [UnitySetUp]
        public IEnumerator LoadProjectScene()
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
            yield return null;
        }

        [UnityTest]
        public IEnumerator CubeExistsAtExpectedRoomFixedPose()
        {
            yield return null;

            GameObject cube = GameObject.Find("RoomFixedTestCube");

            Assert.That(cube, Is.Not.Null);
            Assert.That(cube.transform.parent, Is.Null,
                "The cube must remain at the scene root instead of following the XR camera.");
            Assert.That(Vector3.Distance(cube.transform.position, new Vector3(0f, 0.9f, 1.5f)),
                Is.LessThan(0.001f));
            Assert.That(Vector3.Distance(cube.transform.localScale, Vector3.one * 0.2f),
                Is.LessThan(0.001f));
        }

        [UnityTest]
        public IEnumerator LabelIsWorldSpaceAndAboveCube()
        {
            yield return null;

            GameObject cube = GameObject.Find("RoomFixedTestCube");
            GameObject label = GameObject.Find("HelloPanel");

            Assert.That(cube, Is.Not.Null);
            Assert.That(label, Is.Not.Null);
            Assert.That(label.transform.parent, Is.Null,
                "The label must remain at the scene root instead of following the XR camera.");

            Canvas canvas = label.GetComponent<Canvas>();
            Assert.That(canvas, Is.Not.Null);
            Assert.That(canvas.renderMode, Is.EqualTo(RenderMode.WorldSpace));
            Assert.That(label.transform.position.y, Is.GreaterThan(cube.transform.position.y));
            Assert.That(Mathf.Abs(label.transform.position.z - cube.transform.position.z),
                Is.LessThan(0.5f), "The label must remain visually associated with the cube.");
        }
    }
}
