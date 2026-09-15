using System;
using System.Threading.Tasks;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine.Android;
#endif

/// <summary>
/// Loads the Quest Scene API room through MRUK, finds the nearest TABLE
/// volume, and places the test cube on its top surface.
/// </summary>
public sealed class SemanticTablePlacementController : MonoBehaviour
{
    public enum PlacementState
    {
        Idle,
        WaitingForPermission,
        LoadingRoom,
        TableFound,
        PermissionMissing,
        NoRoomSetup,
        NoTableFound,
        Failed,
        EditorPreview
    }

    [Header("Scene API")]
    [SerializeField] private MRUK mruk;
    [SerializeField] private bool loadAutomatically = true;
    [SerializeField, Min(1f)] private float permissionTimeoutSeconds = 20f;

    [Header("Placement")]
    [SerializeField] private Transform target;
    [SerializeField] private ControllerCubeMover cubeMover;
    [SerializeField] private Transform viewer;
    [SerializeField, Min(0f)] private float surfaceClearance = 0.002f;

    [Header("Wall UI")]
    [SerializeField] private RectTransform wallUi;
    [SerializeField] private Vector2 wallOffsetMeters = Vector2.zero;
    [SerializeField, Min(0f)] private float wallSurfaceDistance = 0.025f;
    [SerializeField, Min(0f)] private float wallEdgePadding = 0.03f;

    [Header("Diagnostics")]
    [SerializeField] private Text statusText;
    [SerializeField] private OVROverlayCanvas statusOverlay;

    public PlacementState State { get; private set; } = PlacementState.Idle;
    public MRUKAnchor SelectedTable { get; private set; }
    public Vector3 SelectedTableTop { get; private set; }
    public MRUKAnchor SelectedWall { get; private set; }
    public Vector3 SelectedWallPosition { get; private set; }
    public bool HasSelectedTable => SelectedTable != null && SelectedTable.VolumeBounds.HasValue;
    public bool HasSelectedWall => SelectedWall != null && SelectedWall.PlaneRect.HasValue;

    private Collider targetCollider;
    private Quaternion selectedWallRotation;

    private async void Start()
    {
        ResolveReferences();

        if (!ValidateReferences() || !loadAutomatically)
        {
            return;
        }

#if UNITY_EDITOR
        SetStatus(
            PlacementState.EditorPreview,
            "Scene API: Gerätetest erforderlich\nIm Editor bleiben Würfel und Schild an ihrer Testposition.");
#else
        await LoadAndPlaceAsync();
#endif
    }

    /// <summary>
    /// Can be called from a UI button or a later retry interaction.
    /// </summary>
    public async void Retry()
    {
        ResolveReferences();
        if (ValidateReferences())
        {
            await LoadAndPlaceAsync();
        }
    }

    private async Task LoadAndPlaceAsync()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (!Permission.HasUserAuthorizedPermission(OVRPermissionsRequester.ScenePermission))
        {
            SetStatus(PlacementState.WaitingForPermission, "Scene API: Warte auf Raumfreigabe ...");
            float deadline = Time.realtimeSinceStartup + permissionTimeoutSeconds;
            while (!Permission.HasUserAuthorizedPermission(OVRPermissionsRequester.ScenePermission)
                   && Time.realtimeSinceStartup < deadline)
            {
                await Task.Yield();
            }

            if (!Permission.HasUserAuthorizedPermission(OVRPermissionsRequester.ScenePermission))
            {
                SetStatus(
                    PlacementState.PermissionMissing,
                    "Scene API: Raumfreigabe fehlt\nBitte die Berechtigung in den Quest-Einstellungen erlauben.");
                return;
            }
        }
#endif

        SetStatus(PlacementState.LoadingRoom, "Scene API: Lade gespeicherten Raum ...");

        MRUK.LoadDeviceResult result;
        try
        {
            result = await mruk.LoadSceneFromDevice(
                requestSceneCaptureIfNoDataFound: true,
                removeMissingRooms: true,
                sceneModel: MRUK.SceneModel.V2FallbackV1);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            SetStatus(PlacementState.Failed, "Scene API: Unerwarteter Ladefehler\nDetails stehen im Unity-Log.");
            return;
        }

        if (result != MRUK.LoadDeviceResult.Success)
        {
            HandleLoadFailure(result);
            return;
        }

