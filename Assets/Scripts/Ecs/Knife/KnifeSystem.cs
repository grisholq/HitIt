using UnityEngine;
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
        private EcsFilterSingle<KnifeBuffer> knifeBufferFilter = null;
        private EcsFilterSingle<KnifePositioner> knifePositionerFilter = null;
        private EcsFilterSingle<KnifesPreparer> knifePreparerFilter = null;
     
        private EcsFilterSingle<InputData> inputDataFilter = null;
        private EcsFilter<KnifeHitKnifeEvent> knifeHitEvent = null;

        public void Initialize()
        {
            KnifeFactory factory = world.CreateEntityWith<KnifeFactory>();
            KnifePositioner positioner = world.CreateEntityWith<KnifePositioner>();
            KnifeBuffer buffer = world.CreateEntityWith<KnifeBuffer>();
            world.CreateEntityWith<KnifeThrower>().Inizialize();
            world.CreateEntityWith<KnifeTimer>().Inizialize();                       
            world.CreateEntityWith<KnifesPreparer>().Inizialize();

            factory.Inizialize();
            positioner.Inizialize();

            KnifeMono knife = factory.GetKnife();
            buffer.ActiveKnife = knife;
            positioner.SetKnifePosition(knife.transform, KnifePositions.Active);
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            RunEvents();

            KnifeFactory factory = knifeFactoryFilter.Data;
            KnifeThrower thrower = knifeThrowerFilter.Data;
            KnifeBuffer buffer = knifeBufferFilter.Data;
            KnifePositioner positioner = knifePositionerFilter.Data;
            KnifesPreparer preparer = knifePreparerFilter.Data;


            if (inputDataFilter.Data.Pressed && knifeTimerFilter.Data.Update())
            {
                positioner.SetKnifePosition(buffer.ActiveKnife.transform, KnifePositions.Active);
                thrower.ThrowKnife(buffer.ActiveKnife);

                KnifeMono knife = factory.GetKnife();
                positioner.SetKnifePosition(knife.transform, KnifePositions.Secondary);
                buffer.ActiveKnife = knife;
            }

            if (buffer.ActiveKnife != null) preparer.MoveKnifeToActivePosition(buffer.ActiveKnife);
        }

        private void RunEvents()
        {
            if(knifeHitEvent.EntitiesCount != 0)
            {
                KnifeHitKnifeEvent[] events = knifeHitEvent.Components1;
                KnifeThrower thrower = knifeThrowerFilter.Data;

                for (int i = 0; i < knifeHitEvent.EntitiesCount; i++)
                {
                    thrower.RicochetKnife(events[i].Knife);
                }
                World.Instance.RemoveEntitiesWith<KnifeHitKnifeEvent>();
            }
        }
    }
}