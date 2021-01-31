using HitIt.Storage;
using HitIt.Other;
using UnityEngine;

namespace HitIt.Ecs
{
    public class LogSounds : IInizializable
    {
        private LogsMono logMono;

        private AudioSource logHitSound;
        private AudioSource logCrackSound;

        public void Inizialize()
        {
            logMono = StorageFacility.Instance.GetTransform(TransformObject.LogsMono).GetComponent<LogsMono>();
            logHitSound = logMono.LogHitSound;
            logCrackSound = logMono.LogCrackSound;
        }

        public void PlayLogHitSound()
        {
            logHitSound.Stop();
            logHitSound.Play();
        }
        
        public void PlayLogCrackSound()
        {
            logCrackSound.Stop();
            logCrackSound.Play();
        }
    }
}