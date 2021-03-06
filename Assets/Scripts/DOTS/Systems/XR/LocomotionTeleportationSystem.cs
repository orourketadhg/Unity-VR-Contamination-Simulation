using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.XR;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs;
using com.TUDublin.VRContaminationSimulation.Rig;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.XR {
    
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
            
            var collisionWorld = _buildPhysicsWorld.PhysicsWorld;
            var ecb = _entityCommandBufferSystem.CreateCommandBuffer();

            var translationHandle = GetComponentTypeHandle<Translation>();
            var rotationHandle = GetComponentTypeHandle<Rotation>();
            var inputHandle = GetComponentTypeHandle<LocomotionTeleportationInputData>();
            var teleportHandle = GetComponentTypeHandle<LocomotionTeleportationData>();
            
            var raycastInputs = new NativeList<RaycastInput>(Allocator.TempJob);
            var raycastsHits = new NativeArray<RaycastHit>(raycastInputs.Length, Allocator.TempJob);

            // Get raycastInputs from entities 
            var raycastInputCreationJob = new ConstructLocomotionTeleportationRaycastInputsJob() {
                translationHandle = translationHandle,
                rotationHandle = rotationHandle,
                inputHandle = inputHandle,
                teleportHandle = teleportHandle,
                raycastInputs = raycastInputs
            };
            
            // perform raycasts based on raycastInputs
            var locomotionTeleportRaycastJob = new RaycastJob() {
                world = collisionWorld,
                inputs = raycastInputs,
                results = raycastsHits
            };
            
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
            
            raycastInputCreationJob.Schedule(_locomotionTeleportationQuery, Dependency).Complete();
            locomotionTeleportRaycastJob.Schedule(raycastInputs.Length, 4, Dependency).Complete();
            
            foreach (var t in raycastsHits) {
                Debug.Log(t.Position);
            }

            raycastsHits.Dispose();
            raycastInputs.Dispose();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

}