using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {
        public static Vector3 AsVector3(this Vector2Int vector) => 
            new Vector3(vector.x, vector.y, 0);

        public static Vector2 AsVector2(this Vector3 vector) => 
            new Vector2(vector.x, vector.y);
        public static Vector2Int AsVector2Int(this Vector3 vector) =>
            new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
        
        
        public static List<Vector2> AsListVector2(this List<Vector3> vectors)
        {
            var listVector2 = vectors.Select(vector => vector.AsVector2()).ToList();
            return listVector2;
        }
    }
}