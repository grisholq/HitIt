using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeMono : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;

        [SerializeField] private Collider activeCollider;
        [SerializeField] private Collider unactiveCollider;

        public Rigidbody Rigidbody
        { 
            get
            {
                return rigidbody;
            } 
        }

        public float SpawnTime { get; set; }        

        public void Throw(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        public void Spin(Vector3 spin)
        {
            rigidbody.AddTorque(spin, ForceMode.Acceleration);
        }

        public void SetColliderActivity(KnifeColliderType type)
        {
            switch (type)
            {
                case KnifeColliderType.Active:
                    breaK
            }
        }

        private void OnTriggerStay(Collider other)
        {
            KnifeMono knife = other.transform.GetComponent<KnifeMono>();
            LogMono log = other.transform.GetComponent<LogMono>();

            if (knife != null)
            {
                if (SpawnTime > knife.SpawnTime)
                {
                    World.Instance.Current.CreateEntityWith<KnifeHitKnifeEvent>().Knife = this;
                }
            }

            if (log != null)
            {
                World.Instance.Current.CreateEntityWith<KnifeHitLogEvent>().Knife = this;
            }
        }
    }
}