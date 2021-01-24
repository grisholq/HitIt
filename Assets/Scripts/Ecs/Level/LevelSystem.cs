using LeopotamGroup.Ecs;
using UnityEngine;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<LevelFactory> levelFactoryFilter = null;
        
        private EcsFilter<LoadLevelEvent> loadLevelEvent = null;

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
            RunEvents();

            if (Input.GetKeyDown(KeyCode.L))
            {
                world.CreateEntityWith<LoadLevelEvent>().Data = Factory.GetLevel(LevelDifficulty.Simple);
            }
        }

        private void RunEvents()
        {
            World.Instance.RemoveEntitiesWith<LoadLevelEvent>();
        }
    }
}