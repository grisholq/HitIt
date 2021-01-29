using UnityEngine;
using UnityEngine.UI;

namespace HitIt.Ecs
{
    public class AppleMenuUI : MonoBehaviour
    {
        [SerializeField] private Text applesCount;

        public int ApplesCount
        {
            set
            {
                applesCount.text = value.ToString();
            }
        }
    }
}