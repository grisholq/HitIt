using UnityEngine;

namespace HitIt.Ecs
{
    public class LogMono : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            
            KnifeMono knife = other.GetComponent<KnifeMono>();
            if (knife != null)
            {
                World.Instance.Current.CreateEntityWith<KnifeHitLogEvent>().Knife = knife;
            }
        }
    }   
}