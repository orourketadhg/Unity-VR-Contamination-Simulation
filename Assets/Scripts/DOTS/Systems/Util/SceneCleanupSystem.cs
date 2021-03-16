using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Particles;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Util {
    
    [UpdateAfter(typeof(ParticleCollisionParentingSystem))]
    public class SceneCleanupSystem : SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {
            var ecb = _entityCommandBufferSystem.CreateCommandBuffer();
            float timeSinceLoad = (float) Time.ElapsedTime;
            
            // Remove free decaying particles
            Entities
                .WithName("RemoveDecayingParticles")
                .WithBurst()
                .WithNone<Parent, LocalToParent>()
                .ForEach((Entity entity, ref VirusParticleData particle, in DecayingParticleData decayData) => {
                    if (decayData.isDecayingParticle == 0) {
                        return;
                    }
                    
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime >= decayData.lifetime) {
                        ecb.DestroyEntity(entity);
                    }
                }).Schedule();
            
            // Remove long lived particles
            Entities
                .WithName("RemoveLongLivedParticles")
                .WithBurst()
                .ForEach((Entity entity, ref VirusParticleData particle) => {
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime >= 60f) {
                        ecb.DestroyEntity(entity);
                    }
                }).Schedule();
            
            // Remove stuck particles
            Entities
                .WithName("RemovingStuckParticles")
                .WithAll<Parent, LocalToParent, DecayingParticleData>()
                .ForEach((Entity entity, ref VirusParticleData particle, ref DecayingParticleData decayData) => {
                    
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime > 20f) {
                        ecb.RemoveComponent<Parent>(entity);
                        ecb.RemoveComponent<LocalToParent>(entity);
                        decayData.isDecayingParticle = 1;
                    }
                }).Schedule();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);

        }
    }

}