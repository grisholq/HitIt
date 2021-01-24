using UnityEngine;
using HitIt.Ecs;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "LevelInformation", menuName = "MyAssets/Level/LevelInformation")]
    public class LevelInformation : ScriptableObject
    {
        public int KnifesAmount;
        public int AttachedKnifesAmount;
        [Range(0, 360f)] public float[] KnifesAngle;
        [Range(0, 360f)] public float[] ApplesAngle;
        public LogRotationPattern LogRotationPattern;
        public Transform LogPrefab;
        public Transform AttachedKnifePrefab;
    }
}