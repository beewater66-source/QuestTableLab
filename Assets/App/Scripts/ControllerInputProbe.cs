using UnityEngine;

/// <summary>
/// First isolated Quest controller check: shows a ray from the right controller
/// and changes its colour while the index trigger is pressed.
/// </summary>
public sealed class ControllerInputProbe : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller controller = OVRInput.Controller.RTouch;
    [SerializeField, Min(0.25f)] private float rayLength = 3f;
    [SerializeField, Min(0f)] private float rayStartOffset = 0.06f;
    [SerializeField] private float floorHeight = 0f;
    [SerializeField, Min(0.001f)] private float rayWidth = 0.006f;
    [SerializeField] private Color idleColour = new(0.1f, 0.8f, 1f, 1f);
    [SerializeField] private Color triggerColour = new(1f, 0.35f, 0.1f, 1f);

    private LineRenderer controllerRay;
    private GameObject rayTip;
    private Material rayMaterial;

    public bool IsControllerActive { get; private set; }
    public float RayLength => rayLength;
    public OVRInput.Controller Controller => controller;
    public Ray AimRay => new(
        transform.position + transform.forward * rayStartOffset,
        transform.forward);

    private void Awake()
    {
        CreateRayVisual();
        SetVisualState(false);
    }

    private void Update()
    {
        var isRightControllerActive = OVRInput.GetActiveControllerForHand(OVRInput.Handedness.RightHanded)
            == controller;
        var isTracked = isRightControllerActive
            && OVRInput.GetControllerPositionTracked(controller)
            && OVRInput.GetControllerOrientationTracked(controller);

        IsControllerActive = isTracked;

        controllerRay.enabled = isTracked;
        rayTip.SetActive(isTracked);

        if (!isTracked)
        {
            return;
        }

        // This object is a direct child of TrackingSpace, so the raw local
        // controller pose maps to the same tracked coordinate system.
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);

        var visibleLength = rayLength;
        if (Physics.Raycast(AimRay, out var hit, rayLength, ~0, QueryTriggerInteraction.Ignore))
        {
            visibleLength = hit.distance;
        }

        if (TryGetFloorPoint(out _, out var floorDistance))
        {
            visibleLength = Mathf.Min(visibleLength, floorDistance);
        }

        var localRayEnd = Vector3.forward * (rayStartOffset + visibleLength);
        controllerRay.SetPosition(1, localRayEnd);
        rayTip.transform.localPosition = localRayEnd;

        var triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controller);
        SetVisualState(triggerPressed);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            Debug.Log("[QuestTableLab] Right controller index trigger pressed.");
        }
    }

    public bool TryGetFloorPoint(out Vector3 point, out float distance)
    {
        var floorPlane = new Plane(Vector3.up, new Vector3(0f, floorHeight, 0f));
        if (AimRay.direction.y < -0.001f
            && floorPlane.Raycast(AimRay, out distance)
            && distance <= rayLength)
        {
            point = AimRay.GetPoint(distance);
            return true;
        }

        point = default;
        distance = 0f;
        return false;
    }

    private void OnDestroy()
    {
        if (rayMaterial != null)
        {
            Destroy(rayMaterial);
        }
    }

    private void CreateRayVisual()
    {
        controllerRay = gameObject.AddComponent<LineRenderer>();
        controllerRay.useWorldSpace = false;
        controllerRay.positionCount = 2;
        controllerRay.SetPosition(0, Vector3.forward * rayStartOffset);
        controllerRay.SetPosition(1, Vector3.forward * (rayStartOffset + rayLength));
        controllerRay.startWidth = rayWidth;
        controllerRay.endWidth = rayWidth * 0.55f;
        controllerRay.numCapVertices = 4;
        controllerRay.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        controllerRay.receiveShadows = false;

        // URP/Lit is already referenced by the scene's cube and therefore
        // included in the Android build. A dynamically found, stripped shader
        // would show up as a magenta error ray on the Quest.
        var shader = Shader.Find("Universal Render Pipeline/Lit");
        rayMaterial = new Material(shader)
        {
            name = "Controller Input Probe (Runtime)"
        };
        controllerRay.material = rayMaterial;

        rayTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rayTip.name = "ControllerRayTip_Runtime";
        rayTip.transform.SetParent(transform, false);
        rayTip.transform.localPosition = Vector3.forward * (rayStartOffset + rayLength);
        rayTip.transform.localScale = Vector3.one * 0.025f;

        var tipCollider = rayTip.GetComponent<Collider>();
        if (tipCollider != null)
        {
            Destroy(tipCollider);
        }

        rayTip.GetComponent<Renderer>().material = rayMaterial;
    }

    private void SetVisualState(bool triggerPressed)
    {
        var colour = triggerPressed ? triggerColour : idleColour;
        controllerRay.startColor = colour;
        controllerRay.endColor = colour;
        rayMaterial.color = colour;
    }
}
