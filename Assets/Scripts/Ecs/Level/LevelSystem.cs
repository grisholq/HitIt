using LeopotamGroup.Ecs;
using UnityEngine;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        #region SingleFilters
        private EcsFilterSingle<LevelFactory> levelFactoryFilter = null;
        private EcsFilterSingle<LevelChooser> levelChooserFilter = null;
        private EcsFilterSingle<Delayer> delayeFilter = null;
        #endregion

        #region Events
        private EcsFilter<KnifeSystemFunction> knifeSystemFilter = null;
        private EcsFilter<LogSystemFunction> logSystemFilter = null;  
        
        private EcsFilter<LevelFailedEvent> levelFailedEvent = null;
        private EcsFilter<LevelPassedEvent> levelPassedEvent = null;       
        private EcsFilter<LevelResetEvent> levelResetEvent = null;      
        private EcsFilter<StartGameEvent> startGameEvent = null;
        #endregion

        #region Properties
        private LevelFactory Factory { get { return levelFactoryFilter.Data; } }
        private LevelChooser Chooser { get { return levelChooserFilter.Data; } }
        private Delayer Delayer { get { return delayeFilter.Data; } }
        #endregion

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
        }

        private void RunEvents()
        {
            RunStartGameEvent();
            RunLevelPassedEvent();
            RunLevelFailedEvent();
            RunLevelResetEvent();
        }

        private void RunLevelPassedEvent()
        {
            if (levelPassedEvent.EntitiesCount != 0)
            {
                Vibration.Vibrate(50);
                Chooser.LevelPassed();
                Delayer.Delay(UnloadLevel, LoadLevel, 0.7f);
                World.Instance.RemoveEntitiesWith<LevelPassedEvent>();
            }           
        }

        private void RunLevelFailedEvent()
        {
            if (levelFailedEvent.EntitiesCount != 0)
            {
                Delayer.Delay(
                () =>
                {
                    Chooser.Reset(); 
                    UnloadLevel();
                }
                ,
                () =>
                {                   
                    World.Instance.Current.CreateEntityWith<GameOverMenuEvent>();
                }
                , 0.3f); 
               
                World.Instance.RemoveEntitiesWith<LevelFailedEvent>();
            }        
        }
        
        private void RunLevelResetEvent()
        {
            if (levelResetEvent.EntitiesCount != 0)
            {
                Chooser.Reset();
                World.Instance.RemoveEntitiesWith<LevelResetEvent>();
            }          
        }

        private void RunStartGameEvent()
        {
            if (startGameEvent.EntitiesCount != 0)
            {
                Chooser.Reset();
                LoadLevel();
                World.Instance.RemoveEntitiesWith<StartGameEvent>();
            }
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
            0.35f);                    
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
            0.35f);
        }
    }
}