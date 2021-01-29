using LeopotamGroup.Ecs;
using UnityEngine;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LoaderSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        #region SingleFilters
        //Creators
        private EcsFilterSingle<KnifeFactory> knifeFactoryFilter = null;
        private EcsFilterSingle<AppleFactory> appleFactoryFilter = null;
        private EcsFilterSingle<LogSpawner> logSpawnerFilter = null;

        //Buffers
        private EcsFilterSingle<KnifeBuffer> knifeBufferFilter = null;
        private EcsFilterSingle<LogBuffer> logBufferFilter = null;

        //Data
        private EcsFilterSingle<LogAttachRadii> logAttachRadiiFilter = null;
        private EcsFilterSingle<KnifeForces> knifeForcesFilter = null;

        //Other knifes
        private EcsFilterSingle<KnifeTimer> knifeTimerFilter = null;       
        private EcsFilterSingle<KnifePositioner> knifePositionerFilter = null;
        private EcsFilterSingle<KnifesPreparer> knifePreparerFilter = null;
        private EcsFilterSingle<KnifeUIHandler> knifeUIFilter = null;
        private EcsFilterSingle<KnifeCounter> knifeCounterFilter = null;       
        private EcsFilterSingle<KnifesList> knifeListFilter = null;

        //Other logs
        private EcsFilterSingle<LogObjectsSetter> logObjectsSetterFilter = null;     
        private EcsFilterSingle<LogAttacher> logAttacherFilter = null;
        private EcsFilterSingle<LogRotator> logRotatorFilter = null;
        private EcsFilterSingle<LogLevelSettings> logSettingsFilter = null;           
        #endregion

        #region Events
        private EcsFilter<LoadLevelEvent> loadLevelEvent = null;
        private EcsFilter<UnloadLevelEvent> unloadLevelEvent = null;
        #endregion

        #region Properties
        //Knife
        private KnifeFactory KnifeFactory { get { return knifeFactoryFilter.Data; } }
        private KnifeTimer Timer { get { return knifeTimerFilter.Data; } }
        private KnifeBuffer KnifeBuffer { get { return knifeBufferFilter.Data; } }
        private KnifePositioner Positioner { get { return knifePositionerFilter.Data; } }
        private KnifesPreparer Preparer { get { return knifePreparerFilter.Data; } }
        private KnifeUIHandler UIHanlder { get { return knifeUIFilter.Data; } }
        private KnifeCounter Counter { get { return knifeCounterFilter.Data; } }
        private KnifeForces Forces { get { return knifeForcesFilter.Data; } }
        private KnifesList List { get { return knifeListFilter.Data; } }

        //Log
        private LogObjectsSetter LogObjectsSetter { get { return logObjectsSetterFilter.Data; } }
        private LogSpawner Spawner { get { return logSpawnerFilter.Data; } }
        private LogAttacher Attacher { get { return logAttacherFilter.Data; } }
        private LogRotator Rotator { get { return logRotatorFilter.Data; } }
        private LogBuffer LogBuffer { get { return logBufferFilter.Data; } }
        private LogLevelSettings Settings { get { return logSettingsFilter.Data; } }
        private LogAttachRadii LogRadii { get { return logAttachRadiiFilter.Data; } }
        private AppleFactory AppleFactory { get { return appleFactoryFilter.Data; } }
        #endregion

        public void Initialize()
        {
            
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
            if (loadLevelEvent.EntitiesCount != 0)
            {
                LoadLevel();
                World.Instance.RemoveEntitiesWith<LoadLevelEvent>();
            }

            if(unloadLevelEvent.EntitiesCount != 0)
            {
                UnloadLevel();
                World.Instance.RemoveEntitiesWith<UnloadLevelEvent>();
            }
        }

        public void LoadLevel()
        {
            KnifeLoading();
            LogLoading();
        }

        public void UnloadLevel()
        {
            KnifeUnloading();
            LogUnloading();
        }

        public void KnifeLoading()
        {
            if (loadLevelEvent.EntitiesCount == 0) return;

            LevelData data = loadLevelEvent.Components1[0].LevelData;

            Counter.Left = data.KnifesAmount;
            Counter.Total = data.KnifesAmount;
            UIHanlder.SetKnifeAmount(Counter.Left, Counter.Total);
            Timer.Reset();

            KnifeMono knife = KnifeFactory.GetKnife();
            Counter.DecrementLeft();
            Positioner.SetKnifePosition(knife.transform, KnifePositions.Active);
            KnifeBuffer.ActiveKnife = knife;
            LogObjectsSetter.Stop(knife, false);
            List.AddKnife(knife);
        }

        public void KnifeUnloading()
        {
            if (unloadLevelEvent.EntitiesCount == 0) return;

            KnifeBuffer.ActiveKnife = null;
            List.DestroyAll();
            KnifeFactory.ResetIndex();

            World.Instance.RemoveEntitiesWith<LogObjectRandomForce>();
            World.Instance.RemoveEntitiesWith<KnifeHitKnifeEvent>();
            World.Instance.RemoveEntitiesWith<KnifeHitAppleEvent>();
            World.Instance.RemoveEntitiesWith<KnifesExpiredEvent>();
        }

        public void LogLoading()
        {
            if (loadLevelEvent.EntitiesCount == 0) return;

            LevelData data = loadLevelEvent.Components1[0].LevelData;

            LogMono log = Spawner.GetLog(data.LogPrefab);

            Rotator.SetIterator(data.LogPattern.GetIterator());
            Settings.KnifesToAttach = data.KnifesAmount;
            LogBuffer.ActiveLog = log;

            Attacher.Reset();

            for (int i = 0; i < data.AttachableObjects.Length; i++)
            {
                AttachableObject buf = data.AttachableObjects[i];

                switch (buf.Type)
                {
                    case AttachableObjectType.Apple:

                        AppleMono apple = AppleFactory.GetApple();
                        LogObjectsSetter.Stop(apple, true);
                        Attacher.AttachObject(LogBuffer.ActiveLog, apple, LogRadii.AppleRadius, 0, buf.Angle);
                        break;

                    case AttachableObjectType.Knife:

                        KnifeMono knife = KnifeFactory.GetAttachKnife();
                        LogObjectsSetter.Stop(knife, true);
                        knife.ColliderType = KnifeColliderType.Unactive;
                        Attacher.AttachObject(LogBuffer.ActiveLog, knife, LogRadii.KnifeRadius, 90, buf.Angle);
                        break;
                }
            }
        }
          
        public void LogUnloading()
        {
            if (unloadLevelEvent.EntitiesCount == 0) return;

            if (LogBuffer.ActiveLog != null) Object.Destroy(LogBuffer.ActiveLog.gameObject);
            Rotator.StopOperation();

            World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
            World.Instance.RemoveEntitiesWith<AllKnifesAttachedEvent>();
        }
    }
}