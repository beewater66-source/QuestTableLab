using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Connects one virtual object to a semantic profile entry.
/// The optional icon and prefab fields in the profile can support richer content later.
/// </summary>
public sealed class SemanticContentBinding : MonoBehaviour
{
    [SerializeField] private SemanticLabelProfile profile;
    [SerializeField] private MRUKAnchor.SceneLabels semanticLabel = MRUKAnchor.SceneLabels.TABLE;
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Image targetIcon;
    [SerializeField] private bool applyOnStart = true;

    public SemanticLabelProfile Profile => profile;
    public MRUKAnchor.SceneLabels SemanticLabel => semanticLabel;

    private void Start()
    {
        if (applyOnStart)
        {
            ApplyProfile();
        }
    }

    public bool ApplyProfile()
    {
        if (profile == null || !profile.TryGetEntry(semanticLabel, out SemanticLabelProfile.Entry entry))
        {
            return false;
        }

        targetRenderer ??= GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            MaterialPropertyBlock properties = new();
            targetRenderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", entry.Color);
            properties.SetColor("_Color", entry.Color);
            targetRenderer.SetPropertyBlock(properties);
        }

        if (targetIcon != null)
        {
            targetIcon.color = entry.Color;
            if (entry.Icon != null)
            {
                targetIcon.sprite = entry.Icon;
            }
        }

        return targetRenderer != null || targetIcon != null;
    }
}