        PlaceUiOnBestWall();
        PlaceOnNearestTable();
    }

    public bool PlaceUiOnBestWall()
    {
        MRUKRoom room = mruk != null ? mruk.GetCurrentRoom() : null;
        if (room == null || wallUi == null)
        {
            return false;
        }

        Vector3 viewerPosition = viewer != null ? viewer.position : wallUi.position;
        Vector3 viewerForward = viewer != null ? viewer.forward : Vector3.forward;
        if (!TryFindBestWall(room, viewerPosition, viewerForward, out MRUKAnchor wall))
        {
            Debug.LogWarning("[QuestTableLab] No suitable WALL_FACE plane found for the status UI.");
            return false;
        }

        return PlaceUiOnWall(wall);
    }

    public bool TryGetTableConstrainedCubePosition(
        Ray controllerRay,
        Vector3 worldGrabOffset,
        float footprintRadius,
        float halfHeight,
        out Vector3 position)
    {
        position = default;
        if (SelectedTable == null || !SelectedTable.VolumeBounds.HasValue)
        {
            return false;
        }

        Plane tablePlane = new(Vector3.up, SelectedTableTop);
        if (!tablePlane.Raycast(controllerRay, out float distance) || distance < 0f)
        {
            return false;
        }

        Vector3 requestedWorldPosition = controllerRay.GetPoint(distance) + worldGrabOffset;
        Vector3 requestedLocalPosition = SelectedTable.transform.InverseTransformPoint(requestedWorldPosition);
        Bounds bounds = SelectedTable.VolumeBounds.Value;
        float xInset = Mathf.Min(footprintRadius, bounds.size.x * 0.5f);
        float yInset = Mathf.Min(footprintRadius, bounds.size.y * 0.5f);
        float x = Mathf.Clamp(requestedLocalPosition.x, bounds.min.x + xInset, bounds.max.x - xInset);
        float y = Mathf.Clamp(requestedLocalPosition.y, bounds.min.y + yInset, bounds.max.y - yInset);

        Vector3 surfacePoint = SelectedTable.transform.TransformPoint(new Vector3(x, y, 0f));
        position = surfacePoint + Vector3.up * (halfHeight + surfaceClearance);
        return true;
    }

    public Vector2 GetWallLocalOffset(Vector3 uiCenter, Vector3 grabbedPoint)
    {
        if (SelectedWall == null)
        {
            return Vector2.zero;
        }

        Vector3 localCenter = SelectedWall.transform.InverseTransformPoint(uiCenter);
        Vector3 localGrab = SelectedWall.transform.InverseTransformPoint(grabbedPoint);
        return new Vector2(localCenter.x - localGrab.x, localCenter.y - localGrab.y);
    }

    public bool TryGetWallConstrainedUiPose(
        Ray controllerRay,
        Vector2 localGrabOffset,
        out Vector3 position,
        out Quaternion rotation)
    {
        position = default;
        rotation = default;
        if (SelectedWall == null || !SelectedWall.PlaneRect.HasValue || wallUi == null)
        {
            return false;
        }

        Plane wallPlane = new(SelectedWall.transform.forward, SelectedWall.transform.position);
        if (!wallPlane.Raycast(controllerRay, out float distance) || distance < 0f)
        {
            return false;
        }

        Vector3 localHit = SelectedWall.transform.InverseTransformPoint(controllerRay.GetPoint(distance));
        Rect wallRect = SelectedWall.PlaneRect.Value;
        Vector2 requestedOffset = new(
            localHit.x + localGrabOffset.x - wallRect.center.x,
            localHit.y + localGrabOffset.y - wallRect.center.y);
        CalculateWallUiPose(
            SelectedWall,
            requestedOffset,
            wallSurfaceDistance,
            wallEdgePadding,
            GetWallUiHalfSize(),
            out position,
            out rotation);
        return true;
    }

    public void ResetWallUi()
    {
        if (wallUi == null || SelectedWall == null)
        {
            return;
        }

        wallUi.SetPositionAndRotation(SelectedWallPosition, selectedWallRotation);
        statusOverlay?.SetFrameDirty();
    }

    public bool PlaceOnNearestTable()
    {
        MRUKRoom room = mruk != null ? mruk.GetCurrentRoom() : null;
        if (room == null)
        {
            SetStatus(
                PlacementState.NoRoomSetup,
                "Scene API: Kein Raum gefunden\nBitte das Space Setup auf der Quest abschließen.");
            return false;
        }

        Vector3 viewerPosition = viewer != null ? viewer.position : target.position;
        if (!TryFindNearestTable(room, viewerPosition, out MRUKAnchor table, out Vector3 tableTop))
        {
            SetStatus(
                PlacementState.NoTableFound,
                "Scene API: Kein TABLE-Label gefunden\nBitte im Space Setup einen Tisch markieren.");
            return false;
        }

        return PlaceOnTable(table, tableTop);
    }

    public static bool TryFindNearestTable(
        MRUKRoom room,
        Vector3 viewerPosition,
        out MRUKAnchor nearestTable,
        out Vector3 tableTop)
    {
        nearestTable = null;
        tableTop = default;

        if (room == null)
        {
            return false;
        }

        float bestDistance = float.PositiveInfinity;
        foreach (MRUKAnchor anchor in room.Anchors)
        {
            if (anchor == null
                || !anchor.HasAnyLabel(MRUKAnchor.SceneLabels.TABLE)
                || !anchor.VolumeBounds.HasValue)
            {
                continue;
            }

            // MRUK defines the anchor transform position as the top center of a volume.
            Vector3 candidateTop = anchor.transform.position;
            Vector2 viewerXZ = new(viewerPosition.x, viewerPosition.z);
            Vector2 tableXZ = new(candidateTop.x, candidateTop.z);
            float horizontalDistance = Vector2.SqrMagnitude(viewerXZ - tableXZ);

            if (horizontalDistance < bestDistance)
            {
                bestDistance = horizontalDistance;
                nearestTable = anchor;
                tableTop = candidateTop;
            }
        }

        return nearestTable != null;
    }

    public bool TryAdoptTableFromRay(Ray controllerRay, float maxDistance)
    {
        MRUKRoom room = mruk != null ? mruk.GetCurrentRoom() : null;
        if (!TryRaycastTableTop(room, controllerRay, maxDistance, out MRUKAnchor table, out _))
        {
            return false;
        }

        if (table == SelectedTable)
        {
            return true;
        }

        SetSelectedTable(table, table.transform.position, moveTarget: false);
        UpdateSuccessStatus();
        Debug.Log($"[QuestTableLab] Cube adopted TABLE '{GetAnchorId(table)}' from controller ray.");
        return true;
    }

    public bool TryAdoptWallFromRay(Ray controllerRay, float maxDistance, out bool changed)
    {
        changed = false;
        MRUKRoom room = mruk != null ? mruk.GetCurrentRoom() : null;
        if (!TryRaycastWall(room, controllerRay, maxDistance, out MRUKAnchor wall, out _))
        {
            return false;
        }

        if (wall == SelectedWall)
        {
            return true;
        }

        SetSelectedWall(wall, moveTarget: false);
        UpdateSuccessStatus();
        changed = true;
        Debug.Log($"[QuestTableLab] Wall UI adopted WALL_FACE '{GetAnchorId(wall)}' from controller ray.");
        return true;
    }

    public static bool TryRaycastTableTop(
        MRUKRoom room,
        Ray ray,
        float maxDistance,
        out MRUKAnchor table,
        out RaycastHit hit)
    {
        table = null;
        hit = default;
        if (room == null
            || !room.Raycast(
                ray,
                maxDistance,
                new LabelFilter(MRUKAnchor.SceneLabels.TABLE, MRUKAnchor.ComponentType.Volume),
                out hit,
                out table)
            || table == null
            || !table.VolumeBounds.HasValue)
        {
            return false;
        }

        // Side faces do not represent a valid resting surface for the cube.
        return Vector3.Dot(hit.normal, Vector3.up) > 0.5f;
    }

    public static bool TryRaycastWall(
        MRUKRoom room,
        Ray ray,
        float maxDistance,
        out MRUKAnchor wall,
        out RaycastHit hit)
    {
        wall = null;
        hit = default;
        return room != null
               && room.Raycast(
                   ray,
                   maxDistance,
                   new LabelFilter(MRUKAnchor.SceneLabels.WALL_FACE, MRUKAnchor.ComponentType.Plane),
                   out hit,
                   out wall)
               && wall != null
               && wall.PlaneRect.HasValue;
    }

    public static bool TryFindBestWall(
        MRUKRoom room,
        Vector3 viewerPosition,
        Vector3 viewerForward,
        out MRUKAnchor bestWall)
    {
        bestWall = null;
        if (room == null)
        {
            return false;
        }

        Vector3 lookDirection = viewerForward.sqrMagnitude > 0.0001f
            ? viewerForward.normalized
            : Vector3.forward;
        float bestScore = float.NegativeInfinity;
        float bestFallbackDistance = float.PositiveInfinity;
        MRUKAnchor fallbackWall = null;

        foreach (MRUKAnchor wall in room.WallAnchors)
        {
            if (wall == null
                || !wall.HasAnyLabel(MRUKAnchor.SceneLabels.WALL_FACE)
                || !wall.PlaneRect.HasValue)
            {
                continue;
            }

            Vector3 center = wall.transform.TransformPoint(wall.PlaneRect.Value.center);
            Vector3 toWall = center - viewerPosition;
            float distance = toWall.magnitude;
            if (distance < 0.001f)
            {
                continue;
            }

            Vector3 direction = toWall / distance;
            bool facesViewer = Vector3.Dot(wall.transform.forward, -direction) > 0f;
            if (!facesViewer)
            {
                continue;
            }

            if (distance < bestFallbackDistance)
            {
                bestFallbackDistance = distance;
                fallbackWall = wall;
            }

            float alignment = Vector3.Dot(lookDirection, direction);
            if (alignment <= 0f)
            {
                continue;
            }

            // Direction dominates; distance only breaks ties between similarly visible walls.
            float score = alignment * 10f - distance * 0.01f;
            if (score > bestScore)
            {
                bestScore = score;
                bestWall = wall;
            }
        }

        bestWall ??= fallbackWall;
        return bestWall != null;
    }

    public static void CalculateWallUiPose(
        MRUKAnchor wall,
        Vector2 requestedOffset,
        float surfaceDistance,
        float edgePadding,
        Vector2 halfUiSize,
        out Vector3 position,
        out Quaternion rotation)
    {
        Rect plane = wall.PlaneRect.Value;
        float horizontalInset = Mathf.Min(halfUiSize.x + edgePadding, plane.width * 0.5f);
        float verticalInset = Mathf.Min(halfUiSize.y + edgePadding, plane.height * 0.5f);
        float x = Mathf.Clamp(
            plane.center.x + requestedOffset.x,
            plane.xMin + horizontalInset,
            plane.xMax - horizontalInset);
        float y = Mathf.Clamp(
            plane.center.y + requestedOffset.y,
            plane.yMin + verticalInset,
            plane.yMax - verticalInset);

        position = wall.transform.TransformPoint(new Vector3(x, y, 0f))
                   + wall.transform.forward * surfaceDistance;
        // MRUK's WALL_FACE normal points into the room. Unity world-space Canvas content
        // is viewed from the opposite side of its local forward axis, so the Canvas
        // forward direction must point back toward the wall.
        rotation = Quaternion.LookRotation(-wall.transform.forward, wall.transform.up);
    }

    private bool PlaceOnTable(MRUKAnchor table, Vector3 tableTop)
    {
        if (table == null || !table.VolumeBounds.HasValue)
        {
            return false;
        }

        Vector3 targetPosition = SetSelectedTable(table, tableTop, moveTarget: true);
        Debug.Log($"[QuestTableLab] TABLE '{GetAnchorId(table)}' selected. Cube placed at {targetPosition}.");
        UpdateSuccessStatus();
        return true;
    }

    private bool PlaceUiOnWall(MRUKAnchor wall)
    {
        if (wall == null || !wall.PlaneRect.HasValue || wallUi == null)
        {
            return false;
        }

        SetSelectedWall(wall, moveTarget: true);
        Debug.Log($"[QuestTableLab] WALL_FACE '{GetAnchorId(wall)}' selected. UI placed at {SelectedWallPosition}.");
        return true;
    }

    private Vector3 SetSelectedTable(MRUKAnchor table, Vector3 tableTop, bool moveTarget)
    {
        SelectedTable = table;
        SelectedTableTop = tableTop;
        float halfHeight = targetCollider != null ? targetCollider.bounds.extents.y : 0f;
        Vector3 targetPosition = tableTop + Vector3.up * (halfHeight + surfaceClearance);
        Quaternion targetRotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);

        if (cubeMover != null)
        {
            cubeMover.SetResetPose(targetPosition, targetRotation, moveTarget);
        }
        else if (moveTarget)
        {
            target.SetPositionAndRotation(targetPosition, targetRotation);
            Physics.SyncTransforms();
        }

        return targetPosition;
    }

    private void SetSelectedWall(MRUKAnchor wall, bool moveTarget)
    {
        CalculateWallUiPose(
            wall,
            wallOffsetMeters,
            wallSurfaceDistance,
            wallEdgePadding,
            GetWallUiHalfSize(),
            out Vector3 position,
            out Quaternion rotation);
        SelectedWall = wall;
        SelectedWallPosition = position;
        selectedWallRotation = rotation;

        if (moveTarget)
        {
            wallUi.SetPositionAndRotation(position, rotation);
            Physics.SyncTransforms();
            statusOverlay?.SetFrameDirty();
        }
    }

    private void UpdateSuccessStatus()
    {
        if (SelectedTable == null)
        {
            return;
        }

        string wallStatus = SelectedWall != null ? "WALL_FACE erkannt" : "keine WALL_FACE gefunden";
        SetStatus(
            PlacementState.TableFound,
            $"TABLE + {wallStatus}\nTrigger: Fläche wechseln | B: Reset\nR-Stick: Raumlabels");
    }

    private static string GetAnchorId(MRUKAnchor anchor) =>
        anchor.HasValidHandle ? anchor.Anchor.Uuid.ToString() : anchor.name;

    private void ResolveReferences()
    {
        mruk ??= MRUK.Instance != null ? MRUK.Instance : FindFirstObjectByType<MRUK>();

        if (target == null)
        {
            GameObject targetObject = GameObject.Find("RoomFixedTestCube");
            target = targetObject != null ? targetObject.transform : null;
        }

        cubeMover ??= FindFirstObjectByType<ControllerCubeMover>();
        viewer ??= GameObject.Find("CenterEyeAnchor")?.transform;
        statusText ??= GameObject.Find("Message")?.GetComponent<Text>();
        statusOverlay ??= statusText != null ? statusText.GetComponentInParent<OVROverlayCanvas>() : null;
        wallUi ??= statusText != null ? statusText.GetComponentInParent<Canvas>()?.GetComponent<RectTransform>() : null;
        targetCollider = target != null ? target.GetComponent<Collider>() : null;
    }

    private Vector2 GetWallUiHalfSize()
    {
        if (wallUi == null)
        {
            return Vector2.zero;
        }

        Vector3 scale = wallUi.lossyScale;
        return new Vector2(
            wallUi.rect.width * Mathf.Abs(scale.x) * 0.5f,
            wallUi.rect.height * Mathf.Abs(scale.y) * 0.5f);
    }

    private bool ValidateReferences()
    {
        if (mruk != null && target != null)
        {
            return true;
        }

        SetStatus(PlacementState.Failed, "Scene API: MRUK oder Zielwürfel fehlt.");
        Debug.LogError("[QuestTableLab] Semantic placement requires an MRUK component and target cube.");
        enabled = false;
        return false;
    }

    private void HandleLoadFailure(MRUK.LoadDeviceResult result)
    {
        switch (result)
        {
            case MRUK.LoadDeviceResult.NoScenePermission:
            case MRUK.LoadDeviceResult.FailurePermissionInsufficient:
                SetStatus(PlacementState.PermissionMissing, "Scene API: Raumfreigabe fehlt.");
                break;

            case MRUK.LoadDeviceResult.NoRoomsFound:
                SetStatus(
                    PlacementState.NoRoomSetup,
                    "Scene API: Kein Raum gespeichert\nBitte das Space Setup auf der Quest durchführen.");
                break;

            case MRUK.LoadDeviceResult.FailureTooDark:
                SetStatus(PlacementState.Failed, "Scene API: Raum ist zu dunkel\nBitte Licht einschalten und erneut versuchen.");
                break;

            case MRUK.LoadDeviceResult.FailureTooBright:
                SetStatus(PlacementState.Failed, "Scene API: Raum ist zu hell\nBitte Beleuchtung anpassen und erneut versuchen.");
                break;

            default:
                SetStatus(PlacementState.Failed, $"Scene API konnte nicht geladen werden\nFehler: {result}");
                break;
        }

        Debug.LogWarning($"[QuestTableLab] MRUK scene loading finished with {result}.");
    }

    private void SetStatus(PlacementState state, string message)
    {
        State = state;
        if (statusText != null)
        {
            statusText.resizeTextForBestFit = true;
            statusText.resizeTextMinSize = 32;
            statusText.resizeTextMaxSize = 64;
            statusText.text = message;
            statusOverlay?.SetFrameDirty();
        }
    }

    private static string FormatVector(Vector3 value) =>
        $"({value.x:F2}, {value.y:F2}, {value.z:F2})";
}
