using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    public class ParticleCollisionHandlerSystem : SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        private EntityQuery _particleCollisionQuery;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            
            // get all entities virusParticleData & statefulCollisionEventBuffer components 
            _particleCollisionQuery = GetEntityQuery(new EntityQueryDesc() {
                All = new ComponentType[] {
                    typeof(StatefulCollisionEventBuffer),
                    typeof(VirusParticleData)
                }
            });
        }

        protected override void OnUpdate() {
            
            // check if any particles exist
            if (_particleCollisionQuery.CalculateEntityCount() == 0) {
                return;
            }
            
            var ecb = _entityCommandBuffer.CreateCommandBuffer();

            Entities
                .WithName("ParticleCollisionHandler")
                .WithoutBurst()
                .WithAll<ParticleCollectorTag>()
                .ForEach((Entity entity, int entityInQueryIndex, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer) => {

                    if (collisionBuffer.IsEmpty) {
                        return;
                    }
                    
                    // get the first instance of a Enter collisionEvent
                    for (int i = 0; i < collisionBuffer.Length; i++) {
                        
                        // check for enter type collision
                        if (collisionBuffer[i].CollisionState == CollisionEventState.Enter) {
                            var other = collisionBuffer[i].GetOtherCollisionEntity(entity);
                            
                            // check if the collision entity is a particle
                            if (!HasComponent<VirusParticleData>(other)) {
                                continue;
                            }
                            
                            ecb.AddComponent(other, new Parent() {Value = entity});
                            ecb.AddComponent(other, new LocalToParent());
                            ecb.RemoveComponent(other, new ComponentTypes(typeof(PhysicsMass), typeof(PhysicsVelocity), typeof(PhysicsDamping)));
                        }
                    }
                }).Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
    }

}