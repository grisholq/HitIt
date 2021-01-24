using System;
using UnityEngine;

namespace HitIt.Ecs
{
    [Serializable]
    public class LogRotationNode
    {
        public Vector3 Rotation;
        public float Multiplier;
        public float Time;
        public LogRotationType Type;
    }
}