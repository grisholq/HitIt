using UnityEngine;
using UnityEngine.UI;

namespace HitIt.Ecs
{
    public class AppleMenuUI : MonoBehaviour
    {
        [SerializeField] private Text applesCount;

        public string ApplesCount
        {
            set
            {
                applesCount.text = value;
            }
        }
    }
}