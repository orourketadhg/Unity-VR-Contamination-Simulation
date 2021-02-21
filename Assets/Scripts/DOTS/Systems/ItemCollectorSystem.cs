using com.TUDublin.VRContaminationSimulation.DOTS.Components.Items;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

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
                .ForEach((InteractableCollectorData collector, LocalToWorld ltw) => {

                    if (collector.EnableCollector != 1) {
                        return;
                    }
                    
                    var overlapSpherePosition = ltw.Position + ltw.Right * collector.collectorPositionOffset;
                    float overlapSphereRadius = collector.collectorRadius;
                    var overlapSphereHits = new NativeList<DistanceHit>(Allocator.Temp);
                    var overlapSphereFilter = new CollisionFilter {
                    };
                    
                    if (physicsWorld.OverlapSphere(overlapSpherePosition, overlapSphereRadius, ref overlapSphereHits, overlapSphereFilter)) {
                        Debug.Log(overlapSphereHits.Length);
                    }

                }).Run();
            

        }
    }

}