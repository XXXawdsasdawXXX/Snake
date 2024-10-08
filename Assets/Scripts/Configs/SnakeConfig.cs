﻿using System;
using Entities;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SnakeConfig", menuName = "Configs")]
    public class SnakeConfig : ScriptableObject
    {
        public SnakeSegment SegmentPrefab;
        public SnakeStaticData StaticData;
    }

    [Serializable]
    public class SnakeStaticData
    {
        public float Speed = 20f;
        public float BonusSpeedStep = 1f;
        public float MaxSpeed = 12f;
        public int InitialSize = 4;
        public bool MoveThroughWalls;
    }
}