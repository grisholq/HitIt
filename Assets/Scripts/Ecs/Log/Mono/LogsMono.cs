using UnityEngine;

namespace HitIt.Ecs
{
    public class LogsMono : MonoBehaviour
    {
        [SerializeField] private Transform logParent;
        [SerializeField] private Transform logPosition;
       
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
    }
}