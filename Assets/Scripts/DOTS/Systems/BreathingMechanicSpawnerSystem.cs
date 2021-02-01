using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class BreathingMechanicSpawnerSystem: SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            
        }

        protected override void OnUpdate() {

        }
        
    }

}