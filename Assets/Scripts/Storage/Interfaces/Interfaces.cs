using System;
using UnityEngine;

namespace Toweristika.Storage
{
    [Serializable]
    public class Interfaces
    {
        [SerializeField] private InterfaceObjectStorage[] interfaceObjects;

        public RectTransform GetInterface(InterfaceObject type)
        {
            for (int i = 0; i < interfaceObjects.Length; i++)
            {
                if (interfaceObjects[i].Type == type)
                {
                    return interfaceObjects[i].Rect;
                }
            }
            return null;
        }
    }
}
