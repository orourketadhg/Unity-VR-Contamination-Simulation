using System;
using com.TUDublin.VRContaminationSimulation.Common.Structs;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class SpawnerSystem: SystemBase {
        
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
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;
            
            // create a queue to store new particleSpawnData
            var particleSpawnDataQueue = new NativeQueue<ParticleSpawnData>(Allocator.Temp);
            
        }
        
        /**
         * 
         */
        [BurstCompile]
        private struct GenerateVirusParticleSpawnDataJob: IJobEntityBatch {
            [NativeSetThreadIndex]
            private int _nativeThreadIndex;

            public NativeArray<Random> randomArray;
            
            [ReadOnly] public ComponentTypeHandle<Translation> spawnerTranslationHandle;
            [ReadOnly] public BufferTypeHandle<VirusParticleData> virusParticleBufferHandle;
            [WriteOnly] public NativeQueue<ParticleSpawnData>.ParallelWriter particleSpawnData;
            
            public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
                // get thread safe random for this thread
                Random random = randomArray[_nativeThreadIndex];
                
                var spawnerPosition = batchInChunk.GetNativeArray(spawnerTranslationHandle);
                var virusParticlesBuffer = batchInChunk.GetBufferAccessor(virusParticleBufferHandle);
                
                // return random to randomArray
                randomArray[_nativeThreadIndex] = random;
            }

            private float3 CalculateParticleScale(Random random, float minValue, float maxValue) {
                return new float3(1, 1, 1) * random.NextFloat(minValue, maxValue);
            }

            private float3 CalculateInitialForce(Random random, float minValue, float maxValue) {
                throw new NotImplementedException();
            }
            
        }
        
        /**
         * Assign virus particle components the generated  
         */
        [BurstCompile]
        private struct SetParticleDataJob : IJobEntityBatch {
            [WriteOnly] public ComponentTypeHandle<Translation> translationHandle;
            [WriteOnly] public ComponentTypeHandle<PhysicsVelocity> velocityHandle;
            [WriteOnly] public ComponentTypeHandle<Scale> scaleHandle;

            public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
                var translations = batchInChunk.GetNativeArray(translationHandle);
                var velocities = batchInChunk.GetNativeArray(velocityHandle);
                var scales = batchInChunk.GetNativeArray(scaleHandle);
            }
        }
    }

    
    
}