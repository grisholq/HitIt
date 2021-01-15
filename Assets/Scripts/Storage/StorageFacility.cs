using UnityEngine;

namespace Toweristika.Storage
{
    public class StorageFacility : SingletonBase<StorageFacility>
    {
        [SerializeField] private Storage[] Storages;
        [SerializeField] private Transforms Transforms;
        [SerializeField] private Interfaces Interfaces;
        [SerializeField] private Prefabs Prefabs;

        public T GetStorageByType<T>() where T : Storage
        {
            for (int i = 0; i < Storages.Length; i++)
            {
                if(Storages[i].GetType() == typeof(T))
                {
                    return Storages[i] as T;
                }
            }
            return null;
        }

        public Transform GetTransform(TransformObject type)
        {
            return Transforms.GetTransform(type);
        }

        public RectTransform GetInterface(InterfaceObject type)
        {
            return Interfaces.GetInterface(type);
        }

        public Transform GetPrefab(PrefabObject type)
        {
            return Prefabs.GetPrefab(type);
        }
    }
}