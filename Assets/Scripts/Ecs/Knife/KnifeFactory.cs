using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeFactory : IInizializable
    {
        private KnifesSettings settings;
        private KnifesMono knifes;


        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();
            knifes = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
        }

        public KnifeMono GetKnife()
        {
            KnifeMono knife = Object.Instantiate(settings.Knife).GetComponent<KnifeMono>();
            knife.transform.eulerAngles = Vector3.zero;
            knife.transform.position = knifes.SecondaryKnifePosition.position;
            knife.transform.SetParent(knifes.KnifesParent);

            return knife;
        }
    }
}