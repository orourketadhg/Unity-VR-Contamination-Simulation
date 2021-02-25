using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class SceneCleanupSystem : SystemBase {

        private BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            float timeSinceLoad = (float) Time.ElapsedTime;
            
            // Remove decaying particles
            Entities
                .WithName("RemoveDecayingParticles")
                .WithBurst()
                .ForEach((Entity entity, int entityInQueryIndex, in VirusParticleData particle, in DecayingLifetimeData decayingLifetimeData) => {
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime >= decayingLifetimeData.lifetime) {
                        ecb.DestroyEntity(entityInQueryIndex, entity);
                    }
                }).ScheduleParallel();
            
            Entities
                .WithName("ParticleCleanup")
                .WithBurst()
                .ForEach((Entity entity, int entityInQueryIndex, in VirusParticleData particle) => {
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime > 30f) {
                        ecb.DestroyEntity(entityInQueryIndex, entity);
                    }
                }).ScheduleParallel();

            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);

        }
    }

}