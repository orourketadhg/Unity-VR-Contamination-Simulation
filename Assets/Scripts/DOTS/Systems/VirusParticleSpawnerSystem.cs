using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine.SocialPlatforms;

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
                .ForEach((int nativeThreadIndex, int entityInQueryIndex, in ParticleSpawnerSettingsData spawnerSettings, in DynamicBuffer<VirusParticleElementData> particleBuffer) => {
                    var random = randomArray[nativeThreadIndex];
                    
                    
                    foreach (var virusParticleType in particleBuffer) {

                        // get number of particles to spawn this iteration
                        int particleCount = random.NextInt(virusParticleType.particleCount.x, virusParticleType.particleCount.y);

                        // create PARTICLE COUNT number of virus particles of this type
                        for (int i = 0; i < particleCount; i++) {
                            var instance = ecb.Instantiate(entityInQueryIndex, virusParticleType.prefab);

                            var instanceScale = new Scale();
                            var instanceTranslation = new Translation();
                            var instanceVelocity = new PhysicsVelocity();
                            
                            ecb.SetComponent(entityInQueryIndex, instance, instanceScale);
                            ecb.SetComponent(entityInQueryIndex, instance, instanceTranslation);
                            ecb.SetComponent(entityInQueryIndex, instance, instanceVelocity);

                        }

                    }

                    randomArray[nativeThreadIndex] = random;

                }).ScheduleParallel();

            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
        
    }
    
}