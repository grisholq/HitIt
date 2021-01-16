using UnityEngine;
using HitIt.Other;

namespace HitIt.Ecs
{
    public class KnifeMono : MonoBehaviour
    {
        [SerializeField] Rigidbody rigidbody;

        public void Throw(Vector3 force)
        {
            rigidbody.AddForce(force, ForceMode.Acceleration);
        }
    }
}