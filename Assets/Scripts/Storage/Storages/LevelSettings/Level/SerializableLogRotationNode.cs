using System;
using UnityEngine;
using HitIt.Ecs;

namespace HitIt.Storage
{
    [Serializable]
    public class SerializableLogRotationNode
    {
        public Vector3 Rotation;
        public float Multiplier;
        public float Time;
        public LogRotationType Type;
    }
}