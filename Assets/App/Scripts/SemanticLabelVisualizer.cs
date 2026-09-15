using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

/// <summary>
/// Displays the semantic classification and bounds of MRUK room anchors.
/// The visualization is a diagnostic layer and never changes room content.
/// </summary>
public sealed class SemanticLabelVisualizer : MonoBehaviour
{
    [Header("Room data")]
    [SerializeField] private MRUK mruk;
    [SerializeField] private Transform viewer;
    [SerializeField] private SemanticLabelProfile profile;

    [Header("Input")]
    [SerializeField] private OVRInput.Controller controller = OVRInput.Controller.RTouch;
    [SerializeField] private OVRInput.Button toggleButton = OVRInput.Button.PrimaryThumbstick;
    [SerializeField] private bool visibleOnStart;

    [Header("Appearance")]
    [SerializeField, Min(0.001f)] private float lineWidth = 0.008f;
    [SerializeField, Min(0.001f)] private float labelSurfaceOffset = 0.025f;
    [SerializeField, Min(0.001f)] private float labelCharacterSize = 0.012f;

    public bool IsVisible { get; private set; }
    public int VisibleAnchorCount { get; private set; }
    public SemanticLabelProfile Profile => profile;

    private readonly List<Transform> labels = new();
    private GameObject visualizationRoot;
    private Material lineMaterial;
    private bool eventsRegistered;

    private void Awake()
    {
        ResolveReferences();
        IsVisible = visibleOnStart;
    }

    private void OnEnable()
    {
        ResolveReferences();
        RegisterEvents();

        if (IsVisible)
        {
            RebuildCurrentRoom();
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(toggleButton, controller))
        {
            SetVisible(!IsVisible);
        }
    }

    private void LateUpdate()
    {
        if (!IsVisible || viewer == null)
        {
            return;
        }

        foreach (Transform label in labels)
        {
            if (label == null)
            {
                continue;
            }

            Vector3 awayFromViewer = label.position - viewer.position;
            if (awayFromViewer.sqrMagnitude > 0.0001f)
            {
                label.rotation = Quaternion.LookRotation(awayFromViewer.normalized, Vector3.up);
            }
        }
    }

    private void OnDisable()
    {
        UnregisterEvents();
        ClearVisualization();
    }

    private void OnDestroy()
    {
        if (lineMaterial != null)
        {
            Destroy(lineMaterial);
        }
    }

    public void SetVisible(bool visible)
    {
        IsVisible = visible;
        if (visible)
        {
            RebuildCurrentRoom();
            Debug.Log($"[QuestTableLab] Semantic labels enabled ({VisibleAnchorCount} anchors). Right stick click hides them.");
        }
        else
        {
            ClearVisualization();
            Debug.Log("[QuestTableLab] Semantic labels hidden. Right stick click shows them.");
        }
    }

    public void ShowRoom(MRUKRoom room)
    {
        IsVisible = true;
        BuildVisualization(room);
    }

    private void RebuildCurrentRoom()
    {
        ResolveReferences();
        BuildVisualization(mruk != null ? mruk.GetCurrentRoom() : null);
    }

    private void BuildVisualization(MRUKRoom room)
    {
        ClearVisualization();
        if (!IsVisible || room == null)
        {
            return;
        }

        visualizationRoot = new GameObject("SemanticLabelVisualization_Runtime");
        foreach (MRUKAnchor anchor in room.Anchors)
        {
            if (anchor == null || anchor.Label == MRUKAnchor.SceneLabels.GLOBAL_MESH)
            {
                continue;
            }

            CreateAnchorVisualization(anchor, visualizationRoot.transform);
            VisibleAnchorCount++;
        }
    }

    private void CreateAnchorVisualization(MRUKAnchor anchor, Transform parent)
    {
        Color color = profile != null ? profile.GetColor(anchor.Label) : Color.white;
        GameObject anchorRoot = new($"SemanticLabel_{anchor.Label}");
        anchorRoot.transform.SetParent(parent, false);

        if (anchor.VolumeBounds.HasValue)
        {
            AddVolumeOutline(anchor, anchor.VolumeBounds.Value, anchorRoot.transform, color);
        }
        else if (anchor.PlaneRect.HasValue)
        {
            AddPlaneOutline(anchor, anchor.PlaneRect.Value, anchorRoot.transform, color);
        }

        AddLabel(anchor, anchorRoot.transform, color);
    }

