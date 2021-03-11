using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.Util;
using Unity.Animation;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    public struct VirusParticleSpawnJob : IJobEntityBatch {

        [NativeSetThreadIndex] private int _nativeThreadIndex;
        [NativeDisableParallelForRestriction] public NativeArray<Random> randomArray;
        public EntityCommandBuffer.ParallelWriter ecb;
        public float time;

        // declare expected component handlers
        public ComponentTypeHandle<ParticleSpawnerInternalSettingsData> spawnerInternalSettingsHandle;
        [ReadOnly] public ComponentTypeHandle<LocalToWorld> spawnerLocalToWorldHandle;
        [ReadOnly] public ComponentTypeHandle<ParticleSpawnerSettingsData> spawnerSettingsHandle;
        [ReadOnly] public ComponentTypeHandle<BreathingMechanicInputData> spawnerInputHandle;
        [ReadOnly] public BufferTypeHandle<VirusParticleElement> virusParticleBufferHandle;

        public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
            
            var spawnerLocalToWorldData = batchInChunk.GetNativeArray(spawnerLocalToWorldHandle);
            var spawnerSettingsData = batchInChunk.GetNativeArray(spawnerSettingsHandle);
            var spawnerInternalSettingsData = batchInChunk.GetNativeArray(spawnerInternalSettingsHandle);
            var spawnerInputData = batchInChunk.GetNativeArray(spawnerInputHandle);
            var particleBuffer = batchInChunk.GetBufferAccessor(virusParticleBufferHandle);
            var random = randomArray[_nativeThreadIndex];
            
            // iterate over entities in batch
            for (int i = 0; i < batchInChunk.Count; i++) {
                var spawnerLocalToWorld = spawnerLocalToWorldData[i];
                var spawnerSettings = spawnerSettingsData[i];
                var spawnerInternalSettings = spawnerInternalSettingsData[i];
                var spawnerInput = spawnerInputData[i];

                // input
                if (( spawnerInput.Value == 1 ) && time > spawnerInternalSettings.timeOfLastInput + 0.5f) {
                    spawnerInternalSettings.timeOfLastInput = time;
                    spawnerInternalSettings.isSpawnerActive = (spawnerInternalSettings.isSpawnerActive == 1) ? 0 : 1;
                    
                    if (spawnerInternalSettings.isSpawnerActive == 1 && time > spawnerInternalSettings.spawnerStartTime + spawnerInternalSettings.spawnerDuration) {
                        CalculateSpawningTime(ref random, ref spawnerInternalSettings, in spawnerSettings, time);
                    }
                }
                
                if (time > spawnerInternalSettings.spawnerStartTime + spawnerInternalSettings.spawnerDuration) {
                
                    if (spawnerInternalSettings.isSpawnerActive == 1 && spawnerSettings.breathingMechanicLooping == 1) {
                        CalculateSpawningTime(ref random, ref spawnerInternalSettings, in spawnerSettings, time);
                    }
                    else {
                        spawnerInternalSettings.isSpawnerActive = 0;
                    }
                }

                // spawner has time remaining
                if (time <= spawnerInternalSettings.spawnerStartTime + spawnerInternalSettings.spawnerDuration) {
                
                    float currentTime = time - spawnerInternalSettings.spawnerStartTime;
                    float timeNormalized = ( spawnerInternalSettings.spawnerDuration - currentTime ) / spawnerInternalSettings.spawnerDuration;
                
                    // iterate over particle buffer on entity
                    for (int j = 0; j < particleBuffer[i].Length; j++) {
                        var virusParticleType = particleBuffer[i][j];
                
                        // get number of particles to spawn this iteration
                        int particleCount = random.NextInt(virusParticleType.particleCount.x, virusParticleType.particleCount.y);
                        particleCount = (int) math.ceil(AnimationCurveEvaluator.Evaluate(timeNormalized, virusParticleType.particleCountCurve) * particleCount);
                
                        // create PARTICLE COUNT number of virus particles of this type
                        for (int k = 0; k < particleCount; k++) {
                            // spawn new instance of particle 
                            var instance = ecb.Instantiate(batchIndex, virusParticleType.prefab);
                
                            // calculate particle component values
                            var instanceScale = CalculateScale(ref random, virusParticleType.particleScale);
                
                            var instanceCompositeScale = float4x4.Scale(instanceScale);
                
                            // TODO: Fix translation not following rotation
                            var instanceTranslation = CalculateTranslation(ref random, in spawnerSettings, timeNormalized) + spawnerLocalToWorld.Position;
                            var instanceRotation = spawnerLocalToWorld.Rotation;
                            var instanceLinearVelocity = CalculateLinearVelocity(ref random, in virusParticleType, spawnerLocalToWorld.Forward, timeNormalized);
                
                            // set new instance components
                            ecb.SetComponent(batchIndex, instance, new CompositeScale() {Value = instanceCompositeScale});
                            ecb.SetComponent(batchIndex, instance, new Rotation() {Value = instanceRotation});
                            ecb.SetComponent(batchIndex, instance, new Translation() {Value = instanceTranslation});
                            ecb.SetComponent(batchIndex, instance, new PhysicsVelocity() {Linear = instanceLinearVelocity});
                            ecb.SetComponent(batchIndex, instance, new VirusParticleData() {spawnTime = time});
                            
                            // particle decaying
                            if (spawnerSettings.totalDecayingVirusParticles == 1) {
                                float randomLifetime = random.NextFloat(virusParticleType.decayTime.x, virusParticleType.decayTime.y);
                                ecb.SetComponent(batchIndex, instance, new DecayingParticleData() {isDecayingParticle = 1, lifetime = randomLifetime});
                            }
                            else if (spawnerSettings.randomDecayingVirusParticles == 1) {
                                float randomDecayChance = random.NextFloat(1);
                                if (randomDecayChance < spawnerSettings.randomDecayChance) {
                                    float randomLifetime = random.NextFloat(virusParticleType.decayTime.x, virusParticleType.decayTime.y);
                                    ecb.SetComponent(batchIndex, instance, new DecayingParticleData() {isDecayingParticle = 1, lifetime = randomLifetime});
                                }
                            }
                        }
                    }
                }

                // return written values 
                spawnerInternalSettingsData[i] = spawnerInternalSettings;
            }
            
            randomArray[_nativeThreadIndex] = random;
        }
        
        private static void CalculateSpawningTime(ref Random random, ref ParticleSpawnerInternalSettingsData internalSpawnerSettings, in ParticleSpawnerSettingsData spawnerSettings, float deltaTime) {
            internalSpawnerSettings.spawnerStartTime = deltaTime;
            internalSpawnerSettings.spawnerDuration = random.NextFloat(spawnerSettings.spawnerDurationRange.x, spawnerSettings.spawnerDurationRange.y);
        }
        
        private static float3 CalculateScale(ref Random random, float2 particleScale) {
            float randomScale = random.NextFloat(particleScale.x, particleScale.y);
            return new float3(randomScale);
        }

        private static float3 CalculateLinearVelocity(ref Random random, in VirusParticleElement particleSettings, float3 direction, float time) {
            float velocity = random.NextFloat(particleSettings.emissionForce.x, particleSettings.emissionForce.y);
            float adjustedVelocity = AnimationCurveEvaluator.Evaluate(time, particleSettings.emissionForceCurve) * velocity;

            return direction * adjustedVelocity;
        }

        private static float3 CalculateTranslation(ref Random random, in ParticleSpawnerSettingsData spawnerSettingsData, float time) {
            float adjustedRadius = AnimationCurveEvaluator.Evaluate(time, spawnerSettingsData.spawnRadiusCurve) * spawnerSettingsData.spawnerRadius;
            var randomPosition = MathUtil.PointInsideRadiusCircle(ref random, adjustedRadius);
        
            // add random position to spawner position
            return new float3() {
                x = randomPosition.x, 
                y = randomPosition.y,
                z = 0
            };
        }
    }
}