using System;
using UnityEngine;

namespace Toweristika.Storage
{
    [Serializable]
    public class Transforms
    {
        [SerializeField] private TransformObjectStorage[] transformObjects;

        public Transform GetTransform(TransformObject type)
        {
            for (int i = 0; i < transformObjects.Length; i++)
            {
                if(transformObjects[i].Type == type)
                {
                    return transformObjects[i].Transform;
                }
            }
            return null;
        }
    }
}
