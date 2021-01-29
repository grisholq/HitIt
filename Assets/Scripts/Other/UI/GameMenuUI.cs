using UnityEngine;
using UnityEngine.UI;

namespace HitIt.Ecs
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] private Text score;
        [SerializeField] private Text knifes;

        public int Score
        {
            set
            {
                score.text = value.ToString();
            }
        }

        public void SetKnifesCount(int current, int total)
        {
            knifes.text = current.ToString() + "/" + total.ToString();
        }
    }
}