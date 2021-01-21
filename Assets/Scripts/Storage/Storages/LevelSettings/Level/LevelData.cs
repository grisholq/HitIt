using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "MyAssets/Settings/LevelSettings")]
    public class LevelData : ScriptableObject
    {
        public int KnifesAmount;
        public int InKnifesAmount;
        public SerializalbeKnifePosition[] InKnifes;
        public SerializableLogRotationNode[] LogRotaiton;
        public Transform LogPrefab;
        public Transform InKnifePrefab;
    }
}