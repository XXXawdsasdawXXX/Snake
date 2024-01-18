using UnityEditor.UIElements;
using UnityEngine;

namespace Utils
{
    public static class Constants
    {
        public const int SEGMENT_COUNT = 4;
        public static Vector2Int DEFAULT_DIRECTION => Vector2Int.right;
        public enum Tag
        {
            Untagget,
            Food,
            Obstacle,
            Wall
        }
    }
}