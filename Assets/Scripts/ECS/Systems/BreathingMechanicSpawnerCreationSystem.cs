using com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Spawner;
using com.TUDublin.VRContaminationSimulation.ECS.Components.Input;
using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class BreathingMechanicSpawnerCreationSystem : SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            
        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBuffer.CreateCommandBuffer();
            var input = GetSingleton<MouthBreathInputData>();
            float deltaTime = Time.DeltaTime;

            Entities
                .ForEach((Entity entity, int entityInQueryIndex, ref MouthBreathSpawnerSettingsData mouthBreathingSpawner, in Translation spawnerPosition) => {
                    var particles = GetBuffer<VirusParticleData>(entity);

                    foreach (VirusParticleData particle in particles) {
                        Entity instance = ecb.Instantiate(particle.Prefab);
                    }
                }).Run();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);

        }
        
    }

}