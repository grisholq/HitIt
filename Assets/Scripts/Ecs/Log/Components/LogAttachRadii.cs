using HitIt.Storage;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class LogAttachRadii: IInizializable
    {
        LogSettings settings;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LogSettings>();
        }

        public float KnifeRadius
        {
            get
            {
                return settings.KnifeAttachRadius;
            }
        }

        public float AppleRadius
        {
            get
            {
                return settings.AppleAttachRadius;
            }
        }
    }
}