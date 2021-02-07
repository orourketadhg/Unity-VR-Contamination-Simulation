using Unity.Entities;


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
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
            
        }
        
    }
    
}