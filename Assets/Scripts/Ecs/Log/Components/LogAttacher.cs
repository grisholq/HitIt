using System.Collections.Generic;
using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogAttacher : IInizializable
    {
        private LogSettings settings;

        private List<KnifeMono> knifes;
        private List<ILogObject> objects;

        public int AttachedKnifesCount
        {
            get
            {
                return knifes.Count;
            }
        }

        public int AttachedObjectsCount
        {
            get
            {
                return objects.Count;
            }
        }

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
            knifes = new List<KnifeMono>();
            objects = new List<ILogObject>();
        }

        public void AttachKnife(LogMono log, KnifeMono knife)
        {
            knifes.Add(knife);

            Vector3 knifePosition = knife.transform.position;
            Vector3 logPosition = log.transform.position;
            knifePosition = (knifePosition - logPosition).normalized;

            knife.transform.position = logPosition + knifePosition * settings.KnifeRadius;
            log.AddChild(knife.transform);
            knife.transform.eulerAngles = Vector3.zero;

            knife.ColliderType = KnifeColliderType.Unactive;
        }

        public void AttachObject(LogMono log, ILogObject attachable, float radius, float objecStartAngle, float angle)
        {
            float angle_r = angle * Mathf.Deg2Rad;           

            Vector3 pos = new Vector3();

            pos.y = Mathf.Sin(angle_r);
            pos.x = Mathf.Cos(angle_r);
            pos *= radius;

            log.AddChild(attachable.Transform);
            attachable.Transform.localPosition = pos;

            Vector3 eulers = attachable.Transform.localEulerAngles;
            eulers.z = objecStartAngle + angle;
            attachable.Transform.localEulerAngles = eulers;
        }

        public List<KnifeMono> GetAttachedKnifes()
        {
            return knifes;
        }

        public void Reset()
        {
            knifes.Clear();
            objects.Clear();
        }
    }
}