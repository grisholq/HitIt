using UnityEngine;
using UnityEngine.UI;

namespace HitIt.Ecs
{
    public class KnifeUIMono : MonoBehaviour
    {
        [SerializeField] private Text knifesAmount;

        public void SetKnifesAmount(int left, int total)
        {
            knifesAmount.text = left.ToString() + "/" + total.ToString();
        }
    }
}