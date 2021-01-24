using UnityEngine;

namespace HitIt.Ecs
{
    public class LevelData
    {
        public int KnifesAmount { get; set; }
        public AttachableObject[] AttachableObjects { get; set; }
        public LogRotationPattern LogPattern { get; set; }
        public Transform LogPrefab { get; set; }
    }
}