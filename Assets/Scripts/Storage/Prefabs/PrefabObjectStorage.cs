using System;
using UnityEngine;

namespace Toweristika.Storage
{
    [Serializable]
    public class PrefabObjectStorage
    {
        [SerializeField] private PrefabObject type;
        [SerializeField] private Transform prefab;

        public PrefabObject Type
        {
            get
            {
                return type;
            }
        }

        public Transform Prefab
        {
            get
            {
                return prefab;
            }
        }
    }
}
