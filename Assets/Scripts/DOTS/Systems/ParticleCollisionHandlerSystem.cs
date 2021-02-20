using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(EndFramePhysicsSystem))]
    public class ParticleCollisionHandlerSystem : SystemBase {
        
        private StepPhysicsWorld _stepPhysicsWorld;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private EntityQuery _virusParticleQuery;

        private NativeList<VirusParticleCollisionEvent> _particleCollisionEvents;

        protected override void OnCreate() {
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();

            _virusParticleQuery = GetEntityQuery(new EntityQueryDesc() {
                All = new ComponentType[] {
                    typeof(VirusParticleData)
                }
            });

            _particleCollisionEvents = new NativeList<VirusParticleCollisionEvent>(Allocator.Persistent);
        }

        protected override void OnUpdate() {

            if (_virusParticleQuery.CalculateEntityCount() == 0) {
                return;
            }
            
            Debug.Log(_particleCollisionEvents.Length);
            _particleCollisionEvents.Clear();

            var collisionEvents = _particleCollisionEvents;

            var virusParticleCollisionJob = new VirusParticleCollisionEventCollectionJob() {
                particleCollisionEvents = collisionEvents,
                particleGroup = GetComponentDataFromEntity<VirusParticleData>(true),
            };
            
            var collisionDependency = virusParticleCollisionJob.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, Dependency);
            
            
            
        }

        protected override void OnDestroy() {
            _particleCollisionEvents.Dispose();
        }

    }
    
    

}