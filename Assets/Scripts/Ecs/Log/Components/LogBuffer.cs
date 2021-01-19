using UnityEngine;
using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class LogBuffer : IInizializable
    {
        public LogMono ActiveLog { get; set; }

        public void Inizialize()
        {
            ActiveLog = null;
        }
    }
}