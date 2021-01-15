using System;
using UnityEngine;

namespace Toweristika.Storage
{
    [Serializable]
    public class TransformObjectStorage
    {
        [SerializeField] private TransformObject type;
        [SerializeField] private Transform transform;

        public TransformObject Type
        {
            get
            {
                return type;
            }
        }

        public Transform Transform
        {
            get
            {
                return transform;
            }
        }
    }
}
