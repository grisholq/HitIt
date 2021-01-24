using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<LevelFactory> levelFactoryFilter = null;

        private LevelFactory Factory { get { return levelFactoryFilter.Data; } }

        public void Initialize()
        {
            world.CreateEntityWith<LevelFactory>().Inizialize();
        }

        public void Destroy()
        {

            
        }

        public void Run()
        {
            
        }

        private void RunEvents()
        {

        }
    }
}
