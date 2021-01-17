using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeMono : MonoBehaviour, IInizializable
    {
        [SerializeField] private Rigidbody rigidbody;

        private bool hitKnife;
        private bool hitLog;
        private bool hitThisFrame;

        [SerializeField] private int knHit;
        [SerializeField] private int lgHit;

        public void Inizialize()
        {
            hitKnife = false;
            hitLog = false;
            hitThisFrame = false;            
        }

        private void Start()
        {
            Inizialize();
        }

        private void Update()
        {
            hitThisFrame = false;
        }

        public void Throw(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<KnifeMono>() != null)
            {
                knHit++;
                if(hitLog & !hitThisFrame) return;
                rigidbody.detectCollisions = false;
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                rigidbody.velocity = new Vector3(Random.Range(-5f, 5f), -11, 0);

                Vector3 torq = new Vector3(0, 0, 40);

                rigidbody.maxAngularVelocity = 1000;
                rigidbody.angularVelocity = torq; 

                transform.SetParent(GameObject.Find("Knifes").transform);

                hitKnife = true;
                hitThisFrame = true;
            }
            else if (collision.transform.GetComponent<Sticky>() != null)
            {
                lgHit++;
                if (hitKnife) return;

                rigidbody.isKinematic = true;
                transform.SetParent(collision.transform, true);

                hitLog = true;
                hitThisFrame = true;
            }
        }
    }
}