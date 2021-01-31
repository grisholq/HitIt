using UnityEngine;

namespace HitIt.Ecs
{
    public class GameOverMenuUI : MonoBehaviour
    {
        public void OnRestartButton()
        {
            World.Instance.Current.CreateEntityWith<GameMenuEvent>();
            World.Instance.Current.CreateEntityWith<StartGameEvent>();
        }

        public void OnMenuButton()
        {
            World.Instance.Current.CreateEntityWith<MainMenuEvent>();
        }
    }
}