using UnityEngine;

namespace HitIt.Ecs
{
    public class PCInputProcessor : IInputProcessor
    {
        public void ProcessInput(InputData data)
        {
            data.Pressed = Input.GetMouseButton(0);
        }
    }
}