using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "KnifesSettings", menuName = "MyAssets/Settings/KnifeSettings")]
    public class KnifesSettings : Storage
    {
        public Transform Knife;

        public float MaxAngularVelocity;

        public Vector3 ForceDirection;
        public float ForceMultiplier;

        public Vector3 RicochetForceMin;
        public Vector3 RicochetForceMax;

        public Vector3 RicochetSpin;
        public float RicochetSpinMultiplier;        

        public float KnifeThrowPeriod;

        public float KnifePositioningTime;
    }
}