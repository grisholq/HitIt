using UnityEngine;

namespace HitIt.Ecs
{
    public class LogMono : MonoBehaviour
    {
        [SerializeField] private LogPartMono[] logParts;
        [SerializeField] private Transform childs;
        [SerializeField] private Transform explosionPosition;
        [SerializeField] private new Collider collider;

        public void Rotate(Vector3 rotation)
        {
            childs.Rotate(rotation);
        }

        public void AddChild(Transform child)
        {
            child.SetParent(childs);
        }

        public void DisableLog()
        {
            collider.enabled = false;
        }

        public void ActivateLogParts()
        {
            for (int i = 0; i < logParts.Length; i++)
            {
                logParts[i].Collider.enabled = true;
                logParts[i].Rigidbody.isKinematic = false;
            }
        }

        public void ExplodeLogParts(float force, float radius)
        {
            for (int i = 0; i < logParts.Length; i++)
            {
                logParts[i].Rigidbody.AddExplosionForce(force, explosionPosition.position, radius);
            }         
        }
    }   
}