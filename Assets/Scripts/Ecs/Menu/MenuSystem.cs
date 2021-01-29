using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class MenuSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<MenuSelector> menuSelectorFilter = null;
        
        private EcsFilter<GameMenuEvent> gameMenuFilter = null;
        private EcsFilter<GameOverMenuEvent> gameOverMenuFilter = null;
        private EcsFilter<MainMenuEvent> mainMenuFilter = null;

        private MenuSelector Selector { get { return menuSelectorFilter.Data; } }

        public void Initialize()
        {
            world.CreateEntityWith<MenuSelector>().Inizialize();
            world.CreateEntityWith<MainMenuEvent>();
        }

        public void Destroy()
        {
            
        }

        public void Run()
        {
            RunEvents();
        }
        
        public void RunEvents()
        {
            if(gameMenuFilter.EntitiesCount != 0)
            {
                Selector.SetAllMenusUnactive();
                Selector.SetGameMenuActive();
                World.Instance.RemoveEntitiesWith<GameMenuEvent>();
            }

            if(gameOverMenuFilter.EntitiesCount != 0)
            {
                Selector.SetAllMenusUnactive();
                Selector.SetGameOverMenuActive();
                World.Instance.RemoveEntitiesWith<GameOverMenuEvent>();
            }
            
            if(mainMenuFilter.EntitiesCount != 0)
            {
                Selector.SetAllMenusUnactive();
                Selector.SetMainMenuActive();
                World.Instance.RemoveEntitiesWith<MainMenuEvent>();
            }
        }
    }
}
