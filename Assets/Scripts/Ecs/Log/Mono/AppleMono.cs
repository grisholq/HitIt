using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class AppleMono : MonoBehaviour, ILogObject
    {
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;

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