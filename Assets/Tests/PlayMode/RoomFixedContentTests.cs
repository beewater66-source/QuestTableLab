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

        [UnityTest]
        public IEnumerator CubeCanBeResetAfterItWasMoved()
        {
            yield return null;

            GameObject cube = GameObject.Find("RoomFixedTestCube");
            ControllerCubeMover mover = Object.FindFirstObjectByType<ControllerCubeMover>();
            Vector3 initialPosition = cube.transform.position;
            Quaternion initialRotation = cube.transform.rotation;

            cube.transform.SetPositionAndRotation(
                initialPosition + new Vector3(0.4f, 0.2f, -0.3f),
                Quaternion.Euler(10f, 35f, 5f));

            mover.ResetTarget();

            Assert.That(Vector3.Distance(cube.transform.position, initialPosition), Is.LessThan(0.001f));
            Assert.That(Quaternion.Angle(cube.transform.rotation, initialRotation), Is.LessThan(0.01f));
        }

        [UnityTest]
        public IEnumerator CubeCanBePlacedWithItsBottomOnFloorLevel()
        {
            yield return null;

            GameObject cube = GameObject.Find("RoomFixedTestCube");
            ControllerCubeMover mover = Object.FindFirstObjectByType<ControllerCubeMover>();
            Vector3 requestedFloorPoint = new(0.35f, 0f, 1.1f);

            Assert.That(cube, Is.Not.Null);
            Assert.That(mover, Is.Not.Null);

            mover.PlaceTargetOnFloor(requestedFloorPoint);

            Collider cubeCollider = cube.GetComponent<Collider>();
            Assert.That(Mathf.Abs(cubeCollider.bounds.min.y), Is.LessThan(0.001f));
            Assert.That(Mathf.Abs(cube.transform.position.x - requestedFloorPoint.x), Is.LessThan(0.001f));
            Assert.That(Mathf.Abs(cube.transform.position.z - requestedFloorPoint.z), Is.LessThan(0.001f));
        }

        [UnityTest]
        public IEnumerator HelloPanelUsesDedicatedOvrOverlayCanvas()
        {
            yield return null;

            GameObject panel = GameObject.Find("HelloPanel");
            Assert.That(panel, Is.Not.Null);

            Component overlay = panel.GetComponent("OVROverlayCanvas");
            Assert.That(overlay, Is.Not.Null, "The world-space label must use OVROverlayCanvas.");
            Assert.That(panel.layer, Is.EqualTo(LayerMask.NameToLayer("Overlay UI")));

            Transform message = panel.transform.Find("Message");
            Assert.That(message, Is.Not.Null);
            Assert.That(message.gameObject.layer, Is.EqualTo(panel.layer),
                "The authored label content must use the hidden overlay layer.");

            Assert.That((bool)overlay.GetType().GetField("manualRedraw").GetValue(overlay), Is.True,
                "The static label should not be redrawn every frame.");
            Assert.That(overlay.GetType().GetField("compositionMode").GetValue(overlay).ToString(),
                Is.EqualTo("DepthTested"));
        }
    }
}
