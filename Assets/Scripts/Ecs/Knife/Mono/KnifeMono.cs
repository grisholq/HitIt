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

<<<<<<< HEAD
        public float SpawnTime { get; set; }     

        public void SetColliderType(KnifeColliderType type)
        {
            ColliderType = type;
=======
        public float SpawnTime { get; set; }        

        public void Throw(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        public void Spin(Vector3 spin)
        {
            rigidbody.AddTorque(spin, ForceMode.Acceleration);
>>>>>>> 765f59305c931184f06d0fecf9e9528deae81a07
        }

        public void SetColliderActivity(KnifeColliderType type)
        {
            switch (type)
            {
                case KnifeColliderType.Active:
<<<<<<< HEAD
                    ActiveCollider.enabled = state;
                    UnactiveCollider.enabled = false;
                    break;

                case KnifeColliderType.Unactive:
                    UnactiveCollider.enabled = state;
                    ActiveCollider.enabled = false;
                    break;
                case KnifeColliderType.None:
                    ActiveCollider.enabled = false;
                    UnactiveCollider.enabled = false;
                    break;
=======
                    breaK
>>>>>>> 765f59305c931184f06d0fecf9e9528deae81a07
            }
        }

        public void Throw(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        public void Spin(Vector3 spin)
        {
            rigidbody.AddTorque(spin, ForceMode.Acceleration);
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