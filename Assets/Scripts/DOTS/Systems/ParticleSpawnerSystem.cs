using System;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
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
using UnityEngine.Profiling;
using Random = Unity.Mathematics.Random;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class ParticleSpawnerSystem: SystemBase {

        private EntityQuery _activeHeavyParticleQuery;
        
        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;

        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            var activeHeavyParticleQueryDesc = new EntityQueryDesc() {
                All = new[] {
                    ComponentType.ReadOnly<Translation>(),
                    ComponentType.ReadOnly<ParticleSpawnerSettingsData>(),
                    ComponentType.ReadOnly<ActiveHeavyParticleData>()
                }
            };
            _activeHeavyParticleQuery = GetEntityQuery(activeHeavyParticleQueryDesc);
        }

        protected override void OnUpdate() {
            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;

            var job = new GenerateVirusParticlesJob<ActiveHeavyParticleData>() {
                randomArray = randomArray,
                ecb = ecb,
                spawnerTranslationHandle = GetComponentTypeHandle<Translation>(),
                spawnerSettingsHandle = GetComponentTypeHandle<ParticleSpawnerSettingsData>(),
                virusParticleDataHandle = GetComponentTypeHandle<ActiveHeavyParticleData>()
            };

        }
        
        private struct GenerateVirusParticlesJob<T>: IJobEntityBatch 
            where T : struct, IComponentData, IVirusParticleSettings {
            
            [NativeSetThreadIndex]
            private int _nativeThreadIndex;
            public NativeArray<Random> randomArray;
            public EntityCommandBuffer.ParallelWriter ecb;

            [ReadOnly] public ComponentTypeHandle<Translation> spawnerTranslationHandle;
            [ReadOnly] public ComponentTypeHandle<ParticleSpawnerSettingsData> spawnerSettingsHandle;
            [ReadOnly] public ComponentTypeHandle<T> virusParticleDataHandle;
            
            public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
                // get components from handles
                var spawnerTranslations = batchInChunk.GetNativeArray(spawnerTranslationHandle);
                var virusParticles = batchInChunk.GetNativeArray(virusParticleDataHandle);
                
                var random = randomArray[_nativeThreadIndex];

                randomArray[_nativeThreadIndex] = random;
            }

            private float3 CalculateParticlePosition() {
                throw new NotImplementedException();
            }
            
            private float3 CalculateParticleScale(Random random, float minValue, float maxValue) {
                throw new NotImplementedException();
            }

            private float3 CalculateInitialParticleLinearForce(Random random, float minValue, float maxValue) {
                throw new NotImplementedException();
            }
        }
    }
    
}