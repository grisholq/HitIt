using UnityEngine;
using UnityEngine.UI;

namespace HitIt.Ecs
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private Text score;

        public int Score
        {
            set
            {
                score.text = value.ToString();
            }
        }
    }
}