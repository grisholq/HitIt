using UnityEngine;
using HitIt.Ecs;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "LogSettings", menuName = "MyAssets/Settings/LogSettings")]
    public class LogSettings : Storage
    {
        public Vector3 Rotation;
        public float RotationMultiplier;
        
        public float KnifeRadius;

        public float LogExplosionForce;
        public float LogExplosionRadius;

        public int KnifesToAttach;

        public Transform Apple;
        
        public int ApplesToAttach;

        public float MaxAppleAngularVelocity;

       // public LogRotationPattern RotationPattern;

        public float AppleAttachRadius;
        public float KnifeAttachRadius;
    }
}