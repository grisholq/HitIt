using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class KnifeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<KnifeFactory> knifeFactoryFilter = null;
        private EcsFilterSingle<KnifeThrower> knifeThrowerFilter = null;
        private EcsFilterSingle<KnifeTimer> knifeTimerFilter = null;

        private EcsFilterSingle<InputData> inputDataFilter = null;

        public void Initialize()
        {
            world.CreateEntityWith<KnifeFactory>().Inizialize();
            world.CreateEntityWith<KnifeThrower>().Inizialize();
            world.CreateEntityWith<KnifeTimer>().Inizialize();
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            if(inputDataFilter.Data.Pressed && knifeTimerFilter.Data.Update())
            {
                KnifeFactory factory = knifeFactoryFilter.Data;
                KnifeThrower thrower = knifeThrowerFilter.Data;

                KnifeMono knife = factory.GetKnife();
                thrower.ThrowKnife(knife);                
            }
        }
    }
}