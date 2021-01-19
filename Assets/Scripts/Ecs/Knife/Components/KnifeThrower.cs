using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeThrower : IInizializable
    {
        private KnifesMono knifesMono;
        private KnifesSettings settings;

        private Vector3 force;
        private Vector3 ricochetSpin;

        public void Inizialize()
        {
            knifesMono = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();

            force = settings.ForceDirection * settings.ForceMultiplier;
            ricochetSpin = settings.RicochetSpin * settings.RicochetSpinMultiplier;
        }

        public void ThrowKnife(KnifeMono knife)
        {
            knife.SetKinematic(false);
            knife.SetCollisionDetection(true);
            knife.SetCollisionDetectionMode(CollisionDetectionMode.ContinuousDynamic);
            knife.SpawnTime = Time.time;

            knife.Throw(force);
        }

        public void RicochetKnife(KnifeMono knife)
        {
            knife.SetVelocity(Vector3.zero);
            knife.SetCollisionDetection(false);
            knife.SetKinematic(false);
            knife.transform.SetParent(knifesMono.KnifesParent, true);

            Vector3 force = new Vector3();

            force.x = Random.Range(settings.RicochetForceMin.x, settings.RicochetForceMax.x);
            force.y = Random.Range(settings.RicochetForceMin.y, settings.RicochetForceMax.y);
            force.z = Random.Range(settings.RicochetForceMin.z, settings.RicochetForceMax.z);
            
            knife.Throw(force);
            knife.Spin(ricochetSpin);
        }
    }
}