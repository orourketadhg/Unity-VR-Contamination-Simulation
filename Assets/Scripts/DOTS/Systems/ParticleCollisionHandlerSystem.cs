using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Physics;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {
    
    [DisableAutoCreation]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(StatefulCollisionEventSystem))]
    [UpdateBefore(typeof(EndFramePhysicsSystem))]
    public class ParticleCollisionHandlerSystem : SystemBase {
        
        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        private StatefulCollisionEventSystem _collisionEventSystem;
        private EntityArchetype _jointEntityArchetype;
        private EntityQuery _particleCollisionQuery;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            _collisionEventSystem = World.GetOrCreateSystem<StatefulCollisionEventSystem>();

            _jointEntityArchetype = EntityManager.CreateArchetype(
                typeof(PhysicsJoint),
                typeof(PhysicsConstrainedBodyPair),
                typeof(DeleteMeData)
            );
            
            // get all entities virusParticleData & statefulCollisionEventBuffer components 
            _particleCollisionQuery = GetEntityQuery(new EntityQueryDesc() {
                All = new ComponentType[] {
                    typeof(VirusParticleData)
                }
            });
        }

        protected override void OnUpdate() {
            
            Dependency = JobHandle.CombineDependencies(Dependency, _collisionEventSystem.OutDependency);
            
            // check if any particles exist
            if (_particleCollisionQuery.CalculateEntityCount() == 0) {
                return;
            }
            
            var ecb = _entityCommandBuffer.CreateCommandBuffer();
            var jointArchetype = _jointEntityArchetype;

            Entities
                .WithBurst()
                .WithAll<VirusParticleData>()
                .ForEach((Entity entity, ref PhysicsVelocity velocity, ref StickyParticleData sticky, ref BrownianMotionData motionData, ref DecayingParticleData decayingParticleData, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer,  in Translation translation, in Rotation rotation) => {

                    if (sticky.value == Entity.Null) {

                        if (collisionBuffer.IsEmpty) {
                            return;
                        }

                        int collisionIndex = -1;
                        for (int i = 0; i < collisionBuffer.Length; i++) {
                            if (collisionBuffer[i].CollisionState == CollisionEventState.Enter) {
                                collisionIndex = i;
                                break;
                            }
                        }

                        if (collisionIndex < 0 ) {
                            return;
                        }

                        var other = collisionBuffer[collisionIndex].GetOtherCollisionEntity(entity);

                        var otherTranslation = GetComponent<Translation>(other);
                        var otherRotation = GetComponent<Rotation>(other);

                        var entityRT = new RigidTransform(rotation.Value, translation.Value);
                        var otherRT = new RigidTransform(otherRotation.Value, otherTranslation.Value);

                        var entityBF = new BodyFrame(entityRT);
                        var otherBF = new BodyFrame(otherRT);

                        var joint = PhysicsJoint.CreateHinge(entityBF, otherBF);
                        var cBP = new PhysicsConstrainedBodyPair(entity, other, false);
                        var deleteMeData = new DeleteMeData() {value = 0};

                        var jointEntity = ecb.CreateEntity(jointArchetype);
                        ecb.AddComponent(jointEntity, joint);
                        ecb.AddComponent(jointEntity, cBP);
                        
                        // ecb.AddComponent(jointEntity, deleteMeData);
                        ecb.SetComponent(other, new PhysicsVelocity());
                        
                        motionData.enabled = 0;
                        decayingParticleData.isDecayingParticle = 0;
                        sticky.value = jointEntity;
                        velocity = new PhysicsVelocity();
                    }

                }).Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
    }

}