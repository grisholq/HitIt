using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "KnifesSettings", menuName = "MyAssets/Settings/KnifeSettings")]
    public class KnifesSettings : Storage
    {
        public Transform Knife;
        public Vector3 ForceDirection;
        public float ForceMultiplier;

        public float KnifeThrowPeriod;
    }
}