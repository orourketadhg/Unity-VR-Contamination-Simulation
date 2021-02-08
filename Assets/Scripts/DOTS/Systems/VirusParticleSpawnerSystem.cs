using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public class VirusParticleSpawnerSystem: SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        private EntityQuery spawnerQuery;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            var queryDesc = new EntityQueryDesc() {
                All = new ComponentType[] {
                    ComponentType.ReadOnly<LocalToWorld>(),
                    ComponentType.ReadOnly<ParticleSpawnerSettingsData>(),
                    ComponentType.ReadOnly<VirusParticleElementData>(), 
                }
            };
            spawnerQuery = GetEntityQuery(queryDesc);

        }
        
        protected override void OnUpdate() {
            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;

            var spawnerLocalToWorldHandle = GetComponentTypeHandle<LocalToWorld>();
            var spawnerSettingsHandle = GetComponentTypeHandle<ParticleSpawnerSettingsData>();
            var virusParticleBufferHandle = GetBufferTypeHandle<VirusParticleElementData>();

            var particleSpawnJobHandle = new VirusParticleSpawnJob() {
                randomArray = randomArray,
                ecb = ecb,
                spawnerLocalToWorldHandle = spawnerLocalToWorldHandle,
                spawnerSettingsHandle = spawnerSettingsHandle,
                virusParticleBufferHandle = virusParticleBufferHandle
            };

            var particleSpawnJobHandleDependency = particleSpawnJobHandle.ScheduleParallel(spawnerQuery, 1, Dependency);
            Dependency = JobHandle.CombineDependencies(Dependency, particleSpawnJobHandleDependency);
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
        
        [BurstCompile]
        private struct VirusParticleSpawnJob : IJobEntityBatch {

            [NativeSetThreadIndex] private int _nativeThreadIndex;
            [NativeDisableParallelForRestriction] public NativeArray<Random> randomArray;
            public EntityCommandBuffer.ParallelWriter ecb;

            [ReadOnly] public ComponentTypeHandle<LocalToWorld> spawnerLocalToWorldHandle;
            [ReadOnly] public ComponentTypeHandle<ParticleSpawnerSettingsData> spawnerSettingsHandle;
            [ReadOnly] public BufferTypeHandle<VirusParticleElementData> virusParticleBufferHandle;
            
            public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
                var spawnerLocalToWorlds = batchInChunk.GetNativeArray(spawnerLocalToWorldHandle);
                var spawnerSettings = batchInChunk.GetNativeArray(spawnerSettingsHandle);
                var particleBuffer = batchInChunk.GetBufferAccessor(virusParticleBufferHandle);
                
                var random = randomArray[_nativeThreadIndex];

                // iterate over entities in batch
                for (int i = 0; i < batchInChunk.Count; i++) {
                    
                    var spawnerLocalToWorld = spawnerLocalToWorlds[i];
                    var spawnerSettingData = spawnerSettings[i];
                    
                    // iterate over particle buffer on entity
                    for (int j = 0; j < particleBuffer[i].Length; j++) {
                        var virusParticleType = particleBuffer[i][j];
                        
                        // get number of particles to spawn this iteration
                        int particleCount = random.NextInt(virusParticleType.particleCount.x, virusParticleType.particleCount.y);

                        // create PARTICLE COUNT number of virus particles of this type
                        for (int k = 0; k < particleCount; k++) {
                            // spawn new instance of particle 
                            var instance = ecb.Instantiate(batchIndex, virusParticleType.prefab);

                            // calculate particle component values
                            float instanceScale = CalculateScale(ref random, virusParticleType.particleScale);
                            var instanceTranslation = CalculateTranslation(ref random, in spawnerSettingData) + spawnerLocalToWorld.Position + spawnerLocalToWorld.Forward;
                            var instanceVelocity = random.NextFloat(virusParticleType.linearEmissionForce.x, virusParticleType.linearEmissionForce.y) * spawnerLocalToWorld.Forward;

                            // set new instance components  
                            // ecb.SetComponent(batchIndex, instance, new Scale() {Value = instanceScale});
                            ecb.SetComponent(batchIndex, instance, new Rotation() {Value = spawnerLocalToWorld.Rotation});
                            ecb.SetComponent(batchIndex, instance, new Translation() {Value = instanceTranslation});
                            ecb.SetComponent(batchIndex, instance, new PhysicsVelocity() {Linear = instanceVelocity});
                        }
                    }
                    
                }
                
                randomArray[_nativeThreadIndex] = random;
            }
            
            private static float CalculateScale(ref Random random, float2 particleScale) {
                return random.NextFloat(particleScale.x, particleScale.y);
            }
        
            private static float3 CalculateTranslation(ref Random random, in ParticleSpawnerSettingsData spawnerSettingsData) {
                // get random random position in circle
                var randomPosition = NonUniformDiskPointPicking(ref random, spawnerSettingsData.SpawnerRadius);
            
                // add random position to spawner position
                return new float3() {
                    x = randomPosition.x, 
                    y = 0,
                    z = randomPosition.y
                };
            }
        
            private static float2 NonUniformDiskPointPicking(ref Random random, float radius) {
                // generate random alpha value [0-2π]
                float a = 2 * math.PI * random.NextFloat();

                float x = radius * math.cos(a);
                float y = radius * math.sin(a);

                return new float2() {x = x, y = y};
            }
        }
    }
    
}