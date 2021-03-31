using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.XR;
using com.TUDublin.VRContaminationSimulation.Rig;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.XR {
    
    /**
     * System to perform locomotion teleportation - Unfinished
     */
    [UpdateAfter(typeof(FixedStepSimulationSystemGroup))]
    public class LocomotionTeleportationSystem : SystemBase {

        private XRRig _xrRig;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private LocomotionPickupSystem _locomotionPickupSystem;
        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBufferSystem;

        private EntityQuery _locomotionTeleportationQuery;

        protected override void OnCreate() {
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _locomotionPickupSystem = World.GetOrCreateSystem<LocomotionPickupSystem>();
            _entityCommandBufferSystem = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            _locomotionTeleportationQuery = GetEntityQuery(new EntityQueryDesc() {
                All = new ComponentType[] {
                    typeof(Translation),
                    typeof(Rotation),
                    typeof(LocomotionTeleportationInputData),
                    typeof(LocomotionTeleportationData),
                }
            });
            
            // get the XR rig parent
            _xrRig = Object.FindObjectOfType<XRRig>();
        }

        protected override void OnUpdate() {
            
            Dependency = JobHandle.CombineDependencies(Dependency, _locomotionPickupSystem.OutDependency);
            
            if (_locomotionTeleportationQuery.CalculateEntityCount() == 0) {
                return;
            }
            
            var collisionWorld = _buildPhysicsWorld.PhysicsWorld.CollisionWorld;
            var ecb = _entityCommandBufferSystem.CreateCommandBuffer();
            
            Entities
                .WithName("LocomotionTeleportation")
                .WithoutBurst()
                .ForEach((Entity entity, int entityInQueryIndex, int nativeThreadIndex, in Translation translation, in Rotation rotation, in LocomotionTeleportationInputData input, in LocomotionTeleportationData teleportationData) => {
                    if (input.enableTeleport == 1) {

                        var startPos = translation.Value;
                        var endPos = translation.Value + math.forward(rotation.Value) * teleportationData.distance;
                    
                        // construct raycast input 
                        var rayInput = new RaycastInput() {
                            Start = startPos,
                            End = endPos,
                            Filter = new CollisionFilter() {
                                BelongsTo = ~0u,
                                CollidesWith = ~0u,
                                GroupIndex = 0
                            }
                        };
            
                        // debug test for teleport raycast 
                        if (input.engageTeleport == 1) {
                            bool didCast = collisionWorld.CastRay(rayInput, out var hit);

                            if (!didCast) {
                                Debug.Log("Failed to cast ray");
                            }

                            Debug.Log(hit.Entity);
                        }
                    }
                }).Schedule();
            
            // Display debug indicator for teleport
            Entities
                .WithName("LocomotionTeleportIndicator")
                .WithBurst()
                .ForEach((in LocomotionTeleportationData teleportData, in LocomotionTeleportationInputData inputData, in Translation translation, in Rotation rotation) => {

                    var indicator = teleportData.indicator;

                    if (inputData.enableTeleport == 1 && HasComponent<Disabled>(indicator)) {
                        ecb.RemoveComponent<Disabled>(indicator);
                    }
                    else if (inputData.enableTeleport == 0 && !HasComponent<Disabled>(indicator)) {
                        ecb.AddComponent<Disabled>(indicator);
                    }

                    if (inputData.enableTeleport == 1) {
                        var position = translation.Value + math.forward(rotation.Value) * teleportData.distance;
                        ecb.SetComponent(indicator, new Translation() {Value = position});
                    }
                    
                }).Schedule();

            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

}