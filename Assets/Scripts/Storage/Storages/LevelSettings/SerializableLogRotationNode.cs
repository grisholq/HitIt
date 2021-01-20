using System;
using UnityEngine;

namespace HitIt.Ecs
{
    [Serializable]
    public class SerializableLogRotationNode
    {
        public Vector3 Rotation;
        public float Multiplier;
        public LogRotationType Type;
    }
}