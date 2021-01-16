using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeThrower : IInizializable
    {
        private KnifesSettings settings;
        private Vector3 force;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();
            force = settings.ForceDirection * settings.ForceMultiplier;
        }

        public void ThrowKnife(KnifeMono knife)
        {
            knife.Throw(force);
        }
    }
}