    private void AddVolumeOutline(MRUKAnchor anchor, Bounds bounds, Transform parent, Color color)
    {
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        Vector3[] corners =
        {
            new(min.x, min.y, min.z), new(max.x, min.y, min.z),
            new(max.x, max.y, min.z), new(min.x, max.y, min.z),
            new(min.x, min.y, max.z), new(max.x, min.y, max.z),
            new(max.x, max.y, max.z), new(min.x, max.y, max.z)
        };
        int[,] edges =
        {
            { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 },
            { 4, 5 }, { 5, 6 }, { 6, 7 }, { 7, 4 },
            { 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 }
        };

        for (int edge = 0; edge < edges.GetLength(0); edge++)
        {
            AddLine(
                parent,
                anchor.transform.TransformPoint(corners[edges[edge, 0]]),
                anchor.transform.TransformPoint(corners[edges[edge, 1]]),
                color);
        }
    }

    private void AddPlaneOutline(MRUKAnchor anchor, Rect rect, Transform parent, Color color)
    {
        Vector3[] corners =
        {
            anchor.transform.TransformPoint(new Vector3(rect.xMin, rect.yMin, 0f)),
            anchor.transform.TransformPoint(new Vector3(rect.xMax, rect.yMin, 0f)),
            anchor.transform.TransformPoint(new Vector3(rect.xMax, rect.yMax, 0f)),
            anchor.transform.TransformPoint(new Vector3(rect.xMin, rect.yMax, 0f))
        };

        for (int edge = 0; edge < corners.Length; edge++)
        {
            AddLine(parent, corners[edge], corners[(edge + 1) % corners.Length], color);
        }
    }

    private void AddLine(Transform parent, Vector3 start, Vector3 end, Color color)
    {
        GameObject lineObject = new("Outline");
        lineObject.transform.SetParent(parent, false);
        LineRenderer line = lineObject.AddComponent<LineRenderer>();
        line.useWorldSpace = true;
        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.startColor = color;
        line.endColor = color;
        line.numCapVertices = 2;
        line.sharedMaterial = GetLineMaterial();
        line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        line.receiveShadows = false;
    }

    private void AddLabel(MRUKAnchor anchor, Transform parent, Color color)
    {
        GameObject labelObject = new("Label");
        labelObject.transform.SetParent(parent, false);
        Vector3 center = anchor.VolumeBounds.HasValue
            ? anchor.GetAnchorCenter()
            : anchor.transform.TransformPoint(new Vector3(
                anchor.PlaneRect?.center.x ?? 0f,
                anchor.PlaneRect?.center.y ?? 0f,
                0f));
        labelObject.transform.position = center + anchor.transform.forward * labelSurfaceOffset;

        TextMesh text = labelObject.AddComponent<TextMesh>();
        text.text = profile != null
            ? profile.GetDisplayName(anchor.Label)
            : anchor.Label.ToString().Replace(", ", "\n");
        text.anchor = TextAnchor.MiddleCenter;
        text.alignment = TextAlignment.Center;
        text.fontSize = 64;
        text.characterSize = labelCharacterSize;
        text.color = color;
        text.richText = false;
        labels.Add(labelObject.transform);
    }

    private Material GetLineMaterial()
    {
        if (lineMaterial != null)
        {
            return lineMaterial;
        }

        Shader shader = Shader.Find("Sprites/Default") ?? Shader.Find("Universal Render Pipeline/Unlit");
        lineMaterial = new Material(shader)
        {
            name = "Semantic Label Lines (Runtime)"
        };
        return lineMaterial;
    }

    private void HandleSceneChanged()
    {
        if (IsVisible)
        {
            RebuildCurrentRoom();
        }
    }

    private void HandleRoomChanged(MRUKRoom room)
    {
        if (IsVisible)
        {
            BuildVisualization(room);
        }
    }

    private void RegisterEvents()
    {
        if (mruk == null || eventsRegistered)
        {
            return;
        }

        mruk.SceneLoadedEvent.AddListener(HandleSceneChanged);
        mruk.RoomCreatedEvent.AddListener(HandleRoomChanged);
        mruk.RoomUpdatedEvent.AddListener(HandleRoomChanged);
        eventsRegistered = true;
    }

    private void UnregisterEvents()
    {
        if (mruk == null || !eventsRegistered)
        {
            return;
        }

        mruk.SceneLoadedEvent.RemoveListener(HandleSceneChanged);
        mruk.RoomCreatedEvent.RemoveListener(HandleRoomChanged);
        mruk.RoomUpdatedEvent.RemoveListener(HandleRoomChanged);
        eventsRegistered = false;
    }

    private void ResolveReferences()
    {
        mruk ??= GetComponent<MRUK>();
        mruk ??= MRUK.Instance != null ? MRUK.Instance : FindFirstObjectByType<MRUK>();
        viewer ??= GameObject.Find("CenterEyeAnchor")?.transform;
    }

    private void ClearVisualization()
    {
        labels.Clear();
        VisibleAnchorCount = 0;
        if (visualizationRoot != null)
        {
            Destroy(visualizationRoot);
            visualizationRoot = null;
        }
    }
}
