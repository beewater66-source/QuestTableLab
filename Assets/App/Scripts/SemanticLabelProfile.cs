using System;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

/// <summary>
/// Central configuration that maps MRUK semantic labels to presentation and content.
/// Recognition code only deals with labels; projects can change this profile without rewriting it.
/// </summary>
[CreateAssetMenu(fileName = "SemanticLabelProfile", menuName = "QuestTableLab/Semantic Label Profile")]
public sealed class SemanticLabelProfile : ScriptableObject
{
    [Serializable]
    public sealed class Entry
    {
        [SerializeField] private MRUKAnchor.SceneLabels label;
        [SerializeField] private string displayName;
        [SerializeField] private Color color = Color.white;
        [SerializeField] private Sprite icon;
        [SerializeField] private GameObject contentPrefab;

        public MRUKAnchor.SceneLabels Label => label;
        public string DisplayName => string.IsNullOrWhiteSpace(displayName) ? label.ToString() : displayName;
        public Color Color => color;
        public Sprite Icon => icon;
        public GameObject ContentPrefab => contentPrefab;

        public Entry(MRUKAnchor.SceneLabels label, string displayName, Color color)
        {
            this.label = label;
            this.displayName = displayName;
            this.color = color;
        }
    }

    [SerializeField] private List<Entry> entries = new();
    [SerializeField] private Color fallbackColor = Color.white;

    public IReadOnlyList<Entry> Entries => entries;
    public Color FallbackColor => fallbackColor;

    public bool TryGetEntry(MRUKAnchor.SceneLabels labels, out Entry entry)
    {
        foreach (Entry candidate in entries)
        {
            if (candidate != null && candidate.Label == labels)
            {
                entry = candidate;
                return true;
            }
        }

        foreach (Entry candidate in entries)
        {
            if (candidate != null && (candidate.Label & labels) != 0)
            {
                entry = candidate;
                return true;
            }
        }

        entry = null;
        return false;
    }

    public Color GetColor(MRUKAnchor.SceneLabels labels) =>
        TryGetEntry(labels, out Entry entry) ? entry.Color : fallbackColor;

    public string GetDisplayName(MRUKAnchor.SceneLabels labels) =>
        TryGetEntry(labels, out Entry entry)
            ? entry.DisplayName
            : labels.ToString().Replace(", ", "\n");

    [ContextMenu("Apply QuestTableLab defaults")]
    public void ApplyRecommendedDefaults()
    {
        entries = new List<Entry>
        {
            new(MRUKAnchor.SceneLabels.TABLE, "TABLE", new Color(0.15f, 0.65f, 1f, 1f)),
            new(MRUKAnchor.SceneLabels.COUCH, "COUCH", new Color(0.2f, 1f, 0.4f, 1f)),
            new(MRUKAnchor.SceneLabels.SCREEN, "SCREEN", new Color(1f, 0.35f, 0.25f, 1f)),
            new(MRUKAnchor.SceneLabels.WALL_FACE, "WALL FACE", new Color(0.75f, 0.35f, 1f, 1f)),
            new(MRUKAnchor.SceneLabels.FLOOR, "FLOOR", new Color(0.2f, 1f, 0.9f, 1f)),
            new(MRUKAnchor.SceneLabels.CEILING, "CEILING", new Color(1f, 0.85f, 0.2f, 1f)),
            new(MRUKAnchor.SceneLabels.DOOR_FRAME, "DOOR", new Color(1f, 0.55f, 0.15f, 1f)),
            new(MRUKAnchor.SceneLabels.WINDOW_FRAME, "WINDOW", new Color(0.25f, 0.85f, 1f, 1f)),
            new(MRUKAnchor.SceneLabels.STORAGE, "STORAGE", new Color(0.75f, 0.55f, 0.25f, 1f)),
            new(MRUKAnchor.SceneLabels.BED, "BED", new Color(1f, 0.45f, 0.75f, 1f)),
            new(MRUKAnchor.SceneLabels.LAMP, "LAMP", new Color(1f, 0.95f, 0.35f, 1f)),
            new(MRUKAnchor.SceneLabels.PLANT, "PLANT", new Color(0.25f, 0.8f, 0.25f, 1f)),
            new(MRUKAnchor.SceneLabels.WALL_ART, "WALL ART", new Color(1f, 0.35f, 0.85f, 1f)),
            new(MRUKAnchor.SceneLabels.OTHER, "OTHER", Color.white)
        };
    }
}
