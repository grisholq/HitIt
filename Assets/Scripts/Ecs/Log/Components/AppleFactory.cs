using HitIt.Other;
using HitIt.Storage;
using UnityEngine;

namespace HitIt.Ecs
{
    public class AppleFactory : IInizializable
    {
        private LogSettings settings;
        private LogsMono logsMono;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
            logsMono = StorageFacility.Instance.GetTransform(TransformObject.LogsMono).GetComponent<LogsMono>(); 
        }

        public AppleMono GetApple()
        {
            AppleMono apple = Object.Instantiate(settings.Apple).GetComponent<AppleMono>();
            apple.Transform.SetParent(logsMono.ApplesParent);

            apple.Rigidbody.isKinematic = true;
            apple.SetColliderActivity(false);

            return apple;
        }
    }
}