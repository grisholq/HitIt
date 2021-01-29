using UnityEngine;

namespace HitIt.Ecs
{
    public class MainMenuUI : MonoBehaviour
    {
        public void OnPlayButton()
        {
            World.Instance.Current.CreateEntityWith<GameMenuEvent>();
            World.Instance.Current.CreateEntityWith<StartGameEvent>();
        }
    }
}