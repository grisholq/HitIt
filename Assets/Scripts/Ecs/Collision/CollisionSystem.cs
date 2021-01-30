
using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class CollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;

        private EcsFilter<KnifeHitKnifeEvent> knifeHitKnifeEvent = null;
        private EcsFilter<KnifeHitLogEvent> knifeHitLogEvent = null;

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
            if (knifeHitLogEvent.EntitiesCount == 0 || knifeHitKnifeEvent.EntitiesCount == 0) return;

            KnifeHitKnifeEvent knifeHit = knifeHitKnifeEvent.Components1[0];
            KnifeHitLogEvent logHit = knifeHitLogEvent.Components1[0];

            if (knifeHit.Knife == logHit.Knife) World.Instance.RemoveEntitiesWith<KnifeHitLogEvent>();
        }
    }
}