using UnityEngine;

namespace HitIt.Ecs
{
    public class KnifesMono : MonoBehaviour
    {
        [SerializeField] private Transform knifesParent;
        [SerializeField] private Transform activeKnifePosition;
        [SerializeField] private Transform secondaryKnifePosition;
        [SerializeField] private AudioSource knifeHitSound;
        [SerializeField] private AudioSource appleHitSound;

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
        
        public AudioSource KnifeHitSound
        {
            get
            {
                return knifeHitSound;
            }
        }
        
        public AudioSource AppleHitSound
        {
            get
            {
                return appleHitSound;
            }
        }
    }
}