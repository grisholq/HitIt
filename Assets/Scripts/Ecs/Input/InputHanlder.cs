using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class InputHanlder : IInizializable
    {
        IInputProcessor inputProcessor;

        public void Inizialize()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                inputProcessor = new AndroidInputProcessor();
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                inputProcessor = new PCInputProcessor();
            }
        }

        public void ProcessInput(InputData data)
        {
            inputProcessor.ProcessInput(data);
        }
    }
}