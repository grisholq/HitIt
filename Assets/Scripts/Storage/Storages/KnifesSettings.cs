﻿using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "KnifesSettings", menuName = "MyAssets/Settings/KnifeSettings")]
    public class KnifesSettings : Storage
    {
        public Transform Knife;

        public float MaxKnifeAngularVelocity;

        public Vector3 ThrowForceDirection;
        public float ThrowForceMultiplier;

        public Vector3 RicochetForceMin;
        public Vector3 RicochetForceMax;
       
        public Vector3 RandomForceMin;
        public Vector3 RandomForceMax;

        public Vector3 RandomTorqueMin;
        public Vector3 RandomTorqueMax;

        public Vector3 RicochetSpin;
        public float RicochetSpinMultiplier;        

        public float KnifeThrowPeriod;

        public float KnifePositioningTime;

        public int KnifesOnLevel;
    }
}