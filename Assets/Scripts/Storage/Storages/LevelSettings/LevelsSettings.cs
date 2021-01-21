using UnityEngine;

namespace HitIt.Storage
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "MyAssets/Settings/LevelSettings")]
    public class LevelsSettings : Storage
    {
        public LevelData[] Levels;
    }
}