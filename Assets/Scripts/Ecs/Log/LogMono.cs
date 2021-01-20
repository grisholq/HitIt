using UnityEngine;

namespace HitIt.Ecs
{
    public class LogMono : MonoBehaviour
    {

        [SerializeField] private LogPartMono[] logParts;
        [SerializeField] private Transform explosionPosition;
        [SerializeField] private Collider collider;

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

        private void OnTriggerEnter(Collider other)
        {            
            KnifeMono knife = other.GetComponent<KnifeMono>();
            if (knife != null)
            {
                World.Instance.Current.CreateEntityWith<KnifeHitLogEvent>().Knife = knife;
            }
        }
    }   
}