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
        private EcsFilterSingle<LogObjectsSetter> logObjectsSetterFilter = null;

        private EcsFilterSingle<InputData> inputDataFilter = null;

        private EcsFilter<KnifeHitKnifeEvent> knifeHitKnifeEvent = null;
        private EcsFilter<KnifeHitAppleEvent> knifeHitAppleEvent = null;
        private EcsFilter<KnifesExpiredEvent> knifesExpiredEvent = null;
        private EcsFilter<KnifesRandomForceEvent> knifesRandomForcesEvent = null;
        private EcsFilter<LoadLevelEvent> loadLevelEvent = null;

        private KnifeFactory Factory { get { return knifeFactoryFilter.Data; } }
        private KnifeTimer Timer { get { return knifeTimerFilter.Data; } }
        private KnifeBuffer Buffer { get { return knifeBufferFilter.Data; } }
        private KnifePositioner Positioner { get { return knifePositionerFilter.Data; } }
        private KnifesPreparer Preparer { get { return knifePreparerFilter.Data; } }
        private KnifeUIHandler UIHanlder { get { return knifeUIFilter.Data; } }
        private KnifeCounter Counter { get { return knifeCounterFilter.Data; } }
        private KnifeForces Forces { get { return knifeForcesFilter.Data; } }
        private LogObjectsSetter LogObjectsSetter { get { return logObjectsSetterFilter.Data; } }
        private InputData Input { get { return inputDataFilter.Data; } }

        public void Initialize()
        {
            world.CreateEntityWith<KnifeFactory>().Inizialize();
            world.CreateEntityWith<KnifePositioner>().Inizialize();           
            world.CreateEntityWith<KnifeCounter>().Inizialize();
            world.CreateEntityWith<KnifeUIHandler>().Inizialize();
            world.CreateEntityWith<KnifeTimer>().Inizialize();
            world.CreateEntityWith<KnifesPreparer>().Inizialize();
            world.CreateEntityWith<KnifeForces>().Inizialize(); 
            world.CreateEntityWith<KnifeBuffer>();
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            RunLevelLoading();
            RunEvents();
            RunSystem();         
        }

        private void RunLevelLoading()
        {
            if (loadLevelEvent.EntitiesCount == 0) return;

            Debug.Log(1);

            LevelData data = loadLevelEvent.Components1[0].Data;

            Factory.ResetIndex();
            Counter.Left = data.KnifesAmount;
            Counter.Total = data.KnifesAmount;
            UIHanlder.SetKnifeAmount(Counter.Left, Counter.Total);
            Timer.Reset();
            Buffer.ActiveKnife = null;

            KnifeMono knife = Factory.GetKnife();
            Positioner.SetKnifePosition(knife.transform, KnifePositions.Active);
            Buffer.ActiveKnife = knife;
            LogObjectsSetter.Stop(knife, false);
        }

        private void RunSystem()
        {
            if (knifesExpiredEvent.EntitiesCount != 0) return;

            if (Input.Pressed && Timer.Update())
            {
                if (Buffer.ActiveKnife != null)
                {
                    Positioner.SetKnifePosition(Buffer.ActiveKnife.transform, KnifePositions.Active);
                    LogObjectsSetter.Activate(Buffer.ActiveKnife);
                    Buffer.ActiveKnife.Rigidbody.AddForce(Forces.ThrowForce, ForceMode.Acceleration);
                    Buffer.ActiveKnife = null;
                }

                if (!Counter.KnifesExpired())
                {
                    KnifeMono knife = Factory.GetKnife();
                    LogObjectsSetter.Stop(knife, false);
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
            if (knifeHitAppleEvent.EntitiesCount != 0)
            {
                KnifeHitAppleEvent[] events = knifeHitAppleEvent.Components1;

                for (int i = 0; i < knifeHitAppleEvent.EntitiesCount; i++)
                {
                    AppleMono apple = events[i].Apple;

                    apple.Rigidbody.velocity = Vector3.zero;
                    LogObjectsSetter.Deactivate(apple);
                    apple.Rigidbody.AddForce(Forces.RandomForce, ForceMode.Acceleration);
                    apple.Rigidbody.AddTorque(Forces.RandomTorque, ForceMode.Acceleration);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitAppleEvent>();
            }

            if (knifeHitKnifeEvent.EntitiesCount != 0)
            {
                KnifeHitKnifeEvent[] events = knifeHitKnifeEvent.Components1;

                for (int i = 0; i < knifeHitKnifeEvent.EntitiesCount; i++)
                {
                    KnifeMono knife = events[i].Knife;

                    knife.Rigidbody.velocity = Vector3.zero;
                    LogObjectsSetter.Deactivate(knife);
                    knife.Rigidbody.AddForce(Forces.RicochetForce, ForceMode.Acceleration);
                    knife.Rigidbody.AddTorque(Forces.RicochetTorque, ForceMode.Acceleration);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitKnifeEvent>();
            }

            if (knifeForcesFilter.EntitiesCount != 0)
            {
                KnifesRandomForceEvent[] events = knifesRandomForcesEvent.Components1;

                for (int i = 0; i < knifesRandomForcesEvent.EntitiesCount; i++)
                {
                    for (int i1 = 0; i1 < events[i].Knifes.Count; i1++)
                    {
                        KnifeMono knife = events[i].Knifes[i1];

                        LogObjectsSetter.Deactivate(knife);
                        knife.Rigidbody.AddForce(Forces.RandomForce, ForceMode.Acceleration);
                        knife.Rigidbody.AddTorque(Forces.RandomTorque, ForceMode.Acceleration);
                    }
                }

                World.Instance.RemoveEntitiesWith<KnifesRandomForceEvent>();
            }
        }
    }
}