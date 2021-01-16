using HitIt.Other;

namespace HitIt.Ecs
{
    public class InputData : IInizializable
    {
        public bool Pressed { get; set; }

        public void Inizialize()
        {
            Pressed = false;
        }
    }
}