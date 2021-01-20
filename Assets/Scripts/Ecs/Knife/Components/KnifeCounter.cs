using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeCounter : IInizializable
    {
        private KnifesSettings settings;

        public int Left { get; set; }
        public int Total { get; set; }

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<KnifesSettings>();
            Total = settings.KnifesOnLevel;
            Left = Total;
        }

        public void DecrementLeft()
        {
            Left--;
        }

        public bool KnifesExpired()
        {
            return Left == 0;
        }
    }
}