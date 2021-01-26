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

        private EcsFilter<KnifeSystemFunction> knifeSystemFilter = null;
        private EcsFilter<LogSystemFunction> logSystemFilter = null;
       
        private EcsFilter<LevelFailedEvent> levelFailedEvent = null;
        private EcsFilter<LevelPassedEvent> levelPassedEvent = null;
       
        private EcsFilter<LevelResetEvent> levelResetEvent = null;
       
        private EcsFilter<StartGameEvent> startGameEvent = null;

        private LevelFactory Factory { get { return levelFactoryFilter.Data; } }
        private LevelChooser Chooser { get { return levelChooserFilter.Data; } }
        private Delayer Delayer { get { return delayeFilter.Data; } }

        public void Initialize()
        {
            world.CreateEntityWith<LevelFactory>().Inizialize();
            world.CreateEntityWith<LevelChooser>().Inizialize();
            world.CreateEntityWith<Delayer>();
            world.CreateEntityWith<StartGameEvent>();
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
            if(startGameEvent.EntitiesCount != 0)
            {
                LoadLevel();
                World.Instance.RemoveEntitiesWith<StartGameEvent>();
            }

            if(levelPassedEvent.EntitiesCount != 0)
            {
                RunLevelPassedEvent();
            }

            if(levelFailedEvent.EntitiesCount != 0)
            {
                RunLevelFailedEvent();
            }

            if(levelResetEvent.EntitiesCount != 0)
            {
                RunLevelResetEvent();
            }
        }

        private void RunLevelPassedEvent()
        {
            Chooser.LevelPassed();

            Delayer.Delay(UnloadLevel, LoadLevel, 0.5f);

            World.Instance.RemoveEntitiesWith<LevelPassedEvent>();
        }

        private void RunLevelFailedEvent()
        {
            UnloadLevel();
            World.Instance.RemoveEntitiesWith<LevelFailedEvent>();
        }
        
        private void RunLevelResetEvent()
        {
            Chooser.Reset();
            World.Instance.RemoveEntitiesWith<LevelResetEvent>();
        }

        private void LoadLevel()
        {
            Delayer.Delay(
            () =>
            {
                world.CreateEntityWith<LoadLevelEvent>().LevelData = Factory.GetLevel(Chooser.GetLevelDifficulty());
            },
            () =>
            {
                world.CreateEntityWith<KnifeSystemFunction>();
                world.CreateEntityWith<LogSystemFunction>();
            },
            0.25f);                    
        }

        private void UnloadLevel()
        {
            Delayer.Delay(
            () => 
            {
                
            }, 
            () => 
            {
                World.Instance.RemoveEntitiesWith<KnifeSystemFunction>();
                World.Instance.RemoveEntitiesWith<LogSystemFunction>();
                world.CreateEntityWith<UnloadLevelEvent>();
            }, 
            0.25f);
        }
    }
}