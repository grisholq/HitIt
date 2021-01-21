using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeMono : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;

        [SerializeField] private Collider ActiveCollider;
        [SerializeField] private Collider UnactiveCollider;

        private KnifeColliderType ColliderType;

        public Rigidbody Rigidbody
        { 
            get
            {
                return rigidbody;
            } 
        }

        public float SpawnTime { get; set; }     

        public void SetColliderType(KnifeColliderType type)
        {
            ColliderType = type;
        }        

        public void Throw(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        public void Spin(Vector3 spin)
        {
            rigidbody.AddTorque(spin, ForceMode.Acceleration);
        }

        private void SetColliderState(bool state)
        {
            switch (ColliderType)
            {
                case KnifeColliderType.Active:
                    ActiveCollider.enabled = state;
                    UnactiveCollider.enabled = false;
                    break;

                case KnifeColliderType.Unactive:
                    UnactiveCollider.enabled = state;
                    ActiveCollider.enabled = false;
                    break;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            KnifeMono knife = other.transform.GetComponent<KnifeMono>();

            if (knife == null) return;

            if (SpawnTime > knife.SpawnTime)
            {
                KnifeHitKnifeEvent e = World.Instance.Current.CreateEntityWith<KnifeHitKnifeEvent>();
                e.Knife = this;
            }
        }
    }
}