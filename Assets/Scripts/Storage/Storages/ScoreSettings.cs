using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "MyAssets/Settings/ScoreSettings")]
    public class ScoreSettings : Storage
    {
        public int ApplesCount;
        public int MaxScore;
    }
}