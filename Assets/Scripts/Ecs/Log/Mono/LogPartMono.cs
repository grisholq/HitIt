using UnityEngine;

public class LogPartMono : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private new Collider collider;

    public Rigidbody Rigidbody
    {
        get
        {
            return rigidbody;
        }
    }

    public Collider Collider
    {
        get
        {
            return collider;
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<MeshCollider>();
    }
}
