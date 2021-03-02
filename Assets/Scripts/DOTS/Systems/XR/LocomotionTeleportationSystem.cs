using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.XR;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs;
using com.TUDublin.VRContaminationSimulation.Rig;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.XR {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class LocomotionTeleportationSystem : SystemBase {

        private XRRig _xrRig;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private EndFramePhysicsSystem _endFramePhysicsSystem;
        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBufferSystem;

        private EntityQuery _locomotionTeleportationQuery;

        protected override void OnCreate() {
            _buildPhysicsWorld = World.GetExistingSystem<BuildPhysicsWorld>();
            _endFramePhysicsSystem = World.GetExistingSystem<EndFramePhysicsSystem>();
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

            if (_locomotionTeleportationQuery.CalculateEntityCount() == 0) {
                return;
            }
            
            var collisionWorld = _buildPhysicsWorld.PhysicsWorld.CollisionWorld;
            // var ecb = _entityCommandBufferSystem.CreateCommandBuffer();
            Dependency = JobHandle.CombineDependencies(Dependency, _endFramePhysicsSystem.GetOutputDependency());
            
            var translationHandle = new ComponentTypeHandle<Translation>();
            var rotationHandle = new ComponentTypeHandle<Rotation>();
            var inputHandle = new ComponentTypeHandle<LocomotionTeleportationInputData>();
            var teleportHandle = new ComponentTypeHandle<LocomotionTeleportationData>();
            
            var raycastInputsList = new NativeList<RaycastInput>(Allocator.TempJob);

            // Get raycastInputs from entities 
            var raycastInputCreationJob = new LocomotionTeleportInputJob() {
                translationHandle = translationHandle,
                rotationHandle = rotationHandle,
                inputHandle = inputHandle,
                teleportHandle = teleportHandle,
                raycastInputs = raycastInputsList
            };
            var raycastInputCreationJobHandle = raycastInputCreationJob.Schedule(_locomotionTeleportationQuery, Dependency);
            
            // var raycastInputs = raycastInputsList.AsArray();
            // var raycastsHits = new NativeArray<RaycastHit>();
            //
            // // perform raycasts based on raycastInputs
            // var locomotionTeleportRaycastJob = new RaycastJob() {
            //     world = collisionWorld,
            //     inputs = raycastInputs,
            //     results = raycastsHits
            // };
            // locomotionTeleportRaycastJob.Schedule(raycastInputs.Length, 4, raycastInputCreationJobHandle).Complete();

            raycastInputsList.Dispose();
        }
    }

}