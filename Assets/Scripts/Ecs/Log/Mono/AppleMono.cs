using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class AppleMono : MonoBehaviour, IForceable, ICollidable, IAttachable
    {
        private new Collider collider;
        private new Rigidbody rigidbody;

        public bool IsTrigger 
        { 
            get => collider;
            set { } 
        }
        public Rigidbody Rigidbody 
        { 
            get => rigidbody;
            set { }
        }
        public Transform Transform
        { 
            get => transform;
            set { }
        }

        public void SetColliderActivity(bool state)
        {
            collider.enabled = state;
        }
    }
}