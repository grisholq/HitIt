using HitIt.Other;
using HitIt.Storage;
using UnityEngine;

namespace HitIt.Ecs
{
    public class ApplesHandler : IInizializable
    {
        private AppleMenuUI appleUI;
        private ScoreSettings settings;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<ScoreSettings>();
            appleUI = StorageFacility.Instance.GetInterface(InterfaceObject.AppleUI).GetComponent<AppleMenuUI>();
        }

        public void AddApple()
        {
            settings.ApplesCount++;
        }

        public void ShowApplesCount()
        {
            appleUI.ApplesCount = settings.ApplesCount.ToString();
        }
    }
}