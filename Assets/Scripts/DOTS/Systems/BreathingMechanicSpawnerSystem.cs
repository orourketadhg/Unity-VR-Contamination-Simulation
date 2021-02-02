using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using com.TUDublin.VRContaminationSimulation.Common.Structs;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class BreathingMechanicSpawnerSystem: SystemBase {
        
        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;

        private EntityQuery _spawnerQuery;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            // query spawner entities
            var spawnerSettingsQueryDesc = new EntityQueryDesc() {
                All = new[] {
                    ComponentType.ReadOnly<Translation>(),  
                    ComponentType.ReadOnly<VirusParticleData>(),
                },
                Any = new[] {
                    ComponentType.ReadOnly<MouthBreathSpawnerSettingsData>()
                }
            };
            _spawnerQuery = GetEntityQuery(spawnerSettingsQueryDesc);

        }

        protected override void OnUpdate() {
            
            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            
            // create a queue to store new particleSpawnData
            var particleSpawnDataQueue = new NativeQueue<ParticleSpawnData>(Allocator.Temp);
            
        }
        
        [BurstCompile]
        private struct GenerateVirusParticleSpawnData: IJobEntityBatch {

            [ReadOnly] public ComponentTypeHandle<Translation> spawnerTranslationHandle;
            [ReadOnly] public BufferTypeHandle<VirusParticleData> virusParticleBufferHandle;


            // OUTPUT - Store generated data for new particles
            [WriteOnly] public NativeQueue<ParticleSpawnData>.ParallelWriter particleSpawnData;
            
            public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
                var spawnerPosition = batchInChunk.GetNativeArray(spawnerTranslationHandle);
                var virusParticlesBuffer = batchInChunk.GetBufferAccessor(virusParticleBufferHandle);

            }
        }


        [BurstCompile]
        private struct SetVirusParticleData : IJobEntityBatch {

            [WriteOnly] public ComponentTypeHandle<Translation> translationHandle;
            [WriteOnly] public ComponentTypeHandle<PhysicsVelocity> velocityHandle;
            [WriteOnly] public ComponentTypeHandle<Scale> scaleHandle;

            public NativeArray<Entity> virusParticles;

            public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
                var translations = batchInChunk.GetNativeArray(translationHandle);
                var velocities = batchInChunk.GetNativeArray(velocityHandle);
                var scales = batchInChunk.GetNativeArray(scaleHandle);
                
            }
        }
    }

    
    
}