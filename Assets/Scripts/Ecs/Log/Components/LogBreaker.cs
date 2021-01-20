using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogBreaker : IInizializable
    {
        private LogsMono logsMono;
        private LogSettings settings;

        public void Inizialize()
        {
            logsMono = StorageFacility.Instance.GetTransform(TransformObject.LogsMono).GetComponent<LogsMono>();
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
        }

        public void BreakLog(LogMono log)
        {
            log.DisableLog();
            log.ActivateLogParts();
            log.ExplodeLogParts(settings.LogExplosionForce, settings.LogExplosionRadius);
        }
    }
}