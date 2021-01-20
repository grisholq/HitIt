﻿using LeopotamGroup.Ecs;

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

        private EcsFilter<KnifeHitLogEvent> knifeHitFilter = null;
        private EcsFilter<KnifesExpiredEvent> knifesExpiredEvent = null;
        private EcsFilter<AllKnifeAttachedEvent> knifesAttachedEvent = null;

        private LogSpawner Spawner { get { return logSpawnerFilter.Data; } }
        private LogAttacher Attacher { get { return logAttacherFilter.Data; } }
        private LogRotator Rotator { get { return logRotatorFilter.Data; } }
        private LogBuffer Buffer { get { return logBufferFilter.Data; } }
        private LogBreaker Breaker { get { return logBreakerFilter.Data; } }

        public void Initialize()
        {
            LogSpawner spawner = world.CreateEntityWith<LogSpawner>();
            LogBuffer buffer = world.CreateEntityWith<LogBuffer>();          
            world.CreateEntityWith<LogAttacher>().Inizialize();
            world.CreateEntityWith<LogRotator>().Inizialize();
            world.CreateEntityWith<LogBreaker>();

            spawner.Inizialize();
            buffer.Inizialize();

            LogMono log = spawner.GetLog();
            buffer.ActiveLog = log;
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            RunEvents();

            

            Rotator.RotateLog(Buffer.ActiveLog.transform);
        }

        public void RunEvents()
        {
            if(knifeHitFilter.EntitiesCount != 0)
            {
                KnifeHitLogEvent[] events = knifeHitFilter.Components1;

                for (int i = 0; i < knifeHitFilter.EntitiesCount; i++)
                {
                    Attacher.AttachKnife(Buffer.ActiveLog ,events[i].Knife);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
            }

            if (knifesExpiredEvent.EntitiesCount != 0)
            {
                Breaker.BreakLog(Buffer.ActiveLog);
            }
        }
    }
}