﻿using UnityEngine;
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
        private EcsFilterSingle<KnifeSounds> knifeSoundsFilter = null;
        private EcsFilterSingle<LogObjectsSetter> logObjectsSetterFilter = null;
        private EcsFilterSingle<InputData> inputDataFilter = null;
        #endregion

        #region Events
        private EcsFilter<KnifeHitKnifeEvent> knifeHitKnifeEvent = null;
        private EcsFilter<KnifeHitAppleEvent> knifeHitAppleEvent = null;
        private EcsFilter<KnifesExpiredEvent> knifesExpiredEvent = null;
        private EcsFilter<LogObjectRandomForce> knifesRandomForcesEvent = null;
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
        private KnifeSounds Sounds { get { return knifeSoundsFilter.Data; } }
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
            world.CreateEntityWith<KnifeSounds>().Inizialize();
            world.CreateEntityWith<LogObjectsSetter>();
            world.CreateEntityWith<KnifeBuffer>();
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            RunEvents();
            if (knifeSystemFilter.EntitiesCount == 0) return;          
            RunSystem();         
        }
     
        private void RunSystem()
        {
            if (knifesExpiredEvent.EntitiesCount != 0) return;

            if (Input.Pressed && Timer.Update())
            {
                RunKnifeThrowing();
                RunKnifeCreating();            
            }

            if (Buffer.ActiveKnife != null) Preparer.MoveKnifeToActivePosition(Buffer.ActiveKnife);
        }

        private void RunKnifeThrowing()
        {
            if (Buffer.ActiveKnife != null)
            {
                Positioner.SetKnifePosition(Buffer.ActiveKnife.transform, KnifePositions.Active);
                LogObjectsSetter.Activate(Buffer.ActiveKnife);
                Buffer.ActiveKnife.Rigidbody.AddForce(Forces.ThrowForce, ForceMode.Acceleration);
                Buffer.ActiveKnife = null;
            }
        }
    
        private void RunKnifeCreating()
        {
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

        private void RunEvents()
        {
            RunAppleHitEvent();
            RunKnifeHitEvent();
            RunLogForceEvent();        
        }

        private void RunAppleHitEvent()
        {
            if (knifeHitAppleEvent.EntitiesCount != 0)
            {
                Sounds.PlayAppleHitSound();

                KnifeHitAppleEvent[] events = knifeHitAppleEvent.Components1;

                for (int i = 0; i < knifeHitAppleEvent.EntitiesCount; i++)
                {
                    AppleMono apple = events[i].Apple;

                    apple.Rigidbody.velocity = Vector3.zero;
                    LogObjectsSetter.Deactivate(apple);
                }

                world.CreateEntityWith<AddAppleEvent>();
                World.Instance.RemoveEntitiesWith<KnifeHitAppleEvent>();
            }
        }
        
        private void RunKnifeHitEvent()
        {
            if (knifeHitKnifeEvent.EntitiesCount != 0)
            {
                Vibration.Vibrate(50);
                Sounds.PlayKnifeHitSound();

                KnifeHitKnifeEvent[] events = knifeHitKnifeEvent.Components1;

                for (int i = 0; i < knifeHitKnifeEvent.EntitiesCount; i++)
                {
                    KnifeMono knife = events[i].Knife;

                    knife.Rigidbody.velocity = Vector3.zero;
                    LogObjectsSetter.Deactivate(knife);
                    knife.Rigidbody.AddForce(Forces.RicochetForce, ForceMode.Acceleration);
                    knife.Rigidbody.AddTorque(Forces.RicochetTorque, ForceMode.Acceleration);
                }

                Debug.Log(1);
                world.CreateEntityWith<NewScoreEvent>();
                world.CreateEntityWith<ResetScoreEvent>();
                world.CreateEntityWith<LevelFailedEvent>();
                World.Instance.RemoveEntitiesWith<KnifeHitKnifeEvent>();
            }
        }
        
        private void RunLogForceEvent()
        {
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