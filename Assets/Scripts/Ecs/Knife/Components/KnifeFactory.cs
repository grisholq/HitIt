using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeFactory : IInizializable
    {
        private KnifesSettings settings;
        private KnifesMono knifes;

        private int knifeCount;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();
            knifes = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
            knifeCount = 0;
        }

        public KnifeMono GetKnife()
        {
            KnifeMono knife = Object.Instantiate(settings.Knife).GetComponent<KnifeMono>();
            knife.transform.SetParent(knifes.KnifesParent);
            knife.Rigidbody.maxAngularVelocity = settings.MaxKnifeAngularVelocity;             
            knife.ColliderType = KnifeColliderType.Active;
            knife.Number = knifeCount;
            knifeCount++;

            return knife;
        }

        public KnifeMono GetAttachKnife()
        {
            KnifeMono knife = Object.Instantiate(settings.Knife).GetComponent<KnifeMono>();
            knife.transform.SetParent(knifes.KnifesParent);
            knife.Rigidbody.maxAngularVelocity = settings.MaxKnifeAngularVelocity;
            knife.ColliderType = KnifeColliderType.Active;
            knife.Number = int.MinValue;

            return knife;
        }

        public void ResetIndex()
        {
            knifeCount = 0;
        }
    }
}