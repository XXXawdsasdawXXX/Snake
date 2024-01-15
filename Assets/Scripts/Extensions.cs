using UnityEngine;

namespace DefaultNamespace
{
    public static class Extensions
    {
        public static Vector3 AsVector3(this Vector2Int vector) => 
            new Vector3(vector.x, vector.y, 0);

        public static Vector2Int AsVector2Int(this Vector3 vector) =>
            new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }
}