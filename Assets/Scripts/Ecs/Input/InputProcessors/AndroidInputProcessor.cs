using UnityEngine;

namespace HitIt.Ecs
{
    public class AndroidInputProcessor : IInputProcessor
    {
        public void ProcessInput(InputData data)
        {
            data.Pressed = Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
}