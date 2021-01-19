using LeopotamGroup.Ecs;
using UnityEngine;

namespace HitIt.Ecs
{
    public class World : SingletonBase<World>
    {
        public EcsWorld Current { get; set; }

        private EcsSystems systems;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            EcsWorld world = new EcsWorld();
            Current = world;

            systems = new EcsSystems(world);

            systems.Add(new InputSystem())
            .Add(new LogSystem())
            .Add(new KnifeSystem());


            systems.Initialize();
        }

        private void Update()
        {
            
            systems.Run();
        }

        public void RemoveEntitiesWith<T>() where T : class, new()
        {
            EcsFilter<T> filter = Current.GetFilter<EcsFilter<T>>();
            if (filter.EntitiesCount == 0) return;

            for (int i = 0; i < filter.EntitiesCount; i++)
            {
                Current.RemoveEntity(filter.Entities[i]);
            }
        }
    }
}