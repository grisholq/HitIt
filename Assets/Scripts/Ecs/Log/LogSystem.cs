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
        
        private EcsFilterSingle<AppleFactory> appleFactoryFilter = null;
        private EcsFilterSingle<KnifeFactory> knifeFactoryFilter = null;

        private EcsFilter<KnifeHitLogEvent> knifeHitFilter = null;
        private EcsFilter<AllKnifesAttachedEvent> knifesAttachedEvent = null;

        private LogSpawner Spawner { get { return logSpawnerFilter.Data; } }
        private LogAttacher Attacher { get { return logAttacherFilter.Data; } }
        private LogRotator Rotator { get { return logRotatorFilter.Data; } }
        private LogBuffer Buffer { get { return logBufferFilter.Data; } }
        private LogBreaker Breaker { get { return logBreakerFilter.Data; } }
        private LogLevelSettings Settings { get { return logSettingsFilter.Data; } }


        private AppleFactory AppleFactory { get { return appleFactoryFilter.Data; } }
        private KnifeFactory KnifeFactory { get { return knifeFactoryFilter.Data; } }

        public void Initialize()
        {
            NextLevelDateEvent data = new NextLevelDateEvent();
            data.LogKnifeAngle = 45;

            LogSpawner spawner = world.CreateEntityWith<LogSpawner>();
            LogBuffer buffer = world.CreateEntityWith<LogBuffer>();          
            LogAttacher attacher = world.CreateEntityWith<LogAttacher>();
            AppleFactory appleFactory = world.CreateEntityWith<AppleFactory>();
            world.CreateEntityWith<LogRotator>().Inizialize();
            world.CreateEntityWith<LogBreaker>().Inizialize();
            world.CreateEntityWith<LogLevelSettings>().KnifesToAttach = 10;

            spawner.Inizialize();
            buffer.Inizialize();
            attacher.Inizialize();
            appleFactory.Inizialize();

            LogMono log = spawner.GetLog();
            buffer.ActiveLog = log;
            attacher.AttachObject(buffer.ActiveLog, appleFactory.GetApple(), 1.1f, 0, -20);
            attacher.AttachObject(buffer.ActiveLog, KnifeFactory.GetKnife(), 1.1f, -90, 120);
            attacher.AttachObject(buffer.ActiveLog, KnifeFactory.GetKnife(), 1.1f, -90, 60);
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