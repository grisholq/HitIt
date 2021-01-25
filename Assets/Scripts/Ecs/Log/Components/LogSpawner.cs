using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogSpawner : IInizializable
    {
        private LogsMono logsMono;
        private LogSettings settings;

        public void Inizialize()
        {
            logsMono = StorageFacility.Instance.GetTransform(TransformObject.LogsMono).GetComponent<LogsMono>();
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
        }

        public LogMono GetLog(Transform logPrefab)
        {
            LogMono log = Object.Instantiate(logPrefab).GetComponent<LogMono>();
            log.transform.position = logsMono.LogPostion.position;
            log.transform.SetParent(logsMono.LogParent);

            return log;
        }
    }
}