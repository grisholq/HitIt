using UnityEngine;

public class Sticky : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float mult;

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * mult);
    }
}
