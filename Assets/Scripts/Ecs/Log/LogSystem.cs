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
        private EcsFilterSingle<LogBreaker> logBreakerFilter = null;
        private EcsFilterSingle<LogLevelSettings> logSettingsFilter = null;

        private EcsFilter<KnifeHitLogEvent> knifeHitFilter = null;
        private EcsFilter<AllKnifesAttachedEvent> knifesAttachedEvent = null;

        private LogSpawner Spawner { get { return logSpawnerFilter.Data; } }
        private LogAttacher Attacher { get { return logAttacherFilter.Data; } }
        private LogRotator Rotator { get { return logRotatorFilter.Data; } }
        private LogBuffer Buffer { get { return logBufferFilter.Data; } }
        private LogBreaker Breaker { get { return logBreakerFilter.Data; } }
        private LogLevelSettings Settings { get { return logSettingsFilter.Data; } }

        public void Initialize()
        {
            LogSpawner spawner = world.CreateEntityWith<LogSpawner>();
            LogBuffer buffer = world.CreateEntityWith<LogBuffer>();          
            world.CreateEntityWith<LogAttacher>().Inizialize();
            world.CreateEntityWith<LogRotator>().Inizialize();
            world.CreateEntityWith<LogBreaker>().Inizialize();
            world.CreateEntityWith<LogLevelSettings>().KnifesToAttach = 10;

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
            if(knifesAttachedEvent.EntitiesCount != 0) return;

            RunEvents();

            if(Settings.KnifesToAttach == Attacher.AttachedKnifesCount)
            {
                Breaker.BreakLog(Buffer.ActiveLog);
                world.CreateEntityWith<KnifesRandomForceEvent>().Knifes = Attacher.GetAttachedKnifes();
                world.CreateEntityWith<AllKnifesAttachedEvent>();
            }

            Rotator.RotateLog(Buffer.ActiveLog);
        }

        public void RunEvents()
        {
            if(knifeHitFilter.EntitiesCount != 0)
            {
                KnifeHitLogEvent[] events = knifeHitFilter.Components1;

                for (int i = 0; i < knifeHitFilter.EntitiesCount; i++)
                {
                    Attacher.AttachKnife(Buffer.ActiveLog, events[i].Knife);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
            }       
        }
    }
}