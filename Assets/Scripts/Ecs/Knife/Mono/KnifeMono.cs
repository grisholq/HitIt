using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeMono : MonoBehaviour, ILogObject
    {
        [SerializeField] private new Rigidbody rigidbody;

        [SerializeField] private Collider activeCollider;
        [SerializeField] private Collider unactiveCollider;

        private KnifeColliderType colliderType;
        private bool colliderState;

        public float Number { get; set; }
        public KnifeColliderType ColliderType 
        {
            get
            {
                return colliderType;
            }

            set
            {
                colliderType = value;
                SetColliderActivity(colliderState);
            }
        }
        public Rigidbody Rigidbody 
        { 
            get => rigidbody;
            set { } 
        }
        public bool IsTrigger 
        {
            get
            {
                return GetActiveCollider().isTrigger;
            }

            set
            {
                GetActiveCollider().isTrigger = value;
            }
        }
        public Transform Transform 
        { 
            get => transform;
            set { }
        }

        public void SetColliderActivity(bool state)
        {
            colliderState = state;
            activeCollider.enabled = false;
            unactiveCollider.enabled = false;
            GetActiveCollider().enabled = state;
        }

        private Collider GetActiveCollider()
        {
            switch (colliderType)
            {
                case KnifeColliderType.Active:
                    return activeCollider;

                case KnifeColliderType.Unactive:
                    return unactiveCollider;
            }
            return null;
        }

        private void OnEnable()
        {
            colliderState = false;
            colliderType = KnifeColliderType.Active;
        }

        private void OnTriggerEnter(Collider other)
        {
            KnifeMono knife = other.transform.GetComponent<KnifeMono>();
            LogMono log = other.transform.GetComponent<LogMono>();
            AppleMono apple = other.transform.GetComponent<AppleMono>();

            if (knife != null)
            {
                if (Number > knife.Number)
                {
                    
                    World.Instance.Current.CreateEntityWith<KnifeHitKnifeEvent>().Knife = this;
                    return;
                }
            }

            if (log != null)
            {
                World.Instance.Current.CreateEntityWith<KnifeHitLogEvent>().Knife = this;
                return;
            }


            if (apple != null)
            {
                World.Instance.Current.CreateEntityWith<KnifeHitAppleEvent>().Apple = apple;
                return;
            }
        }
    }
}