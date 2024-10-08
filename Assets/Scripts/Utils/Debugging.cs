﻿using System;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class Debugging : MonoBehaviour
    {
        public static Debugging Instance;

        public enum Type
        {
            None,
            Camera,
            Snake,
            Input,
            UI,
            JS,
            GameController,
            Health,
            Audio
        }

        [Serializable]
        private class DebugParam
        {
            public Type Type;
            public bool Active = true;
            public Color Color = Color.white;
        }

        [SerializeField] private DebugParam[] _debugParams;

        private void Awake()
        {
            Instance = this;
        }

        public void Log(string message, Type type = Type.None)
        {
            var debugParam = _debugParams.FirstOrDefault(d => d.Type == type);
            if (debugParam != null)
            {
                if (debugParam.Active)
                {
                    ColorLog($"{type.ToString().ToUpper()}: {message}", debugParam.Color);
                }
            }
            else
            {
                ColorLog(message, Color.white);
            }
        }

        public void TestLog(string message)
        {
            ColorLog(message, Color.green);
        }

        public void ErrorLog(string message)
        {
            ColorLog(message, Color.red);
        }

        private void ColorLog(string message, Color color)
        {
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>" + message + "</color>");
        }
    }
}