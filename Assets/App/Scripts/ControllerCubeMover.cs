using UnityEngine;

/// <summary>
/// Lets the user remotely move the test cube with the right controller ray.
/// Hold the index trigger while pointing at the cube and release to leave it
/// at the new room-fixed position.
/// </summary>
public sealed class ControllerCubeMover : MonoBehaviour
{
    private enum MovingTarget
    {
        None,
        Cube,
        WallUi
    }

    [SerializeField] private ControllerInputProbe controllerAim;
    [SerializeField] private Transform target;
    [SerializeField] private string fallbackTargetName = "RoomFixedTestCube";
    [SerializeField] private RectTransform wallTarget;
    [SerializeField] private SemanticTablePlacementController semanticPlacement;

    private Collider targetCollider;
    private Collider wallTargetCollider;
    private MovingTarget movingTarget;
    private float grabDistance;
    private Vector3 grabOffset;
    private Vector2 wallGrabOffset;
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
        semanticPlacement ??= FindFirstObjectByType<SemanticTablePlacementController>();
        wallTarget ??= GameObject.Find("HelloPanel")?.GetComponent<RectTransform>();
        wallTargetCollider = wallTarget != null ? wallTarget.GetComponent<Collider>() : null;

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
            movingTarget = MovingTarget.None;
            placementPreview.SetActive(false);
            return;
        }

        var controller = controllerAim.Controller;
        var aimRay = controllerAim.AimRay;
        var hasFloorPoint = controllerAim.TryGetFloorPoint(out var floorPoint, out var floorDistance);
        bool hitsCube = targetCollider.Raycast(aimRay, out var cubeHit, controllerAim.RayLength);
        RaycastHit wallHit = default;
        bool hitsWallUi = wallTargetCollider != null
            && wallTargetCollider.Raycast(aimRay, out wallHit, controllerAim.RayLength);
        bool hasInteractableHit = hitsCube || hitsWallUi;
        float nearestInteractableDistance = Mathf.Min(
            hitsCube ? cubeHit.distance : float.PositiveInfinity,
            hitsWallUi ? wallHit.distance : float.PositiveInfinity);
        var interactableHit = hitsWallUi && (!hitsCube || wallHit.distance < cubeHit.distance)
            ? MovingTarget.WallUi
            : MovingTarget.Cube;
        bool hitsInteractableFirst = hasInteractableHit
            && (!hasFloorPoint || nearestInteractableDistance < floorDistance);

        placementPreview.SetActive(hasFloorPoint && !hitsInteractableFirst && movingTarget == MovingTarget.None);
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

        if (movingTarget == MovingTarget.None
            && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller)
            && hitsInteractableFirst)
        {
            movingTarget = interactableHit;
            placementPreview.SetActive(false);
            RaycastHit selectedHit = movingTarget == MovingTarget.Cube ? cubeHit : wallHit;
            grabDistance = selectedHit.distance;
            grabOffset = target.position - selectedHit.point;
            if (movingTarget == MovingTarget.WallUi && semanticPlacement != null)
            {
                wallGrabOffset = semanticPlacement.GetWallLocalOffset(wallTarget.position, selectedHit.point);
            }

            Debug.Log(movingTarget == MovingTarget.Cube
                ? "[QuestTableLab] Cube movement started."
                : "[QuestTableLab] Wall UI movement started.");
        }

        else if (movingTarget == MovingTarget.None
            && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller)
            && hasFloorPoint)
        {
            PlaceTargetOnFloor(floorPoint);
            return;
        }

        if (movingTarget == MovingTarget.None)
        {
            return;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            if (movingTarget == MovingTarget.Cube)
            {
                float footprintRadius = Mathf.Max(targetCollider.bounds.extents.x, targetCollider.bounds.extents.z);
                float halfHeight = targetCollider.bounds.extents.y;
                if (semanticPlacement == null || !semanticPlacement.HasSelectedTable)
                {
                    target.position = aimRay.GetPoint(grabDistance) + grabOffset;
                }
                else if (semanticPlacement.TryGetTableConstrainedCubePosition(
                        aimRay,
                        grabOffset,
                        footprintRadius,
                        halfHeight,
                        out Vector3 constrainedPosition))
                {
                    target.position = constrainedPosition;
                }
            }
            else if (semanticPlacement != null
                     && semanticPlacement.TryGetWallConstrainedUiPose(
                         aimRay,
                         wallGrabOffset,
                         out Vector3 wallPosition,
                         out Quaternion wallRotation))
            {
                wallTarget.SetPositionAndRotation(wallPosition, wallRotation);
            }

            return;
        }

        MovingTarget finishedTarget = movingTarget;
        movingTarget = MovingTarget.None;
        Physics.SyncTransforms();
        Debug.Log(finishedTarget == MovingTarget.Cube
            ? $"[QuestTableLab] Cube movement finished at {target.position}."
            : $"[QuestTableLab] Wall UI movement finished at {wallTarget.position}.");
    }

    public void ResetTarget()
    {
        movingTarget = MovingTarget.None;
        target.SetPositionAndRotation(initialPosition, initialRotation);
        semanticPlacement?.ResetWallUi();
        Physics.SyncTransforms();
        Debug.Log("[QuestTableLab] Cube and wall UI reset to their semantic poses.");
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
            movingTarget = MovingTarget.None;
            target.SetPositionAndRotation(initialPosition, initialRotation);
            Physics.SyncTransforms();
        }
    }

    public void PlaceTargetOnFloor(Vector3 floorPoint)
    {
        movingTarget = MovingTarget.None;
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
