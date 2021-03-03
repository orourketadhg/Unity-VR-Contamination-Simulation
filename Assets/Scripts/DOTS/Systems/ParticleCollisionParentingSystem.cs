using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {
    
    public class ParticleCollisionParentingSystem : SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();

            Entities
                .WithName("VirusParticleInitialSticking")
                .WithBurst()
                .WithAll<VirusParticleData>()
                .WithNone<Parent, LocalToParent>()
                .ForEach((Entity entity, int entityInQueryIndex, ref PhysicsVelocity pv, ref DecayingParticleData decayData, ref BrownianMotionData motionData, ref StickyParticleData stickyData, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer) => {
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
                    
                    ecb.AddComponent(entityInQueryIndex, entity, new Parent() {Value = other});
                    ecb.AddComponent(entityInQueryIndex,entity, new LocalToParent());
                    pv = new PhysicsVelocity();
                    ecb.RemoveComponent<PhysicsVelocity>(entityInQueryIndex, entity);
                    ecb.RemoveComponent<PhysicsMass>(entityInQueryIndex, entity);
                    ecb.RemoveComponent<PhysicsDamping>(entityInQueryIndex, entity);
                    
                    motionData.enabled = 0;
                    decayData.isDecayingParticle = 0;
                    stickyData.hasPosition = 0;
                }).ScheduleParallel();

            Entities
                .WithName("VirusParticleTransferSticking")
                .WithBurst()
                .WithAll<VirusParticleData, LocalToParent>()
                .ForEach((Entity entity, int entityInQueryIndex, ref StickyParticleData stickyData, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer, in Parent connectedBody) => {
                    if (collisionBuffer.IsEmpty) {
                        return;
                    }

                    int collisionIndex = -1;
                    for (int i = 0; i < collisionBuffer.Length; i++) {
                        if (collisionBuffer[i].CollisionState == CollisionEventState.Enter && collisionBuffer[i].GetOtherCollisionEntity(entity) != connectedBody.Value) {
                            collisionIndex = i;
                            break;
                        }
                    }

                    if (collisionIndex < 0 ) {
                        return;
                    }
                    
                    var other = collisionBuffer[collisionIndex].GetOtherCollisionEntity(entity);
                    
                    ecb.RemoveComponent<Parent>(entityInQueryIndex, entity);
                    ecb.RemoveComponent<LocalToParent>(entityInQueryIndex, entity);
                    ecb.AddComponent(entityInQueryIndex, entity, new Parent() {Value = other});
                    ecb.AddComponent(entityInQueryIndex,entity, new LocalToParent());
                    stickyData.hasPosition = 0;

                }).ScheduleParallel();

            Entities
                .WithName("VirusParticleStickingPositionUpdate")
                .WithBurst()
                .WithAll<VirusParticleData, Parent, LocalToParent>()
                .ForEach((Entity entity, int entityInQueryIndex, int nativeThreadIndex, ref Translation position, ref Rotation rotation, ref StickyParticleData stickyData) => {

                    if (stickyData.hasPosition == 1) {
                        position.Value = stickyData.position;
                        rotation.Value = stickyData.rotation;
                    }
                    else {
                        stickyData.position = position.Value;
                        stickyData.rotation = rotation.Value;
                    }
                    
                }).ScheduleParallel();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
            
        }
    }

}