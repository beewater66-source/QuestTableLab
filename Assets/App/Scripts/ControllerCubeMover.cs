using UnityEngine;

/// <summary>
/// Lets the user remotely move the test cube with the right controller ray.
/// Hold the index trigger while pointing at the cube and release to leave it
/// at the new room-fixed position.
/// </summary>
public sealed class ControllerCubeMover : MonoBehaviour
{
    [SerializeField] private ControllerInputProbe controllerAim;
    [SerializeField] private Transform target;
    [SerializeField] private string fallbackTargetName = "RoomFixedTestCube";

    private Collider targetCollider;
    private bool isMoving;
    private float grabDistance;
    private Vector3 grabOffset;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private GameObject placementPreview;
    private Material previewMaterial;

    private void Awake()
    {
        if (controllerAim == null)
        {
            controllerAim = GetComponent<ControllerInputProbe>();
        }

        if (target == null)
        {
            var targetObject = GameObject.Find(fallbackTargetName);
            target = targetObject != null ? targetObject.transform : null;
        }

        targetCollider = target != null ? target.GetComponent<Collider>() : null;

        if (target != null)
        {
            initialPosition = target.position;
            initialRotation = target.rotation;
        }

        if (controllerAim == null || targetCollider == null)
        {
            Debug.LogError("[QuestTableLab] Controller cube mover is missing its controller aim or cube collider.");
            enabled = false;
            return;
        }

        CreatePlacementPreview();
    }

    private void OnDisable()
    {
        if (placementPreview != null)
        {
            placementPreview.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (placementPreview != null)
        {
            Destroy(placementPreview);
        }

        if (previewMaterial != null)
        {
            Destroy(previewMaterial);
        }
    }

    private void LateUpdate()
    {
        if (!controllerAim.IsControllerActive)
        {
            isMoving = false;
            placementPreview.SetActive(false);
            return;
        }

        var controller = controllerAim.Controller;
        var aimRay = controllerAim.AimRay;
        var hasFloorPoint = controllerAim.TryGetFloorPoint(out var floorPoint, out var floorDistance);
        var hitsCubeFirst = targetCollider.Raycast(aimRay, out var cubeHit, controllerAim.RayLength)
            && (!hasFloorPoint || cubeHit.distance < floorDistance);

        placementPreview.SetActive(hasFloorPoint && !hitsCubeFirst && !isMoving);
        if (placementPreview.activeSelf)
        {
            placementPreview.transform.SetPositionAndRotation(
                GetRestingPosition(floorPoint),
                initialRotation);
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, controller))
        {
            ResetTarget();
            return;
        }

        if (!isMoving && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller)
            && hitsCubeFirst)
        {
            isMoving = true;
            placementPreview.SetActive(false);
            grabDistance = cubeHit.distance;
            grabOffset = target.position - cubeHit.point;
            Debug.Log("[QuestTableLab] Cube movement started.");
        }

        else if (!isMoving && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller)
            && hasFloorPoint)
        {
            PlaceTargetOnFloor(floorPoint);
            return;
        }

        if (!isMoving)
        {
            return;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            target.position = aimRay.GetPoint(grabDistance) + grabOffset;
            return;
        }

        isMoving = false;
        Debug.Log($"[QuestTableLab] Cube movement finished at {target.position}.");
    }

    public void ResetTarget()
    {
        isMoving = false;
        target.SetPositionAndRotation(initialPosition, initialRotation);
        Debug.Log("[QuestTableLab] Cube reset to its initial pose.");
    }

    /// <summary>
    /// Replaces the pose used by the B-button reset. Semantic placement uses
    /// this after a real table was found so reset returns to that table.
    /// </summary>
    public void SetResetPose(Vector3 position, Quaternion rotation, bool moveTarget = true)
    {
        initialPosition = position;
        initialRotation = rotation;

        if (moveTarget)
        {
            isMoving = false;
            target.SetPositionAndRotation(initialPosition, initialRotation);
            Physics.SyncTransforms();
        }
    }

    public void PlaceTargetOnFloor(Vector3 floorPoint)
    {
        isMoving = false;
        target.SetPositionAndRotation(GetRestingPosition(floorPoint), initialRotation);
        Physics.SyncTransforms();
        Debug.Log($"[QuestTableLab] Cube placed on floor at {target.position}.");
    }

    private Vector3 GetRestingPosition(Vector3 floorPoint)
    {
        return floorPoint + Vector3.up * targetCollider.bounds.extents.y;
    }

    private void CreatePlacementPreview()
    {
        placementPreview = GameObject.CreatePrimitive(PrimitiveType.Cube);
        placementPreview.name = "FloorPlacementPreview_Runtime";
        placementPreview.transform.localScale = target.lossyScale;

        var previewCollider = placementPreview.GetComponent<Collider>();
        if (previewCollider != null)
        {
            Destroy(previewCollider);
        }

        var targetRenderer = target.GetComponent<Renderer>();
        var previewRenderer = placementPreview.GetComponent<Renderer>();
        previewMaterial = new Material(targetRenderer.sharedMaterial)
        {
            name = "Floor Placement Preview (Runtime)",
            color = new Color(0.2f, 1f, 0.35f, 1f)
        };
        previewRenderer.material = previewMaterial;
        placementPreview.SetActive(false);
    }
}
