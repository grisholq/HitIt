using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogAttacher : IInizializable
    {
        private LogSettings settings;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
        }

        public void AttachKnife(LogMono log, KnifeMono knife)
        {
            Vector3 knifePosition = knife.transform.position;
            Vector3 logPosition = log.transform.position;
            knifePosition = (knifePosition - logPosition).normalized;


            knife.transform.position = logPosition + knifePosition * settings.KnifeRadius;
            knife.transform.SetParent(log.transform);
            knife.transform.eulerAngles = Vector3.zero;

            knife.SetKinematic(true);
            knife.SetCollisionDetection(true);
            knife.SetCollisionDetectionMode(CollisionDetectionMode.Discrete);
        }

        public void AttachObject(LogMono log, Transform obj, float radius, float angle)
        {

        }
    }
}