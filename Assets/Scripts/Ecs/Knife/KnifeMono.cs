using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeMono : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Collider collider;

        public Rigidbody Rigidbody
        { 
            get
            {
                return rigidbody;
            } 
        }

        public float SpawnTime { get; set; }

        public void SetKinematic(bool kinematic)
        {
            rigidbody.isKinematic = kinematic;
        }

        public void SetCollisionDetection(bool detection)
        {          
            rigidbody.detectCollisions = detection;
            collider.enabled = detection;
        }

        public void SetCollisionDetectionMode(CollisionDetectionMode mode)
        {
            rigidbody.collisionDetectionMode = mode;
        }

        public void SetVelocity(Vector3 velocity)
        {           
            rigidbody.velocity = velocity;
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

            if (knife == null) return;

            if (SpawnTime > knife.SpawnTime)
            {
                KnifeHitKnifeEvent e = World.Instance.Current.CreateEntityWith<KnifeHitKnifeEvent>();
                e.Knife = this;
            }
        }
    }
}