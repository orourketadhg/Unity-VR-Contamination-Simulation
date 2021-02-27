using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    public class ParticleCollisionHandlerSystem : SystemBase {

        private EndSimulationEntityCommandBufferSystem _entityCommandBuffer;
        private EntityQuery _particleCollisionQuery;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
            
            // get all entities virusParticleData & statefulCollisionEventBuffer components 
            _particleCollisionQuery = GetEntityQuery(new EntityQueryDesc() {
                All = new ComponentType[] {
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
                .ForEach((Entity entity, int entityInQueryIndex, in ParticleCollectorTag collector, in DynamicBuffer<StatefulCollisionEvent> collisionBuffer) => {

                    if (collisionBuffer.IsEmpty) {
                        return;
                    }
                    
                    // get the first instance of a Enter collisionEvent
                    for (int i = 0; i < collisionBuffer.Length; i++) {
                        
                        var other = collisionBuffer[i].GetOtherCollisionEntity(entity);

                        // check for enter type collision
                        if (collisionBuffer[i].CollisionState == CollisionEventState.Enter) {

                            // check if the collision entity is a particle
                            if (!HasComponent<VirusParticleData>(other)) {
                                continue;
                            }
                            
                            ecb.AddComponent(other, new Parent() {Value = entity});
                            ecb.AddComponent(other, new LocalToParent());
                            ecb.AddComponent(other, new IgnoreDecayTag());
                            ecb.AddComponent(other, new IgnoreWalkTag());
                            ecb.RemoveComponent(other, new ComponentTypes(typeof(PhysicsMass), typeof(PhysicsVelocity), typeof(PhysicsDamping)));
                        }
                    }
                }).Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
    }

}