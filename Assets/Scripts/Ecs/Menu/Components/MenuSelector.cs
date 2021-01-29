using UnityEngine;
using HitIt.Storage;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class MenuSelector : IInizializable
    {
        private RectTransform mainMenu;
        private RectTransform gameMenu;
        private RectTransform gameOverMenu;

        public void Inizialize()
        {
            mainMenu = StorageFacility.Instance.GetInterface(InterfaceObject.MainMenuUI);
            gameMenu = StorageFacility.Instance.GetInterface(InterfaceObject.GameUI);
            gameOverMenu = StorageFacility.Instance.GetInterface(InterfaceObject.GameOverUI);
        }

        public void SetAllMenusUnactive()
        {
            mainMenu.gameObject.SetActive(false);
            gameMenu.gameObject.SetActive(false);
            gameOverMenu.gameObject.SetActive(false);
        }

        public void SetMainMenuActive()
        {
            mainMenu.gameObject.SetActive(true);
        }

        public void SetGameMenuActive()
        {
            gameMenu.gameObject.SetActive(true);
        }

        public void SetGameOverMenuActive()
        {
            gameOverMenu.gameObject.SetActive(true);
        }
    }
}