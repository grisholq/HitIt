using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeMono : MonoBehaviour
    {
        [SerializeField] private new Rigidbody rigidbody;

        [SerializeField] private Collider activeCollider;
        [SerializeField] private Collider unactiveCollider;

        private KnifeColliderType colliderType;
        private bool colliderState;

        public float SpawnTime { get; set; }
        public KnifeColliderType ColliderType 
        {
            get
            {
                return colliderType;
            }

            set
            {
                colliderType = value;
                SetCollidersState(colliderState);
            }
        }
        public Rigidbody Rigidbody
        {
            get
            {
                return rigidbody;
            }
        }

        private void OnEnable()
        {
            colliderState = false;
            colliderType = KnifeColliderType.Active;
        }

        public void ApplyForce(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        public void ApplyTorque(Vector3 torque)
        {
            rigidbody.AddTorque(torque, ForceMode.Acceleration);
        }

        public void Activate()
        {
            rigidbody.isKinematic = false;
            SetCollidersState(true);
        }

        public void Stop(bool collidable)
        {
            rigidbody.isKinematic = true;
            SetCollidersState(collidable);
        }

        public void Deactivate()
        {
            rigidbody.isKinematic = false;
            SetCollidersState(false);
        }

        private void SetCollidersState(bool state)
        {
            activeCollider.enabled = false; 
            unactiveCollider.enabled = false;

            colliderState = state;            
            rigidbody.detectCollisions = state;

            switch (ColliderType)
            {
                case KnifeColliderType.Active:
                    activeCollider.enabled = state;
                    break;

                case KnifeColliderType.Unactive:
                    unactiveCollider.enabled = state;
                    break;
            }            
        }

        private void OnTriggerEnter(Collider other)
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