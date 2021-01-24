using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "MyAssets/Settings/LevelSettings")]
    public class LevelsSettings : Storage
    {
        public LevelInformation[] SimpleLevels;
        public LevelInformation[] MediumLevels;
        public LevelInformation[] HardLevels;
        public LevelInformation[] BossesLevels;

        public float AppleChance;

        public float UnloadLevelTime;
        public float LoadLevelTime;
    }
}