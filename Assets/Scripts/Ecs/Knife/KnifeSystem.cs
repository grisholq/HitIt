using UnityEngine;
using LeopotamGroup.Ecs;
using HitIt.Other;

namespace HitIt.Ecs
{
    [EcsInject]
    public class KnifeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;
        
        #region SingleFilters
        private EcsFilterSingle<KnifeFactory> knifeFactoryFilter = null;
        private EcsFilterSingle<KnifeTimer> knifeTimerFilter = null;
        private EcsFilterSingle<KnifeBuffer> knifeBufferFilter = null;
        private EcsFilterSingle<KnifePositioner> knifePositionerFilter = null;
        private EcsFilterSingle<KnifesPreparer> knifePreparerFilter = null;
        private EcsFilterSingle<KnifeUIHandler> knifeUIFilter = null;
        private EcsFilterSingle<KnifeCounter> knifeCounterFilter = null;
        private EcsFilterSingle<KnifeForces> knifeForcesFilter = null;
        private EcsFilterSingle<KnifesList> knifeListFilter = null;
        private EcsFilterSingle<LogObjectsSetter> logObjectsSetterFilter = null;
        private EcsFilterSingle<InputData> inputDataFilter = null;
        #endregion

        #region Events
        private EcsFilter<KnifeHitKnifeEvent> knifeHitKnifeEvent = null;
        private EcsFilter<KnifeHitAppleEvent> knifeHitAppleEvent = null;
        private EcsFilter<KnifesExpiredEvent> knifesExpiredEvent = null;
        private EcsFilter<LogObjectRandomForce> knifesRandomForcesEvent = null;
        private EcsFilter<LoadLevelEvent> loadLevelEvent = null; 
        private EcsFilter<UnloadLevelEvent> unloadLevelEvent = null;
        private EcsFilter<KnifeSystemFunction> knifeSystemFilter = null;
        #endregion

        #region Properties
        private KnifeFactory Factory { get { return knifeFactoryFilter.Data; } }
        private KnifeTimer Timer { get { return knifeTimerFilter.Data; } }
        private KnifeBuffer Buffer { get { return knifeBufferFilter.Data; } }
        private KnifePositioner Positioner { get { return knifePositionerFilter.Data; } }
        private KnifesPreparer Preparer { get { return knifePreparerFilter.Data; } }
        private KnifeUIHandler UIHanlder { get { return knifeUIFilter.Data; } }
        private KnifeCounter Counter { get { return knifeCounterFilter.Data; } }
        private KnifeForces Forces { get { return knifeForcesFilter.Data; } }
        private KnifesList List { get { return knifeListFilter.Data; } }
        private LogObjectsSetter LogObjectsSetter { get { return logObjectsSetterFilter.Data; } }
        private InputData Input { get { return inputDataFilter.Data; } }
        #endregion

        public void Initialize()
        {
            world.CreateEntityWith<KnifeFactory>().Inizialize();
            world.CreateEntityWith<KnifePositioner>().Inizialize();           
            world.CreateEntityWith<KnifeCounter>().Inizialize();
            world.CreateEntityWith<KnifeUIHandler>().Inizialize();
            world.CreateEntityWith<KnifeTimer>().Inizialize();
            world.CreateEntityWith<KnifesPreparer>().Inizialize();
            world.CreateEntityWith<KnifeForces>().Inizialize();
            world.CreateEntityWith<KnifesList>().Inizialize();
            world.CreateEntityWith<KnifeBuffer>();
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            RunLevelUnloading();
            RunLevelLoading(); 
            if (knifeSystemFilter.EntitiesCount == 0) return;
            RunEvents();
            RunSystem();         
        }

        public void RunLevelUnloading()
        {
            if (unloadLevelEvent.EntitiesCount == 0) return;

            Buffer.ActiveKnife = null;
            List.DestroyAll();
            Factory.ResetIndex();

            World.Instance.RemoveEntitiesWith<LogObjectRandomForce>();
            World.Instance.RemoveEntitiesWith<KnifeHitKnifeEvent>();
            World.Instance.RemoveEntitiesWith<KnifeHitAppleEvent>();
            World.Instance.RemoveEntitiesWith<KnifesExpiredEvent>();
        }

        private void RunLevelLoading()
        {
            if (loadLevelEvent.EntitiesCount == 0) return;

            LevelData data = loadLevelEvent.Components1[0].Data;

            Counter.Left = data.KnifesAmount;
            Counter.Total = data.KnifesAmount;
            UIHanlder.SetKnifeAmount(Counter.Left, Counter.Total);
            Timer.Reset();
            Buffer.ActiveKnife = null;

            KnifeMono knife = Factory.GetKnife();
            Counter.DecrementLeft();
            Positioner.SetKnifePosition(knife.transform, KnifePositions.Active);
            Buffer.ActiveKnife = knife;
            LogObjectsSetter.Stop(knife, false);
            List.AddKnife(knife);
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
                    List.AddKnife(knife);
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

                world.CreateEntityWith<LevelFailedEvent>();
                World.Instance.RemoveEntitiesWith<KnifeHitKnifeEvent>();
            }

            if (knifeForcesFilter.EntitiesCount != 0)
            {
                LogObjectRandomForce[] events = knifesRandomForcesEvent.Components1;

                for (int i = 0; i < knifesRandomForcesEvent.EntitiesCount; i++)
                {
                    for (int i1 = 0; i1 < events[i].Objects.Count; i1++)
                    {
                        ILogObject obj = events[i].Objects[i1];

                        LogObjectsSetter.Deactivate(obj);
                        obj.Rigidbody.AddForce(Forces.RandomForce, ForceMode.Acceleration);
                        obj.Rigidbody.AddTorque(Forces.RandomTorque, ForceMode.Acceleration);
                    }
                }

                World.Instance.RemoveEntitiesWith<LogObjectRandomForce>();
            }
        }
    }
}