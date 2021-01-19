using HitIt.Storage;
using HitIt.Other;
using UnityEngine;

namespace HitIt.Ecs
{
    public class KnifesPreparer : IInizializable
    {
        private KnifesSettings settings;
        private KnifesMono knifes;

        private Vector3 start;
        private Vector3 end;
        private float speed;

        public void Inizialize()
        {
            knifes = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();

            end = knifes.ActiveKnifePosition.position;
            start = knifes.SecondaryKnifePosition.position;
            speed = (end - start).magnitude / settings.KnifePositioningTime;
        }

        public void MoveKnifeToActivePosition(KnifeMono knife)
        {
            knife.transform.position = 
                Vector3.MoveTowards(knife.transform.position, end, speed * Time.deltaTime);
        }
    }
}
