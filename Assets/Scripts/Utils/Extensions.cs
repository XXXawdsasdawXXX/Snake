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
        
        public static void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
        
        public static void ShuffleArray<T>(T[] array)
        {
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }
    }
}