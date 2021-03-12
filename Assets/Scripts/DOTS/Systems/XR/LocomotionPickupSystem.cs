using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.XR;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Physics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.XR {
    
    public class LocomotionPickupSystem : SystemBase {

        private BuildPhysicsWorld _buildPhysicsWorld;
        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        private StatefulCollisionEventSystem _statefulCollisionEventSystem;

        public JobHandle OutDependency => Dependency;

        protected override void OnCreate() {
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            _statefulCollisionEventSystem = World.GetOrCreateSystem<StatefulCollisionEventSystem>();
        }

        protected override void OnUpdate() {
            var physicsWorld = _buildPhysicsWorld.PhysicsWorld;
            var ecb = _entityCommandBuffer.CreateCommandBuffer();
            
            // handle physics world dependency issue
            Dependency = JobHandle.CombineDependencies(Dependency, _statefulCollisionEventSystem.OutDependency);
            
            Entities
            .WithName("ItemPickup")
            .WithBurst()
            .ForEach((Entity entity, ref LocomotionPickupData collector, in LocalToWorld ltw) => {
                switch (collector.EnableCollector) {
                    // attempt to pickup item
                    case 1 when collector.collectedItem == Entity.Null: {
                        var overlapSpherePosition = ltw.Position + ( -ltw.Right * collector.CollectorDirection ) * collector.collectorPositionOffset;
                        float overlapSphereRadius = collector.collectorRadius;
                        var overlapSphereHits = new NativeList<DistanceHit>(Allocator.Temp);
                        var overlapSphereFilter = new CollisionFilter() {
                            BelongsTo = ~0u, // belongs to nothing
                            CollidesWith = ( 1u << 12 ), // collide with layer 12
                            GroupIndex = 0
                        };

                        if (physicsWorld.OverlapSphere(overlapSpherePosition, overlapSphereRadius, ref overlapSphereHits, overlapSphereFilter)) {
                            // get the index of the closest entity
                            int otherIndex = 0;
                            if (overlapSphereHits.Length > 1) {
                                for (int i = 1; i < overlapSphereHits.Length; i++) {
                                    if (overlapSphereHits[i].Distance > overlapSphereHits[otherIndex].Distance) {
                                        otherIndex = i;
                                    }
                                }
                            }

                            var other = overlapSphereHits[otherIndex].Entity;
                            var otherInteractableData = GetComponent<InteractableItemData>(other);

                            // check if already held
                            if (otherInteractableData.collector != Entity.Null) {
                                break;
                            }

                            // set item and collector
                            otherInteractableData.collector = entity;
                            collector.collectedItem = other;

                            // calculate held item position and rotation
                            var pos = collector.collectedItemPositionOffset + otherInteractableData.itemPositionOffset;
                            pos.x *= collector.CollectorDirection;
                            var rot = quaternion.EulerXYZ(otherInteractableData.itemRotationOffset);

                            // update collision filter
                            var otherCollider = GetComponent<PhysicsCollider>(other);
                            var otherColliderClone = otherCollider.Value.Value.Clone();
                            var otherFilter = new CollisionFilter() {
                                BelongsTo = ( 1u << 12 ), // belongs to layer 12
                                CollidesWith = ~( 1u << 11 ), // collides with not layer 11
                                GroupIndex = 0
                            };

                            unsafe {
                                var clonePtr = (ColliderHeader*) otherColliderClone.GetUnsafePtr();
                                clonePtr->Filter = otherFilter;
                            }

                            // set other as child of collector
                            ecb.SetComponent(other, otherInteractableData);
                            ecb.AddComponent(other, new Parent() {Value = entity});
                            ecb.AddComponent(other, new LocalToParent());
                            ecb.SetComponent(other, new Translation() {Value = pos});
                            ecb.SetComponent(other, new Rotation() {Value = rot});
                            ecb.SetComponent(other, new PhysicsCollider() {Value = otherColliderClone});
                            ecb.RemoveComponent<PhysicsVelocity>(other);
                        }

                        break;
                    }
                    case 0 when collector.collectedItem != Entity.Null:
                        var item = collector.collectedItem;
                        var itemData = GetComponent<InteractableItemData>(item);

                        // check if this collector is holding the item
                        if (itemData.collector != entity) {
                            break;
                        }

                        // release the item and collector 
                        itemData.collector = Entity.Null;
                        collector.collectedItem = Entity.Null;

                        // update collision filter
                        var itemCollider = GetComponent<PhysicsCollider>(item);
                        var itemColliderClone = itemCollider.Value.Value.Clone();
                        var heldFilter = new CollisionFilter() {
                            BelongsTo = ( 1u << 12 ), // belongs to layer 12
                            CollidesWith = ~0u, // collides with everything
                            GroupIndex = 0
                        };

                        unsafe {
                            var clonePtr = (ColliderHeader*) itemColliderClone.GetUnsafePtr();
                            clonePtr->Filter = heldFilter;
                        }

                        // recreate physics mass, velocity, & damping
                        var velocity = new PhysicsVelocity();

                        // set release position 
                        var itemLtw = GetComponent<LocalToWorld>(item);
                        var position = itemLtw.Position;
                        var rotation = itemLtw.Rotation;

                        // set item components
                        ecb.RemoveComponent<Parent>(item);
                        ecb.RemoveComponent<LocalToParent>(item);
                        ecb.SetComponent(item, itemData);
                        ecb.SetComponent(item, new PhysicsCollider() {Value = itemColliderClone});
                        ecb.SetComponent(item, new Translation() {Value = position});
                        ecb.SetComponent(item, new Rotation() {Value = rotation});
                        ecb.AddComponent(item, velocity);
                        break;
                }
            }).Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
        
    }

}