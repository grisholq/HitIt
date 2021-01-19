using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeTimer : IInizializable
    {
        private KnifesSettings settings;
        private Period knifePeriod;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();
            knifePeriod = new Period(Time.time, settings.KnifeThrowPeriod);
        }

        public bool Update()
        {
            if (knifePeriod.Passed(Time.time))
            {
                knifePeriod.SetLastTime(Time.time);
                return true;
            }

            return false;
        }
    }
}