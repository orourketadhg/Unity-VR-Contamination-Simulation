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
    
    public class ItemCollectorSystem : SystemBase {

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
            .WithoutBurst()
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
                            ecb.RemoveComponent(other, new ComponentTypes(typeof(PhysicsMass), typeof(PhysicsVelocity), typeof(PhysicsDamping)));
                        }

                        break;
                    }
                    case 0 when collector.collectedItem != Entity.Null:
                        var heldItem = collector.collectedItem;
                        var heldItemInteractableData = GetComponent<InteractableItemData>(heldItem);

                        // check if this collector is holding the item
                        if (heldItemInteractableData.collector != entity) {
                            break;
                        }

                        // release the item and collector 
                        heldItemInteractableData.collector = Entity.Null;
                        collector.collectedItem = Entity.Null;

                        // update collision filter
                        var heldItemCollider = GetComponent<PhysicsCollider>(heldItem);
                        var heldItemColliderClone = heldItemCollider.Value.Value.Clone();
                        var heldFilter = new CollisionFilter() {
                            BelongsTo = ( 1u << 12 ), // belongs to layer 12
                            CollidesWith = 0xffffffff, // collides with everything
                            GroupIndex = 0
                        };

                        unsafe {
                            var clonePtr = (ColliderHeader*) heldItemColliderClone.GetUnsafePtr();
                            clonePtr->Filter = heldFilter;
                        }

                        // recreate physics mass, velocity, & damping
                        var mass = PhysicsMass.CreateDynamic(heldItemCollider.MassProperties, heldItemInteractableData.mass);
                        var velocity = new PhysicsVelocity();
                        var damping = new PhysicsDamping() {
                            Linear = 0.01f,
                            Angular = 0.05f
                        };

                        // set release position 
                        var heldItemLtw = GetComponent<LocalToWorld>(heldItem);
                        var position = heldItemLtw.Position;
                        var rotation = heldItemLtw.Rotation;

                        // set item components
                        ecb.RemoveComponent(heldItem, typeof(Parent));
                        ecb.RemoveComponent(heldItem, typeof(LocalToParent));
                        ecb.SetComponent(heldItem, heldItemInteractableData);
                        ecb.SetComponent(heldItem, new PhysicsCollider() {Value = heldItemColliderClone});
                        ecb.SetComponent(heldItem, new Translation() {Value = position});
                        ecb.SetComponent(heldItem, new Rotation() {Value = rotation});
                        ecb.AddComponent(heldItem, mass);
                        ecb.AddComponent(heldItem, velocity);
                        ecb.AddComponent(heldItem, damping);
                        break;
                }
            }).Schedule();
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
        
    }

}