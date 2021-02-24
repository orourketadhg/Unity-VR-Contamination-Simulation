﻿using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {
    
    public class ParticleCollisionHandlerSystem : SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        
        private EntityArchetype _particleJointEntityArchetype;
        private EntityQuery _particleCollisionQuery;
        
        protected override void OnCreate() {

            // Command Buffer for manipulating entity components
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

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
            
            Entities
                .WithName("ParticleJointCreation")
                .WithoutBurst()
                .ForEach((Entity entity, int entityInQueryIndex, ref VirusParticleData particleData, ref PhysicsVelocity velocity, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer, in Translation translation, in Rotation rotation) => {
                    
                    // ignore particles with no collisions
                    if (collisionBuffer.IsEmpty) {
                        return;
                    }

                    // particle doesn't have a joint
                    if (particleData.particleJoint == Entity.Null) {
                        
                        // get the first instance of a Enter collisionEvent
                        int collisionIndex = -1;
                        for (int i = 0; i < collisionBuffer.Length; i++) {
                            if (collisionBuffer[i].CollisionState == CollisionEventState.Enter) {
                                collisionIndex = i;
                                break;
                            }
                        }

                        // check if a collision was found
                        if (collisionIndex < 0) {
                            return;
                        }

                        // get the collision event
                        var collisionEvent = collisionBuffer[collisionIndex];
                        
                        var other = collisionEvent.GetOtherCollisionEntity(entity);
                        var translationB = GetComponent<Translation>(other);
                        var rotationB = GetComponent<Rotation>(other);

                        // create Physics joint between collision entities
                        var joint = CreatePhysicsJoint(translation, translationB, rotation, rotationB);
                        
                        // Body pair to constrain the entities
                        // enableCollision set to false so the bodies do not generate collision events between them
                        var constrainedBodyPair = new PhysicsConstrainedBodyPair(entity, other, false);
                        
                        // create a joint entity
                        var jointEntity = ecb.CreateEntity(entityInQueryIndex, particleJointEntityArchetype);
                        ecb.SetComponent(entityInQueryIndex, entity, new PhysicsVelocity());
                        ecb.SetComponent(entityInQueryIndex, jointEntity, joint);
                        ecb.SetComponent(entityInQueryIndex, jointEntity, constrainedBodyPair);
                        particleData.particleJoint = jointEntity;
                    }
                    
                }).Schedule();

            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }

        private static PhysicsJoint CreatePhysicsJoint(in Translation translationA, in Translation translationB, in Rotation rotationA, in Rotation rotationB) {
            var rigidTransformA = new RigidTransform(rotationA.Value, translationA.Value);
            var rigidTransformB = new RigidTransform(rotationB.Value, translationB.Value);
            
            var bodyFrameA = new BodyFrame(rigidTransformA);
            var bodyFrameB = new BodyFrame(rigidTransformB);

            return PhysicsJoint.CreateFixed(bodyFrameA, bodyFrameB);
        }
        
    }
    
}