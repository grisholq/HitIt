using System;
using UnityEngine;

namespace HitIt.Ecs
{
    [CreateAssetMenu(fileName = "LogRotationPattern", menuName = "MyAssets/Log/LogRotationPattern")]
    public class LogRotationPattern : ScriptableObject
    {
        [SerializeField] private LogRotationNode[] nodes;

        public IIterator<LogRotationNode> GetIterator()
        {
            return new LogRotationIterator(nodes);
        }
    }
}