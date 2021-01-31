using UnityEngine;
using UnityEngine.UI;

namespace HitIt.Ecs
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private Text score;
        [SerializeField] private Text knifes;

        public string Score
        {
            set
            {
                score.text = value;
            }
        }

        public string Knifes
        {
            set
            {
                knifes.text = value;
            }
        }
    }
}