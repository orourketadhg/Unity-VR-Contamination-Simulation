using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class ParticleSpawnerSystem : SystemBase {

        private BeginSimulationEntityCommandBufferSystem _simulationEntityCommandBuffer;
        
        protected override void OnCreate() {
            _simulationEntityCommandBuffer = World.GetExistingSystem<BeginSimulationEntityCommandBufferSystem>();
            
        }

        protected override void OnUpdate() {

            EntityCommandBuffer commandBuffer = _simulationEntityCommandBuffer.CreateCommandBuffer();

            Entities
                .WithName("ParticleSpawner")
                .ForEach((in ParticleSpawnerData spawner, in InputData inputData, in Translation translation) => {

                    if (inputData.TriggerPress) {
                        // create the new particle entity
                        Entity newParticle = commandBuffer.Instantiate(spawner.Entity);
                        commandBuffer.SetComponent(newParticle, new Translation() {
                            Value = translation.Value
                        });
                    }

                }).Schedule();
            
            _simulationEntityCommandBuffer.AddJobHandleForProducer(Dependency);

        }

    }

}