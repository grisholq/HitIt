using UnityEngine;
using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class KnifeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<KnifeFactory> knifeFactoryFilter = null;
        private EcsFilterSingle<KnifeTimer> knifeTimerFilter = null; 
        private EcsFilterSingle<KnifeBuffer> knifeBufferFilter = null;
        private EcsFilterSingle<KnifePositioner> knifePositionerFilter = null;
        private EcsFilterSingle<KnifesPreparer> knifePreparerFilter = null;
        private EcsFilterSingle<KnifeUIHandler> knifeUIFilter = null;
        private EcsFilterSingle<KnifeCounter> knifeCounterFilter = null;
        private EcsFilterSingle<KnifeForces> knifeForcesFilter = null;

        private EcsFilterSingle<InputData> inputDataFilter = null;

        private EcsFilter<KnifeHitKnifeEvent> knifeHitEvent = null;
        private EcsFilter<KnifesExpiredEvent> knifesExpiredEvent = null;
        private EcsFilter<KnifesRandomForceEvent> knifesRandomForcesEvent = null;

        private KnifeFactory Factory { get { return knifeFactoryFilter.Data; } }
        private KnifeTimer Timer { get { return knifeTimerFilter.Data; } }
        private KnifeBuffer Buffer { get { return knifeBufferFilter.Data; } }
        private KnifePositioner Positioner { get { return knifePositionerFilter.Data; } }
        private KnifesPreparer Preparer { get { return knifePreparerFilter.Data; } }
        private KnifeUIHandler UIHanlder { get { return knifeUIFilter.Data; } }
        private KnifeCounter Counter { get { return knifeCounterFilter.Data; } }
        private KnifeForces Forces { get { return knifeForcesFilter.Data; } }
        private InputData Input { get { return inputDataFilter.Data; } }

        public void Initialize()
        {
            KnifeFactory factory = world.CreateEntityWith<KnifeFactory>();
            KnifePositioner positioner = world.CreateEntityWith<KnifePositioner>();
            KnifeBuffer buffer = world.CreateEntityWith<KnifeBuffer>(); 
            KnifeCounter counter = world.CreateEntityWith<KnifeCounter>();
            KnifeUIHandler knifeUI = world.CreateEntityWith<KnifeUIHandler>();
            world.CreateEntityWith<KnifeTimer>().Inizialize();                       
            world.CreateEntityWith<KnifesPreparer>().Inizialize();
            world.CreateEntityWith<KnifeForces>().Inizialize();


            factory.Inizialize();
            positioner.Inizialize();
            knifeUI.Inizialize();
            counter.Inizialize();

            KnifeMono knife = factory.GetKnife();
            buffer.ActiveKnife = knife;
            positioner.SetKnifePosition(knife.transform, KnifePositions.Active);
            knifeUI.SetKnifeAmount(counter.Left, counter.Total);
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            RunEvents();

            if (knifesExpiredEvent.EntitiesCount != 0) return;
      
            if(Input.Pressed && Timer.Update())
            {
                if (Buffer.ActiveKnife != null)
                {
                    Positioner.SetKnifePosition(Buffer.ActiveKnife.transform, KnifePositions.Active);
                    
                    Buffer.ActiveKnife.Rigidbody.isKinematic = false;
                    Buffer.ActiveKnife.SetColliderActivity(true);
                    Debug.Log(Forces.ThrowForce);
                    Buffer.ActiveKnife.Rigidbody.AddForce(Forces.ThrowForce, ForceMode.Acceleration);          
                    Buffer.ActiveKnife = null;
                }

                if (!Counter.KnifesExpired())
                {
                    KnifeMono knife = Factory.GetKnife();
                    Positioner.SetKnifePosition(knife.transform, KnifePositions.Secondary);
                    Buffer.ActiveKnife = knife;
                    Counter.DecrementLeft();
                    UIHanlder.SetKnifeAmount(Counter.Left, Counter.Total);
                }
                else
                {
                    world.CreateEntityWith<KnifesExpiredEvent>();
                }
            }
           
            if (Buffer.ActiveKnife != null) Preparer.MoveKnifeToActivePosition(Buffer.ActiveKnife);
        }

        private void RunEvents()
        {
            if(knifeHitEvent.EntitiesCount != 0)
            {
                KnifeHitKnifeEvent[] events = knifeHitEvent.Components1;

                for (int i = 0; i < knifeHitEvent.EntitiesCount; i++)
                {
                    KnifeMono knife = events[i].Knife;

                    knife.Rigidbody.velocity = Vector3.zero;
                    knife.Rigidbody.isKinematic = false;
                    knife.SetColliderActivity(false);
                    knife.Rigidbody.AddForce(Forces.RicochetForce, ForceMode.Acceleration);
                    knife.Rigidbody.AddTorque(Forces.RicochetTorque, ForceMode.Acceleration);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitKnifeEvent>();
            }

            if(knifeForcesFilter.EntitiesCount != 0)
            {
               KnifesRandomForceEvent[] events = knifesRandomForcesEvent.Components1;
                
                for (int i = 0; i < knifesRandomForcesEvent.EntitiesCount; i++)
                {
                    for (int i1 = 0; i1 < events[i].Knifes.Count; i1++)
                    {
                        KnifeMono knife = events[i].Knifes[i1];

                        knife.Rigidbody.isKinematic = false;
                        knife.SetColliderActivity(false);
                        knife.Rigidbody.AddForce(Forces.RandomForce, ForceMode.Acceleration);
                        knife.Rigidbody.AddTorque(Forces.RandomTorque, ForceMode.Acceleration);
                    }              
                }

                World.Instance.RemoveEntitiesWith<KnifesRandomForceEvent>();             
            }
        }
    }
}