using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeFactory : IInizializable
    {
        private KnifesSettings settings;
        private Transform knifePosition;
        private Transform knifeParent;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();
            knifePosition = StorageFacility.Instance.GetTransform(TransformObject.KnifesPosition);
            knifeParent = StorageFacility.Instance.GetTransform(TransformObject.KnifesParent);
        }

        public KnifeMono GetKnife()
        {
            KnifeMono knife = Object.Instantiate(settings.Knife).GetComponent<KnifeMono>();
            knife.transform.eulerAngles = Vector3.zero;
            knife.transform.position = knifePosition.position;
            knife.transform.SetParent(knifeParent);

            return knife;
        }
    }
}