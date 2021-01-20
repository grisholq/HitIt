using UnityEngine;
using LeopotamGroup.Ecs;

namespace HitIt.Ecs
{
    [EcsInject]
    public class LevelSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world = null;


        public void Initialize()
        {

        }

        public void Destroy()
        {

        }

        public void Run()
        {

        }

        private void RunEvents()
        {

        }
    }
}
