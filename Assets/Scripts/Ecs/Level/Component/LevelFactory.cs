using HitIt.Storage;
using HitIt.Other;
using UnityEngine;

namespace HitIt.Ecs
{
    public class LevelFactory : IInizializable
    {
        private LevelsSettings settings;

        public void Inizialize()
        {
            settings = StorageFacility.Instance.GetStorageByType<LevelsSettings>();
        }

        public LevelData GetLevel(LevelDifficulty difficulty)
        {
            LevelData data = new LevelData();
            LevelInformation information = GetLevelInformation(difficulty);

            data.KnifesAmount = information.KnifesAmount;
            data.LogPattern = information.LogRotationPattern;
            data.LogPrefab = information.LogPrefab;

            int amount = information.AttachedKnifesAmount;

            if(HasApple(settings.AppleChance))
            {
                amount++;
                data.AttachableObjects = new AttachableObject[amount];
                AttachableObject last = data.AttachableObjects[data.AttachableObjects.Length - 1];
                last.Type = AttachableObjectType.Apple;
                last.Angle = information.ApplesAngle[Random.Range(0, information.ApplesAngle.Length - 1)];
            }
            else
            {
                data.AttachableObjects = new AttachableObject[amount];
            }

            data.AttachableObjects = new AttachableObject[amount];

            for (int i = 0; i < information.AttachedKnifesAmount; i++)
            {
                AttachableObject buf = data.AttachableObjects[i];
                buf.Angle = information.KnifesAngle[i];
                buf.Type = AttachableObjectType.Knife;
            }

            return data;
        }

        private LevelInformation GetLevelInformation(LevelDifficulty difficulty)
        {
            int index;

            switch (difficulty)
            {
                case LevelDifficulty.Simple:
                    index = Random.Range(0, settings.SimpleLevels.Length - 1);
                    return settings.SimpleLevels[index];

                case LevelDifficulty.Medium:
                    index = Random.Range(0, settings.MediumLevels.Length - 1);
                    return settings.MediumLevels[index];

                case LevelDifficulty.Hard:
                    index = Random.Range(0, settings.HardLevels.Length - 1);
                    return settings.HardLevels[index];

                case LevelDifficulty.Boss:
                    index = Random.Range(0, settings.BossesLevels.Length - 1);
                    return settings.BossesLevels[index];
            }

            return null;
        }

        private bool HasApple(float chance)
        {
            return Random.value < chance;
        }
    }
}