using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Physics;
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

            var ecb = _entityCommandBufferSystem.CreateCommandBuffer();

            Entities
                .WithoutBurst()
                .WithAll<VirusParticleData, StickyParticleData>()
                .WithNone<Parent, LocalToParent>()
                .ForEach((Entity entity, ref PhysicsVelocity pv, ref DecayingParticleData decayData, ref BrownianMotionData motionData, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer) => {
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
                    
                    ecb.AddComponent(entity, new Parent() {Value = other});
                    ecb.AddComponent(entity, new LocalToParent());
                    pv = new PhysicsVelocity();
                    ecb.RemoveComponent<PhysicsVelocity>(entity);
                    ecb.RemoveComponent<PhysicsMass>(entity);
                    ecb.RemoveComponent<PhysicsDamping>(entity);
                    
                    motionData.enabled = 0;
                    decayData.isDecayingParticle = 0;

                }).Schedule();
            
            
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
            
        }
    }

}