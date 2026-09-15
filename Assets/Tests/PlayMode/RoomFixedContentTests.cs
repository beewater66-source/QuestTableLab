using System.Collections;
using System.Reflection;
using Meta.XR.MRUtilityKit;
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
        public IEnumerator CubeResetPoseCanBeRebasedAfterSemanticPlacement()
        {
            yield return null;

            GameObject cube = GameObject.Find("RoomFixedTestCube");
            ControllerCubeMover mover = Object.FindFirstObjectByType<ControllerCubeMover>();
            Vector3 semanticPosition = new(0.45f, 0.82f, 1.25f);
            Quaternion semanticRotation = Quaternion.Euler(0f, 30f, 0f);

            mover.SetResetPose(semanticPosition, semanticRotation);
            cube.transform.position += Vector3.one;
            mover.ResetTarget();

            Assert.That(Vector3.Distance(cube.transform.position, semanticPosition), Is.LessThan(0.001f));
            Assert.That(Quaternion.Angle(cube.transform.rotation, semanticRotation), Is.LessThan(0.01f));
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

        [UnityTest]
        public IEnumerator SemanticRoomLoaderIsAuthoredForExplicitDeviceLoading()
        {
            yield return null;

            GameObject semanticRoom = GameObject.Find("SemanticRoom");
            Assert.That(semanticRoom, Is.Not.Null);

            Component mruk = semanticRoom.GetComponent("MRUK");
            SemanticTablePlacementController placement =
                semanticRoom.GetComponent<SemanticTablePlacementController>();

            Assert.That(mruk, Is.Not.Null, "The scene needs one MRUK room-data provider.");
            Assert.That(placement, Is.Not.Null, "The TABLE label query must be authored in the scene.");

            object sceneSettings = mruk.GetType().GetField("SceneSettings").GetValue(mruk);
            Assert.That(sceneSettings, Is.Not.Null);
            Assert.That(sceneSettings.GetType().GetField("DataSource").GetValue(sceneSettings).ToString(),
                Is.EqualTo("Device"));
            Assert.That((bool)sceneSettings.GetType().GetField("LoadSceneOnStartup").GetValue(sceneSettings),
                Is.False, "The placement controller must own loading so it can report precise failures.");
        }

        [Test]
        public void SemanticTableSelectionUsesNearestSuitableTableVolume()
        {
            GameObject roomObject = new("SemanticSelectionTestRoom");
            MRUKRoom room = roomObject.AddComponent<MRUKRoom>();
            MRUKAnchor nearTable = CreateTestAnchor(
                room,
                "NearTable",
                MRUKAnchor.SceneLabels.TABLE,
                new Vector3(1f, 0.8f, 0f),
                hasVolume: true);
            CreateTestAnchor(
                room,
                "FarTable",
                MRUKAnchor.SceneLabels.TABLE,
                new Vector3(3f, 0.8f, 0f),
                hasVolume: true);
            CreateTestAnchor(
                room,
                "NearCouch",
                MRUKAnchor.SceneLabels.COUCH,
                new Vector3(0.2f, 0.5f, 0f),
                hasVolume: true);
            CreateTestAnchor(
                room,
                "TableWithoutVolume",
                MRUKAnchor.SceneLabels.TABLE,
                new Vector3(0.1f, 0.8f, 0f),
                hasVolume: false);

            try
            {
                bool found = SemanticTablePlacementController.TryFindNearestTable(
                    room,
                    Vector3.zero,
                    out MRUKAnchor selected,
                    out Vector3 top);

                Assert.That(found, Is.True);
                Assert.That(selected, Is.SameAs(nearTable));
                Assert.That(Vector3.Distance(top, nearTable.transform.position), Is.LessThan(0.001f));
            }
            finally
            {
                Object.DestroyImmediate(roomObject);
            }
        }

        [Test]
        public void SemanticWallSelectionPrefersVisibleFacingWall()
        {
            GameObject roomObject = new("SemanticWallSelectionTestRoom");
            MRUKRoom room = roomObject.AddComponent<MRUKRoom>();
            MRUKAnchor frontWall = CreateTestWall(
                room,
                "FrontWall",
                new Vector3(0f, 1.3f, 3f),
                Quaternion.Euler(0f, 180f, 0f));
            CreateTestWall(
                room,
                "RearWall",
                new Vector3(0f, 1.3f, -1.5f),
                Quaternion.identity);

            try
            {
                bool found = SemanticTablePlacementController.TryFindBestWall(
                    room,
                    Vector3.zero,
                    Vector3.forward,
                    out MRUKAnchor selected);

                Assert.That(found, Is.True);
                Assert.That(selected, Is.SameAs(frontWall));
            }
            finally
            {
                Object.DestroyImmediate(roomObject);
            }
        }

        [Test]
        public void WallUiPoseClampsRequestedOffsetInsidePlane()
        {
            GameObject roomObject = new("SemanticWallPoseTestRoom");
            MRUKRoom room = roomObject.AddComponent<MRUKRoom>();
            MRUKAnchor wall = CreateTestWall(
                room,
                "Wall",
                new Vector3(0f, 1f, 2f),
                Quaternion.Euler(0f, 180f, 0f));

            try
            {
                SemanticTablePlacementController.CalculateWallUiPose(
                    wall,
                    new Vector2(100f, 100f),
                    0.025f,
                    0.03f,
                    new Vector2(0.375f, 0.094f),
                    out Vector3 position,
                    out Quaternion rotation);

                Vector3 localPosition = wall.transform.InverseTransformPoint(
                    position - wall.transform.forward * 0.025f);
                Assert.That(localPosition.x, Is.EqualTo(1.595f).Within(0.001f));
                Assert.That(localPosition.y, Is.EqualTo(1.876f).Within(0.001f));
                Assert.That(Vector3.Dot(rotation * Vector3.forward, -wall.transform.forward),
                    Is.GreaterThan(0.999f));
            }
            finally
            {
                Object.DestroyImmediate(roomObject);
            }
        }

        private static MRUKAnchor CreateTestAnchor(
            MRUKRoom room,
            string name,
            MRUKAnchor.SceneLabels label,
            Vector3 topCenter,
            bool hasVolume)
        {
            GameObject anchorObject = new(name);
            anchorObject.transform.SetParent(room.transform);
            anchorObject.transform.position = topCenter;
            MRUKAnchor anchor = anchorObject.AddComponent<MRUKAnchor>();

            SetInternalProperty(anchor, nameof(MRUKAnchor.Label), label);
            if (hasVolume)
            {
                SetInternalProperty(
                    anchor,
                    nameof(MRUKAnchor.VolumeBounds),
                    (Bounds?)new Bounds(new Vector3(0f, 0f, -0.4f), new Vector3(1f, 1f, 0.8f)));
            }

            room.Anchors.Add(anchor);
            return anchor;
        }

        private static MRUKAnchor CreateTestWall(
            MRUKRoom room,
            string name,
            Vector3 position,
            Quaternion rotation)
        {
            GameObject wallObject = new(name);
            wallObject.transform.SetParent(room.transform);
            wallObject.transform.SetPositionAndRotation(position, rotation);
            MRUKAnchor wall = wallObject.AddComponent<MRUKAnchor>();

            SetInternalProperty(wall, nameof(MRUKAnchor.Label), MRUKAnchor.SceneLabels.WALL_FACE);
            SetInternalProperty(wall, nameof(MRUKAnchor.PlaneRect), (Rect?)new Rect(-2f, 0f, 4f, 2f));
            room.Anchors.Add(wall);
            room.WallAnchors.Add(wall);
            return wall;
        }

        private static void SetInternalProperty<T>(object target, string propertyName, T value)
        {
            PropertyInfo property = target.GetType().GetProperty(
                propertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            property.SetValue(target, value);
        }
    }
}
