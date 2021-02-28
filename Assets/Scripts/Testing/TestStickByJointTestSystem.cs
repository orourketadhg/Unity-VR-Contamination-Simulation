using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.Testing {
    
    [DisableAutoCreation]
    public class TestStickByJointTestSystem : SystemBase {

        private BeginSimulationEntityCommandBufferSystem _entityCommandBuffer;
        private EntityArchetype _jointEntityArchetype;

        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();

            _jointEntityArchetype = EntityManager.CreateArchetype(
                typeof(PhysicsJoint),
                typeof(PhysicsConstrainedBodyPair)
            );

        }

        protected override void OnUpdate() {
            var ecb = _entityCommandBuffer.CreateCommandBuffer();
            var jointArchetype = _jointEntityArchetype;

            Entities
                .WithBurst()
                .ForEach((Entity entity, ref ArrowData arrow, ref PhysicsVelocity velocity, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer,  in Translation translation, in Rotation rotation) => {

                    if (arrow.joint == Entity.Null) {

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

                        if (!HasComponent<TargetData>(other)) {
                            return;
                        }
                        
                        var otherTranslation = GetComponent<Translation>(other);
                        var otherRotation = GetComponent<Rotation>(other);

                        var entityRT = new RigidTransform(rotation.Value, translation.Value);
                        var otherRT = new RigidTransform(otherRotation.Value, otherTranslation.Value);

                        var entityBF = new BodyFrame(entityRT);
                        var otherBF = new BodyFrame(otherRT);

                        var joint = PhysicsJoint.CreateFixed(entityBF, otherBF);
                        var cBP = new PhysicsConstrainedBodyPair(entity, other, false);
                        
                        var jointEntity = ecb.CreateEntity(jointArchetype);
                        ecb.AddComponent(jointEntity, joint);
                        ecb.AddComponent(jointEntity, cBP);
                        
                        ecb.SetComponent(other, new PhysicsVelocity());
                        velocity = new PhysicsVelocity();
                        
                        arrow.joint = jointEntity;
                    }

                }).Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);

        }
    }

}