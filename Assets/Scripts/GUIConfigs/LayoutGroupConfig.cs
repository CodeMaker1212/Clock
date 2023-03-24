using UnityEngine;

namespace Clock
{
    [CreateAssetMenu(fileName = "Layout Group Config")]

    public class LayoutGroupConfig : ScriptableObject
    {
        [SerializeField] private int _bottom;
        [SerializeField] private int _spacing;
        [SerializeField] private TextAnchor _childAlignment;

        public int Bottom => _bottom;
        public int Spacing => _spacing;
        public TextAnchor ChildAlignment => _childAlignment;
    }
}