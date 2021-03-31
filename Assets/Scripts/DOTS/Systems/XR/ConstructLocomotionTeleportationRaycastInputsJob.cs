using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.XR;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.XR {
    
    /**
     * Job to construct Raycast inputs for Locomotion teleportation
     */
    [BurstCompile]
    public struct ConstructLocomotionTeleportationRaycastInputsJob : IJobEntityBatch {

        public ComponentTypeHandle<Translation> translationHandle;
        public ComponentTypeHandle<Rotation> rotationHandle;
        public ComponentTypeHandle<LocomotionTeleportationInputData> inputHandle;
        public ComponentTypeHandle<LocomotionTeleportationData> teleportHandle;

        public NativeList<RaycastInput> raycastInputs;

        public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {

            // get NativeArrays of components from component handlers
            var translations = batchInChunk.GetNativeArray(translationHandle);
            var rotations = batchInChunk.GetNativeArray(rotationHandle);
            var inputs = batchInChunk.GetNativeArray(inputHandle);
            var teleportationData = batchInChunk.GetNativeArray(teleportHandle);

            for (int i = 0; i < batchInChunk.Count; i++) {
            
                var translation = translations[i];
                var rotation = rotations[i];
                var input = inputs[i];
                var teleport = teleportationData[i];

                // create raycast inputs based on user inputs
                if (input.enableTeleport == 1) {
                    
                    var rayInput = new RaycastInput() {
                        Start = translation.Value,
                        End = translation.Value + math.forward(rotation.Value) * teleport.distance,
                        Filter = new CollisionFilter() {
                            BelongsTo = ~0u,
                            CollidesWith = ~0u,
                            GroupIndex = 0
                        }
                    };
            
                    if (input.engageTeleport == 1) {
                        raycastInputs.Add(rayInput);
                    }
                }
            }
            
        }
    }

}