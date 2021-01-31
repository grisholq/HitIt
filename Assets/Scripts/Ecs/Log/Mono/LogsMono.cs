using UnityEngine;

namespace HitIt.Ecs
{
    public class LogsMono : MonoBehaviour
    {
        [SerializeField] private Transform logParent;
        [SerializeField] private Transform logPosition;
        [SerializeField] private Transform applesParent;
        [SerializeField] private AudioSource logHitSound;
        [SerializeField] private AudioSource logCrackSound;

        public Transform LogParent
        {
            get
            {
                return logParent;
            }
        }

        public Transform LogPostion
        {
            get
            {
                return logPosition;
            }
        }

        public Transform ApplesParent
        {
            get
            {
                return applesParent;
            }
        }

        public AudioSource LogHitSound
        {
            get
            {
                return logHitSound;
            }
        }
        
        public AudioSource LogCrackSound
        {
            get
            {
                return logCrackSound;
            }
        }
    }
}