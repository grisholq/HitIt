using System.Collections.Generic;
using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogAttacher : IInizializable
    {
        private LogSettings settings;

        private List<KnifeMono> attachedKnifes;
        private List<Transform> attachedObjects;

        public int AttachedKnifesCount
        {
            get
            {
                return attachedKnifes.Count;
            }
        }

        public int AttachedObjectsCount
        {
            get
            {
                return attachedObjects.Count;
            }
        }

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
            attachedKnifes = new List<KnifeMono>();
            attachedObjects = new List<Transform>();
        }

        public void AttachKnife(LogMono log, KnifeMono knife)
        {
            attachedKnifes.Add(knife);

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
            attachedObjects.Add(obj);
            Vector3 objPosition = obj.transform.position;
            Vector3 logPosition = log.transform.position;
            objPosition = (objPosition - logPosition).normalized;

            obj.transform.position = logPosition + objPosition * settings.KnifeRadius;
            obj.transform.SetParent(log.transform);
        }
    }
}