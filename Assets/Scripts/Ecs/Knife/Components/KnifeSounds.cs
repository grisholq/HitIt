using HitIt.Storage;
using HitIt.Other;
using UnityEngine;

namespace HitIt.Ecs
{
    public class KnifeSounds : IInizializable
    {
        private KnifesMono knifesMono;

        private AudioSource knifeHitSound;
        private AudioSource appleHitSound;

        public void Inizialize()
        {
            knifesMono = StorageFacility.Instance.GetTransform(TransformObject.KnifesMono).GetComponent<KnifesMono>();
            knifeHitSound = knifesMono.KnifeHitSound;
            appleHitSound = knifesMono.AppleHitSound;
        }

        public void PlayKnifeHitSound()
        {
            knifeHitSound.Stop();
            knifeHitSound.Play();
        }
        
        public void PlayAppleHitSound()
        {
            appleHitSound.Stop();
            appleHitSound.Play();
        }
    }
}