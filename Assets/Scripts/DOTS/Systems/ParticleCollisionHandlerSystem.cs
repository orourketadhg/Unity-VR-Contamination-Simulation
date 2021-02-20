using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using Unity.Entities;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(EndFramePhysicsSystem))]
    public class ParticleCollisionHandlerSystem : SystemBase {
        
        private StepPhysicsWorld _stepPhysicsWorld;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private EntityQuery _virusParticleQuery;
        
        protected override void OnCreate() {
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();

            _virusParticleQuery = GetEntityQuery(new EntityQueryDesc() {
                All = new ComponentType[] {
                    typeof(VirusParticleData)
                }
            });

        }

        protected override void OnUpdate() {

        }
        
    }
    
}