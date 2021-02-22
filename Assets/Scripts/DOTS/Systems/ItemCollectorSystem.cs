using com.TUDublin.VRContaminationSimulation.DOTS.Components.Items;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    public class ItemCollectorSystem : SystemBase {

        private BuildPhysicsWorld _buildPhysicsWorld;

        protected override void OnCreate() {
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        }

        protected override void OnUpdate() {
            var physicsWorld = _buildPhysicsWorld.PhysicsWorld;

            Entities
                .WithName("ItemPickup")
                .WithoutBurst()
                .WithStructuralChanges()
                .ForEach((Entity entity, ref InteractableCollectorData collector, in LocalToWorld ltw) => {
                    switch (collector.EnableCollector) {
                        // attempt to pickup item
                        case 1 when collector.collectedItem == Entity.Null: {
                            var overlapSpherePosition = ltw.Position + ( -ltw.Right ) * collector.collectorPositionOffset;
                            float overlapSphereRadius = collector.collectorRadius;
                            var overlapSphereHits = new NativeList<DistanceHit>(Allocator.Temp);
                            var overlapSphereFilter = new CollisionFilter() {
                                BelongsTo = ~0u, // belongs to everything
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

                                {
                                    var other = overlapSphereHits[otherIndex].Entity;
                                    collector.collectedItem = other;
                                        
                                    EntityManager.AddComponentData(other, new Parent() {Value = entity});
                                    EntityManager.AddComponentData(other, new LocalToParent());
                                }
                            }
                            break;
                        }
                        case 0 when collector.collectedItem != Entity.Null:
                            {
                                var other = collector.collectedItem;
                                EntityManager.RemoveComponent(other, typeof(Parent));
                                EntityManager.RemoveComponent(other, typeof(LocalToParent));
                            }

                            collector.collectedItem = Entity.Null;
                            break;
                    }
                }).Run();
        }
    }

}