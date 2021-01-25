using UnityEngine;
using LeopotamGroup.Ecs;
using HitIt.Other;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LogSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilterSingle<LogSpawner> logSpawnerFilter = null;
        private EcsFilterSingle<LogAttacher> logAttacherFilter = null;
        private EcsFilterSingle<LogRotator> logRotatorFilter = null;
        private EcsFilterSingle<LogBuffer> logBufferFilter = null;
        private EcsFilterSingle<LogBreaker> logBreakerFilter = null;
        private EcsFilterSingle<LogLevelSettings> logSettingsFilter = null;
        private EcsFilterSingle<LogObjectsSetter> logObjectsSetterFilter = null;
        private EcsFilterSingle<LogAttachRadii> logAttachRadiiFilter = null;

        private EcsFilterSingle<AppleFactory> appleFactoryFilter = null;
        private EcsFilterSingle<KnifeFactory> knifeFactoryFilter = null;

        private EcsFilter<KnifeHitLogEvent> knifeHitFilter = null;
        private EcsFilter<AllKnifesAttachedEvent> knifesAttachedEvent = null;
        private EcsFilter<LoadLevelEvent> loadLevelEvent = null;     
        private EcsFilter<UnloadLevelEvent> unloadLevelEvent = null;
        private EcsFilter<LogSystemFunction> logSystemFilter = null;

        private LogSpawner Spawner { get { return logSpawnerFilter.Data; } }
        private LogAttacher Attacher { get { return logAttacherFilter.Data; } }
        private LogRotator Rotator { get { return logRotatorFilter.Data; } }
        private LogBuffer Buffer { get { return logBufferFilter.Data; } }
        private LogBreaker Breaker { get { return logBreakerFilter.Data; } }
        private LogLevelSettings Settings { get { return logSettingsFilter.Data; } }
        private LogObjectsSetter LogObjectsSetter { get { return logObjectsSetterFilter.Data; } }
        private LogAttachRadii LogRadii { get { return logAttachRadiiFilter.Data; } }

        private AppleFactory AppleFactory { get { return appleFactoryFilter.Data; } }
        private KnifeFactory KnifeFactory { get { return knifeFactoryFilter.Data; } }

        public void Initialize()
        {
            world.CreateEntityWith<LogSpawner>().Inizialize();
            world.CreateEntityWith<LogBuffer>().Inizialize();          
            world.CreateEntityWith<LogAttacher>().Inizialize();
            world.CreateEntityWith<AppleFactory>().Inizialize();
            world.CreateEntityWith<LogRotator>().Inizialize();
            world.CreateEntityWith<LogBreaker>().Inizialize();
            world.CreateEntityWith<LogAttachRadii>().Inizialize();
            world.CreateEntityWith<LogLevelSettings>();
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            RunLevelUnloading();
            RunLevelLoading(); 
            if (logSystemFilter.EntitiesCount == 0) return;
            RunEvents();           
            RunSystem();
        }

        public void RunLevelUnloading()
        {
            if(unloadLevelEvent.EntitiesCount == 0) return;

            if(Buffer.ActiveLog != null) Object.Destroy(Buffer.ActiveLog.gameObject);
            Rotator.StopOperation();

            World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
            World.Instance.RemoveEntitiesWith<AllKnifesAttachedEvent>();
        }

        public void RunLevelLoading()
        {
            if (loadLevelEvent.EntitiesCount == 0) return;

            LevelData data = loadLevelEvent.Components1[0].Data;

            LogMono log = Spawner.GetLog(data.LogPrefab);
            
            Rotator.SetIterator(data.LogPattern.GetIterator());
            Settings.KnifesToAttach = data.KnifesAmount;
            Buffer.ActiveLog = log;
            
            Attacher.Reset();

            for (int i = 0; i < data.AttachableObjects.Length; i++)
            {
                AttachableObject buf = data.AttachableObjects[i];

                switch (buf.Type)
                {
                    case AttachableObjectType.Apple:

                        AppleMono apple = AppleFactory.GetApple();
                        LogObjectsSetter.Stop(apple, true);
                        Attacher.AttachObject(Buffer.ActiveLog, apple, LogRadii.AppleRadius, 0, buf.Angle);
                        break;

                    case AttachableObjectType.Knife:

                        KnifeMono knife = KnifeFactory.GetKnife();
                        LogObjectsSetter.Stop(knife, true);
                        knife.ColliderType = KnifeColliderType.Unactive;
                        Attacher.AttachObject(Buffer.ActiveLog, knife, LogRadii.KnifeRadius, 90, buf.Angle);
                        break;
                }             
            }
        }

        public void RunSystem()
        {            
            if (knifesAttachedEvent.EntitiesCount != 0) return;

            if (Settings.KnifesToAttach == Attacher.AttachedKnifesCount)
            {
                Breaker.BreakLog(Buffer.ActiveLog);
                world.CreateEntityWith<LogObjectRandomForce>().Objects = Attacher.GetAttachedLogObjects();
                world.CreateEntityWith<AllKnifesAttachedEvent>();
                world.CreateEntityWith<LevelPassedEvent>();
            }

            Rotator.Process(Buffer.ActiveLog);
        }

        public void RunEvents()
        {
            if(knifeHitFilter.EntitiesCount != 0)
            {
                KnifeHitLogEvent[] events = knifeHitFilter.Components1;

                for (int i = 0; i < knifeHitFilter.EntitiesCount; i++)
                {
                    LogObjectsSetter.Stop(events[i].Knife, true);
                    Attacher.AttachKnife(Buffer.ActiveLog, events[i].Knife);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
            }       
        }
    }
}