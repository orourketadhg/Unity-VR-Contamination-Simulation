using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class VirusParticleSpawnerSystem : SystemBase {

        private BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;
        private InputConversionSystem _input;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }


        protected override void OnUpdate() {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            
        }

    }

}