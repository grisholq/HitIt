using UnityEngine;

namespace HitIt.Ecs
{
    public class KnifesMono : MonoBehaviour
    {
        [SerializeField] private Transform knifesParent;
        [SerializeField] private Transform activeKnifePosition;
        [SerializeField] private Transform secondaryKnifePosition;

        public Transform KnifesParent
        {
            get
            {
                return knifesParent;
            }
        }

        public Transform ActiveKnifePosition 
        {
            get
            {
                return activeKnifePosition;
            }
        }


        public Transform SecondaryKnifePosition
        {
            get
            {
                return secondaryKnifePosition;
            }
        }
    }
}