using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using Unity.Entities;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {
    
    public class ParticleCollisionHandlerSystem : SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        
        private EntityArchetype _particleJointEntityArchetype;
        private EntityQuery _particleCollisionQuery;
        
        protected override void OnCreate() {

            // Command Buffer for manipulating entity components
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            
            // keep track of entities associated with joint entities
            // _particlesWithJoints = new NativeList<Entity>(Allocator.Persistent);

            // Entity Archetype of a particle Joint entity
            _particleJointEntityArchetype = EntityManager.CreateArchetype(
                typeof(ParticleJointTag),
                typeof(PhysicsConstrainedBodyPair),
                typeof(PhysicsJoint)
            );
            
            // get all entities virusParticleData & statefulCollisionEventBuffer components 
            _particleCollisionQuery = GetEntityQuery(new EntityQueryDesc() {
                All = new ComponentType[] {
                    typeof(StatefulCollisionEventBuffer),
                    typeof(VirusParticleData)
                }
            });
        }

        protected override void OnUpdate() {

            if (_particleCollisionQuery.CalculateEntityCount() == 0) {
                return;
            }

            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var particleJointEntityArchetype = _particleJointEntityArchetype;
            
        }

        protected override void OnDestroy() {
            //_particlesWithJoints.Dispose();
        }
    }
    
}