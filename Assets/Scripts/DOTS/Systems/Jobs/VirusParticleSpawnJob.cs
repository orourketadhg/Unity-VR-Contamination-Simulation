using System;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    [BurstCompile]
    public struct VirusParticleSpawnJob : IJobEntityBatch {

        [NativeSetThreadIndex] private int _nativeThreadIndex;
        [NativeDisableParallelForRestriction] public NativeArray<Random> randomArray;
        public EntityCommandBuffer.ParallelWriter ecb;
        public float deltaTime;

        // declare expected component handlers
        [ReadOnly] public ComponentTypeHandle<BreathingMechanicInputData> inputHandle;
        [ReadOnly] public ComponentTypeHandle<LocalToWorld> spawnerLocalToWorldHandle;
        [ReadOnly] public ComponentTypeHandle<ParticleSpawnerSettingsData> spawnerSettingsHandle;
        public ComponentTypeHandle<ParticleSpawnerInternalSettingsData> spawnerInternalSettingsHandle;
        [ReadOnly] public BufferTypeHandle<VirusParticleElementData> virusParticleBufferHandle;
        
        public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
            var spawnerLocalToWorldData = batchInChunk.GetNativeArray(spawnerLocalToWorldHandle);
            var spawnerSettingsData = batchInChunk.GetNativeArray(spawnerSettingsHandle);
            var spawnerInternalSettingsData = batchInChunk.GetNativeArray(spawnerInternalSettingsHandle);
            var spawnerInputData = batchInChunk.GetNativeArray(inputHandle);
            var particleBuffer = batchInChunk.GetBufferAccessor(virusParticleBufferHandle);
            
            var random = randomArray[_nativeThreadIndex];
            
            // iterate over entities in batch
            for (int i = 0; i < batchInChunk.Count; i++) {
                var spawnerLocalToWorld = spawnerLocalToWorldData[i];
                var spawnerSettings = spawnerSettingsData[i];
                var spawnerInternalSettings = spawnerInternalSettingsData[i];
                var spawnerInput = spawnerInputData[i];
                
                // if (spawnerInput.Value) {
                //     spawnerInternalSettings.isSpawnerActive = !spawnerInternalSettings.isSpawnerActive;
                // }

                // // check remaining time
                // if (deltaTime > spawnerInternalSettings.spawnerStartTime + spawnerInternalSettings.spawnerDuration) {
                //     
                //     // if spawner has looping enabled 
                //     if (spawnerInternalSettings.isSpawnerActive && (spawnerInput.Value || spawnerSettings.breathingMechanicLooping)) {
                //         CalculateSpawningTime(ref random, ref spawnerInternalSettings, in spawnerSettings, deltaTime);
                //     }
                //     else {
                //         // disable the spawner
                //         spawnerInternalSettings.isSpawnerActive = false;
                //         continue;
                //     }
                // }

                // float currentTime = deltaTime - spawnerInternalSettings.spawnerStartTime;
                // float timeNormalized = math.lerp(0, 1, spawnerInternalSettings.spawnerDuration - currentTime );

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
                        var instanceCompositeScale = float4x4.Scale(CalculateScale(ref random, virusParticleType.particleScale));
                        var instanceTranslation = CalculateTranslation(ref random, in spawnerSettings) + spawnerLocalToWorld.Position;
                        var instanceVelocity = random.NextFloat(virusParticleType.emissionForce.x, virusParticleType.emissionForce.y) * spawnerLocalToWorld.Forward;
                        
                        // set new instance components
                        ecb.SetComponent(batchIndex, instance, new CompositeScale() {Value = instanceCompositeScale});
                        ecb.SetComponent(batchIndex, instance, new Rotation() {Value = spawnerLocalToWorld.Rotation});
                        ecb.SetComponent(batchIndex, instance, new Translation() {Value = instanceTranslation});
                        ecb.SetComponent(batchIndex, instance, new PhysicsVelocity() {Linear = instanceVelocity});
                    }
                }
            }
            
            randomArray[_nativeThreadIndex] = random;
        }
        
        private static void CalculateSpawningTime(ref Random random, ref ParticleSpawnerInternalSettingsData internalSettings, in ParticleSpawnerSettingsData settings, float deltaTime) {
            internalSettings.spawnerStartTime = deltaTime;
            internalSettings.spawnerDuration = random.NextFloat(settings.spawnerDurationRange.x, settings.spawnerDurationRange.y);
        }
        
        private static float3 CalculateScale(ref Random random, float2 particleScale) {
            float randomScale = random.NextFloat(particleScale.x, particleScale.y);
            return new float3(randomScale);
        }

        private static float3 CalculateTranslation(ref Random random, in ParticleSpawnerSettingsData spawnerSettingsData) {
            // get random random position in circle
            var randomPosition = NonUniformDiskPointPicking(ref random, spawnerSettingsData.spawnerRadius);
        
            // add random position to spawner position
            return new float3() {
                x = randomPosition.x, 
                y = randomPosition.y,
                z = 0
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