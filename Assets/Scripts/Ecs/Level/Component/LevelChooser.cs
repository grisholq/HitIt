using HitIt.Storage;
using HitIt.Other;
using UnityEngine;

namespace HitIt.Ecs
{
    public class LevelChooser : IInizializable
    {
        private LevelsSettings settings;

        private int levelsPassed;

        private float currentDifficultyMin;
        private float currentDifficultyMax;

        private bool nextBoss;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LevelsSettings>();
            levelsPassed = 0;
            currentDifficultyMin = settings.StartDifficultyMin;
            currentDifficultyMax = settings.StartDifficultyMax;
            nextBoss = false;
        }


        public LevelDifficulty GetLevelDifficulty()
        {
            if(nextBoss) return LevelDifficulty.Boss;

            float level = Random.Range(currentDifficultyMin, currentDifficultyMax);
            level = Mathf.Clamp(level, 0, settings.MaxDifficulty);
            level = Mathf.FloorToInt(level);

            LevelDifficulty difficulty = (LevelDifficulty)((int)level);

            return difficulty;
        }

        public void LevelPassed()
        {
            if(++levelsPassed >= settings.LevelsPerPhase)
            {
                levelsPassed = 0;
                currentDifficultyMin += settings.DifficultyMinPerLevel;
                currentDifficultyMax += settings.DifficultyMaxPerLevel;
            }
        }

        public void Reset()
        {
            levelsPassed = 0;
            currentDifficultyMin = settings.StartDifficultyMin;
            currentDifficultyMax = settings.StartDifficultyMax;
            nextBoss = false;
        }
    }
}