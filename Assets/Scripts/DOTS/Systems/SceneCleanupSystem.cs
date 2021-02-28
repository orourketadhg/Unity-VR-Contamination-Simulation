﻿using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class SceneCleanupSystem : SystemBase {

        private BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {
            var ecb = _entityCommandBufferSystem.CreateCommandBuffer();
            float timeSinceLoad = (float) Time.ElapsedTime;
            
            // Remove decaying particles
            Entities
                .WithName("RemoveDecayingParticles")
                .WithBurst()
                .ForEach((Entity entity, ref VirusParticleData particle, in DecayingParticleData decayingLifetimeData) => {
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime >= decayingLifetimeData.lifetime) {
                        ecb.DestroyEntity(entity);
                    }
                }).Schedule();

            Entities
                .WithName("ParticleCleanup")
                .WithBurst()
                .ForEach((Entity entity, in VirusParticleData particle) => {
                    float aliveTime = timeSinceLoad - particle.spawnTime;
                    if (aliveTime > 30f) {
                        ecb.DestroyEntity(entity);
                    }
                }).Schedule();
            
            // Entities
            //     .WithName("JointCleanup")
            //     .WithoutBurst()
            //     .WithStructuralChanges()
            //     .ForEach((Entity entity, in PhysicsJoint joint, in PhysicsConstrainedBodyPair constrainedBodyPair) => {
            //         var entityA = constrainedBodyPair.EntityA;
            //         var entityB = constrainedBodyPair.EntityB;
            //
            //         GetComponentDataFromEntity<Translation>();
            //
            //         if (!EntityManager.Exists(entityA) || !EntityManager.Exists(entityB)) {
            //             EntityManager.DestroyEntity(entity);
            //         }
            //     }).Run();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);

        }
    }

}