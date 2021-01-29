using UnityEngine;

namespace HitIt.Ecs
{
    public class GameOverMenuUI : MonoBehaviour
    {
        public void OnRestartButton()
        {
            World.Instance.Current.CreateEntityWith<StartGameEvent>();
            World.Instance.Current.CreateEntityWith<LevelResetEvent>();
        }

        public void OnMenuButton()
        {
            World.Instance.Current.CreateEntityWith<MainMenuEvent>();
        }
    }
}