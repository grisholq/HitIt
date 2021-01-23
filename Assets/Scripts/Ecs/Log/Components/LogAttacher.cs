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
            log.AddChild(knife.transform);
            knife.transform.eulerAngles = Vector3.zero;

            knife.Rigidbody.isKinematic = true;
            knife.SetColliderActivity(true);
            knife.ColliderType = KnifeColliderType.Unactive;
        }

        public void AttachObject(LogMono log, IAttachable attachable, float radius, float defaultAngle, float angle)
        {
            float realAngle = defaultAngle + angle;
            Vector3 dir = new Vector3();

            dir.y = Mathf.Sin(realAngle);
            dir.z = Mathf.Cos(realAngle);
            dir *= radius;

            log.AddChild(attachable.Transform);
            attachable.Transform.localPosition = dir;
        }

        public List<KnifeMono> GetAttachedKnifes()
        {
            return attachedKnifes;
        }
    }
}