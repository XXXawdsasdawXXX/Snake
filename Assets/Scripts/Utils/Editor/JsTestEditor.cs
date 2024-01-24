using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
    [CustomEditor(typeof(JsTest))]
    public class JsTestEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            JsTest test = (JsTest)target;

            if (GUILayout.Button("Start test session"))
            {
                test.StartTestSession();
            }
        }
    }
    
}