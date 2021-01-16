using UnityEngine;
using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;
        
        private EcsFilterSingle<InputHanlder> inputHandlerFilter = null;
        private EcsFilterSingle<InputData> inputDataFilter = null;

        public void Initialize()
        {
            world.CreateEntityWith<InputHanlder>().Inizialize();
            world.CreateEntityWith<InputData>().Inizialize();
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            inputHandlerFilter.Data.ProcessInput(inputDataFilter.Data);
        }
    }
}