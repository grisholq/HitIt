using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
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
        private EcsFilterSingle<LogBreaker> logBreakerFilter = null;
        private EcsFilterSingle<LogLevelSettings> logSettingsFilter = null;
             
        private EcsFilterSingle<InputData> inputDataFilter = null;
        #endregion

        #region Events
        private EcsFilter<KnifeHitKnifeEvent> knifeHitKnifeEvent = null;
        private EcsFilter<KnifeHitAppleEvent> knifeHitAppleEvent = null;
        private EcsFilter<KnifeHitLogEvent> knifeHitFilter = null;

        private EcsFilter<LoadLevelEvent> loadLevelEvent = null;
        private EcsFilter<UnloadLevelEvent> unloadLevelEvent = null;

        private EcsFilter<KnifeSystemFunction> knifeSystemFilter = null;
        private EcsFilter<LogSystemFunction> logSystemFilter = null;

        private EcsFilter<KnifesExpiredEvent> knifesExpiredEvent = null;
        private EcsFilter<AllKnifesAttachedEvent> knifesAttachedEvent = null;

        private EcsFilter<LogObjectRandomForce> knifesRandomForcesEvent = null;

        private EcsFilter<LoadLevelEvent> loadLevelEvent = null;
        private EcsFilter<UnloadLevelEvent> unloadLevelEvent = null;
        #endregion

        #region Properties
        private KnifeFactory KnifeFactory { get { return knifeFactoryFilter.Data; } }
        private KnifeTimer Timer { get { return knifeTimerFilter.Data; } }
        private KnifeBuffer KnifeBuffer { get { return knifeBufferFilter.Data; } }
        private KnifePositioner Positioner { get { return knifePositionerFilter.Data; } }
        private KnifesPreparer Preparer { get { return knifePreparerFilter.Data; } }
        private KnifeUIHandler UIHanlder { get { return knifeUIFilter.Data; } }
        private KnifeCounter Counter { get { return knifeCounterFilter.Data; } }
        private KnifeForces Forces { get { return knifeForcesFilter.Data; } }
        private KnifesList List { get { return knifeListFilter.Data; } }

        private LogObjectsSetter LogObjectsSetter { get { return logObjectsSetterFilter.Data; } }
        private InputData Input { get { return inputDataFilter.Data; } }
        private LogSpawner Spawner { get { return logSpawnerFilter.Data; } }
        private LogAttacher Attacher { get { return logAttacherFilter.Data; } }
        private LogRotator Rotator { get { return logRotatorFilter.Data; } }
        private LogBuffer LogBuffer { get { return logBufferFilter.Data; } }
        private LogBreaker Breaker { get { return logBreakerFilter.Data; } }
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

        }

        public void LoadLevel()
        {

        }

        public void UnloadLevel()
        {

        }

        public void KnifeLoading()
        {

        }
        
        public void LogLoading()
        {

        }
        
        public void KnifeUnloading()
        {

        }
        
        public void LogUnloading()
        {

        }
    }
}