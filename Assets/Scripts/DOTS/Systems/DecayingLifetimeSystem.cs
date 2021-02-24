using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(ParticleCleanupSystem))]
    public class DecayingLifetimeSystem : SystemBase {

        private BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            float timeSinceLoad = (float) Time.ElapsedTime;
            
            // TODO: refactor to preform all cleanup of particles
            Entities
                .WithName("DecayingParticles")
                .WithBurst()
                .ForEach((Entity entity, int entityInQueryIndex, in VirusParticleData particle, in DecayingLifetimeData decayingLifetimeData) => {
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime >= decayingLifetimeData.lifetime) {

                        ecb.DestroyEntity(entityInQueryIndex, entity);
                    }
                }).ScheduleParallel();

            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);

        }
    }

}