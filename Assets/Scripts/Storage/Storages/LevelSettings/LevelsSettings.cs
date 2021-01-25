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

        public int LevelsPerPhase;
        public int LevelsPhases;

        public float DifficultyMinPerLevel; 
        public float DifficultyMaxPerLevel;

        public float StartDifficultyMin;
        public float StartDifficultyMax;

        public float MaxDifficulty;
    }
}