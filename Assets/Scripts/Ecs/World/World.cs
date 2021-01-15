using UnityEngine;
using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    public class World : SingletonBase<World>
    {
        public EcsWorld Current { get; set; }

        private EcsSystems systems;

        private void Awake()
        {
            EcsWorld world = new EcsWorld();
            systems = new EcsSystems(world);
        }

        private void Update()
        {
            systems.Run();
        }
    }
}