using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class ComponentsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        public void Initialize()
        {
            world.CreateEntityWith<LogObjectsSetter>();
        }

        public void Destroy()
        {
            
        }     

        public void Run()
        {
            
        }
    }
}