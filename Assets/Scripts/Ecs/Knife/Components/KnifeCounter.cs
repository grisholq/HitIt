using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeCounter : IInizializable
    {
        public int Left { get; set; }
        public int Total { get; set; }

        public void Inizialize()
        {
            Left = 0;
            Total = 0;
        }

        public void DecrementLeft()
        {
            Left--;
        }
    }
}