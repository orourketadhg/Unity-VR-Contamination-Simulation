using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;
using Unity.Rendering;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    public class DecayingLifetimeSystem : SystemBase {

        private BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {

            EntityCommandBuffer ecb = _entityCommandBufferSystem.CreateCommandBuffer();
            float timeSinceLoad = (float) Time.ElapsedTime;

            Entities.ForEach((Entity entity, in DecayingLifetimeData decayingLifetimeData) => {
                float aliveTime = timeSinceLoad - decayingLifetimeData.spawnTime;
                if (aliveTime >= decayingLifetimeData.lifetime) {
                    ecb.DestroyEntity(entity);
                }
            }).ScheduleParallel();

            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);

        }
    }

}