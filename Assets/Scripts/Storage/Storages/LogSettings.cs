using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "LogSettings", menuName = "MyAssets/Settings/LogSettings")]
    public class LogSettings : Storage
    {
        public Vector3 Rotation;
        public float RotationMultiplier;

        public Transform Log;

        public float KnifeRadius;
    }
}