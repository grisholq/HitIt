using UnityEngine;
using UnityEngine.UI;

namespace HitIt.Ecs
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Text maxScore;

        public string MaxScore
        {
            set
            {
                maxScore.text = value;
            }
        }


        public void OnPlayButton()
        {
            World.Instance.Current.CreateEntityWith<GameMenuEvent>();
            World.Instance.Current.CreateEntityWith<StartGameEvent>();
        }
    }
}