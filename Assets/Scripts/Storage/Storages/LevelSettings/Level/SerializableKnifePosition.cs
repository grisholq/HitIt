using System;
using UnityEngine;

namespace HitIt.Storage
{
    [Serializable]
    public class SerializalbeKnifePosition
    {
        [Range(0f, 360f)] public float Angle;
    }
}
