using UnityEngine;
using System.Collections.Generic;

public class Sticky : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;

    private void Start()
    {
        knifes = new LinkedList<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform knife = collision.transform;
        knife.GetComponent<Rigidbody>().isKinematic = true;        
        knife.SetParent(transform);
        knifes.AddLast(knife);

    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
