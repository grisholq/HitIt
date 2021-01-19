using UnityEngine;
using LeopotamGroup.Ecs;

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
        
        private EcsFilter<KnifeHitLogEvent> knifeHitFilter = null;

        public void Initialize()
        {
            LogSpawner spawner = world.CreateEntityWith<LogSpawner>();
            LogBuffer buffer = world.CreateEntityWith<LogBuffer>();          
            world.CreateEntityWith<LogAttacher>().Inizialize();
            world.CreateEntityWith<LogRotator>().Inizialize();

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

            LogSpawner spawner = logSpawnerFilter.Data;
            LogAttacher attacher = logAttacherFilter.Data;
            LogRotator rotator = logRotatorFilter.Data;
            LogBuffer buffer = logBufferFilter.Data;

            rotator.RotateLog(buffer.ActiveLog.transform);
        }

        public void RunEvents()
        {
            if(knifeHitFilter.EntitiesCount != 0)
            {
                KnifeHitLogEvent[] events = knifeHitFilter.Components1;
                LogAttacher attacher = logAttacherFilter.Data;
                LogBuffer buffer = logBufferFilter.Data;

                for (int i = 0; i < knifeHitFilter.EntitiesCount; i++)
                {
                    attacher.AttachKnife(buffer.ActiveLog ,events[i].Knife);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
            }
        }
    }
}