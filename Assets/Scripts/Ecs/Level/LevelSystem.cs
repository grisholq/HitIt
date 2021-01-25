using LeopotamGroup.Ecs;
using UnityEngine;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<LevelFactory> levelFactoryFilter = null;
        private EcsFilterSingle<LevelChooser> levelChooserFilter = null;
        private EcsFilterSingle<Delayer> delayeFilter = null;

        private EcsFilter<LoadLevelEvent> loadLevelEvent = null;
        private EcsFilter<UnloadLevelEvent> unloadLevelEvent = null;

        private EcsFilter<KnifeSystemFunction> knifeSystemFilter = null;
        private EcsFilter<LogSystemFunction> logSystemFilter = null;
       
        private EcsFilter<LevelFailedEvent> levelFailedEvent = null;
        private EcsFilter<LevelPassedEvent> levelPassedEvent = null;
       
        private EcsFilter<StartGameEvent> startGameEvent = null;

        private LevelFactory Factory { get { return levelFactoryFilter.Data; } }
        private LevelChooser Chooser { get { return levelChooserFilter.Data; } }
        private Delayer Delayer { get { return delayeFilter.Data; } }

        public void Initialize()
        {
            world.CreateEntityWith<LevelFactory>().Inizialize();
            world.CreateEntityWith<LevelChooser>().Inizialize();
            world.CreateEntityWith<Delayer>();        
        }

        public void Destroy()
        {
       
        }

        public void Run()
        {
            RunEvents();

            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadLevel();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                UnloadLevel();
            }
        }

        private void RunEvents()
        {
            World.Instance.RemoveEntitiesWith<LoadLevelEvent>();
            World.Instance.RemoveEntitiesWith<UnloadLevelEvent>();

            if(startGameEvent.EntitiesCount != 0)
            {
                LoadLevel();
            }
        }

        private void RunLevelPassedEvent()
        {
            UnloadLevel();

            Delayer.Delay(() =>
            {
                LoadLevel();
            }
            , 0.30f);
            
            

            World.Instance.RemoveEntitiesWith<LevelPassedEvent>();
        }

        private void RunLevelFailedEvent()
        {
            UnloadLevel();
        }

        private void LoadLevel()
        {
            world.CreateEntityWith<LoadLevelEvent>().Data = Factory.GetLevel(LevelDifficulty.Simple);
            world.CreateEntityWith<KnifeSystemFunction>();
            world.CreateEntityWith<LogSystemFunction>();
        }

        private void UnloadLevel()
        {
            World.Instance.RemoveEntitiesWith<LogSystemFunction>();
            World.Instance.RemoveEntitiesWith<KnifeSystemFunction>();
            world.CreateEntityWith<UnloadLevelEvent>();
        }
    }
}