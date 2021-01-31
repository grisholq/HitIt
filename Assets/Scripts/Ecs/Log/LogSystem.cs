using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LogSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        #region SingleFilters
        private EcsFilterSingle<LogAttacher> logAttacherFilter = null;
        private EcsFilterSingle<LogRotator> logRotatorFilter = null;
        private EcsFilterSingle<LogBuffer> logBufferFilter = null;
        private EcsFilterSingle<LogBreaker> logBreakerFilter = null;
        private EcsFilterSingle<LogLevelSettings> logSettingsFilter = null;
        private EcsFilterSingle<LogObjectsSetter> logObjectsSetterFilter = null;
        private EcsFilterSingle<LogSounds> logSoundsFilter = null;
        #endregion

        #region Events
        private EcsFilter<KnifeHitLogEvent> knifeHitFilter = null;
        private EcsFilter<AllKnifesAttachedEvent> knifesAttachedEvent = null;
        private EcsFilter<LogSystemFunction> logSystemFilter = null;
        #endregion

        #region Properties
        private LogAttacher Attacher { get { return logAttacherFilter.Data; } }
        private LogRotator Rotator { get { return logRotatorFilter.Data; } }
        private LogBuffer Buffer { get { return logBufferFilter.Data; } }
        private LogBreaker Breaker { get { return logBreakerFilter.Data; } }
        private LogLevelSettings Settings { get { return logSettingsFilter.Data; } }
        private LogObjectsSetter LogObjectsSetter { get { return logObjectsSetterFilter.Data; } }
        private LogSounds Sounds { get { return logSoundsFilter.Data; } }
        #endregion

        public void Initialize()
        {
            world.CreateEntityWith<LogSpawner>().Inizialize();
            world.CreateEntityWith<LogBuffer>().Inizialize();          
            world.CreateEntityWith<LogAttacher>().Inizialize();
            world.CreateEntityWith<AppleFactory>().Inizialize();
            world.CreateEntityWith<LogRotator>().Inizialize();
            world.CreateEntityWith<LogBreaker>().Inizialize();
            world.CreateEntityWith<LogAttachRadii>().Inizialize();
            world.CreateEntityWith<LogSounds>().Inizialize();
            world.CreateEntityWith<LogLevelSettings>();
        }

        public void Destroy()
        {

        }

        public void Run()
        {
            if (logSystemFilter.EntitiesCount == 0) return;
            RunEvents();           
            RunSystem();
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
                Sounds.PlayLogCrackSound();
            }

            Rotator.Process(Buffer.ActiveLog);
        }

        public void RunEvents()
        {
            if(knifeHitFilter.EntitiesCount != 0)
            {
                Sounds.PlayLogHitSound();
                KnifeHitLogEvent[] events = knifeHitFilter.Components1;

                for (int i = 0; i < knifeHitFilter.EntitiesCount; i++)
                {
                    Vibration.Vibrate(50);
                    LogObjectsSetter.Stop(events[i].Knife, true);
                    Attacher.AttachKnife(Buffer.ActiveLog, events[i].Knife);
                }

                World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
            }       
        }
    }
}