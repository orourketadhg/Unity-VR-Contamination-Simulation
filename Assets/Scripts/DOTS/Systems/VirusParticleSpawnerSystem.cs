using System;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public class VirusParticleSpawnerSystem: SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            
        }
        
        protected override void OnUpdate() {
            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;

            
            Entities
                .WithBurst()
                .WithNativeDisableParallelForRestriction(randomArray)
                .ForEach((int nativeThreadIndex, int entityInQueryIndex, in ParticleSpawnerSettingsData spawnerSettings, in DynamicBuffer<VirusParticleElementData> particleBuffer, in LocalToWorld spawnerLtw) => {
                    var random = randomArray[nativeThreadIndex];

                    foreach (var virusParticleType in particleBuffer) {

                        // get number of particles to spawn this iteration
                        int particleCount = random.NextInt(virusParticleType.particleCount.x, virusParticleType.particleCount.y);

                        // create PARTICLE COUNT number of virus particles of this type
                        for (int i = 0; i < particleCount; i++) {
                            // spawn new instance of particle 
                            var instance = ecb.Instantiate(entityInQueryIndex, virusParticleType.prefab);
                            
                            // calculate particle component values
                            float instanceScale = CalculateScale(ref random, virusParticleType.particleScale);
                            var instanceTranslation = CalculateTranslation(ref random, in spawnerSettings) + spawnerLtw.Position + spawnerLtw.Forward;
                            var instanceVelocity = random.NextFloat(virusParticleType.linearEmissionForce.x, virusParticleType.linearEmissionForce.y) * spawnerLtw.Forward;

                            // set new instance components  
                            ecb.SetComponent(entityInQueryIndex, instance, new Scale() {Value = instanceScale});
                            ecb.SetComponent(entityInQueryIndex, instance, new Rotation() {Value = spawnerLtw.Rotation});
                            ecb.SetComponent(entityInQueryIndex, instance, new Translation() {Value = instanceTranslation});
                            ecb.SetComponent(entityInQueryIndex, instance, new PhysicsVelocity() {Linear = instanceVelocity});
                        }

                    }

                    randomArray[nativeThreadIndex] = random;

                }).ScheduleParallel();

            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
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