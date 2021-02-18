using Unity.Entities;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StepPhysicsWorld))]
    public class ParticleCollisionHandlerSystem : SystemBase {

        private StepPhysicsWorld _stepPhysicsWorld;
        private BuildPhysicsWorld _buildPhysicsWorld;
        
        protected override void OnCreate() {
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        }

        protected override void OnUpdate() {
            
        }
        
    }

}