using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogRotator : IInizializable
    {
        private LogSettings settings;

        private Vector3 rotation;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
            rotation = settings.Rotation * settings.RotationMultiplier;
        }

        public void RotateLog(LogMono log)
        {
            log.Rotate(rotation * Time.deltaTime);
        }
    }
}