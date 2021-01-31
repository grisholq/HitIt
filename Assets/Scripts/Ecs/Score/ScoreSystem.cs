using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class ScoreSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<ApplesHandler> applesHandlerFilter = null;
        private EcsFilterSingle<ScoreHandler> scoreHanlderFilter = null;

        private EcsFilter<AddAppleEvent> addAppleEvent = null;
        private EcsFilter<AddScoreEvent> addScoreEvent = null;
        private EcsFilter<NewScoreEvent> newScoreEvent = null;
        private EcsFilter<ResetScoreEvent> resetScoreEvent = null;
          
        private ApplesHandler ApplesHandler { get { return applesHandlerFilter.Data; } }
        private ScoreHandler ScoreHandler { get { return scoreHanlderFilter.Data; } }

        public void Initialize()
        {
            world.CreateEntityWith<ApplesHandler>().Inizialize();
            world.CreateEntityWith<ScoreHandler>().Inizialize();             
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            ApplesHandler.ShowApplesCount();
            ScoreHandler.ShowScore();
            RunEvents();
        }

        public void RunEvents()
        {
            RunAddAppleEvent();
            RunAddScoreEvent();
            RunNewScoreEvent();
            RunResetScoreEvent();
        }
        
        public void RunAddAppleEvent()
        {
            if(addAppleEvent.EntitiesCount != 0)
            {
                ApplesHandler.AddApple();
                World.Instance.RemoveEntitiesWith<AddAppleEvent>();
            }
        }
        
        public void RunAddScoreEvent()
        {
            if (addScoreEvent.EntitiesCount != 0)
            {
                ScoreHandler.AddScore();
                World.Instance.RemoveEntitiesWith<AddScoreEvent>();
            }
        }
        
        public void RunNewScoreEvent()
        {
            if (newScoreEvent.EntitiesCount != 0)
            {
                ScoreHandler.HandleNewScore();
                World.Instance.RemoveEntitiesWith<NewScoreEvent>();
            }
        }
        
        public void RunResetScoreEvent()
        {
            if (resetScoreEvent.EntitiesCount != 0)
            {
                ScoreHandler.ResetScore();
                World.Instance.RemoveEntitiesWith<ResetScoreEvent>();
            }
        }
    }
}