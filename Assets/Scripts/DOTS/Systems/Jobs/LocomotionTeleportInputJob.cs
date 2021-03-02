using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.XR;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {

    [BurstCompile]
    public struct LocomotionTeleportInputJob : IJobEntityBatch {

        public ComponentTypeHandle<Translation> translationHandle;
        public ComponentTypeHandle<Rotation> rotationHandle;
        public ComponentTypeHandle<LocomotionTeleportationInputData> inputHandle;
        public ComponentTypeHandle<LocomotionTeleportationData> teleportHandle;

        public NativeList<RaycastInput> raycastInputs;

        public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {

            // var translations = batchInChunk.GetNativeArray(translationHandle);
            // var rotations = batchInChunk.GetNativeArray(rotationHandle);
            // var inputs = batchInChunk.GetNativeArray(inputHandle);
            // var teleportData = batchInChunk.GetNativeArray(teleportHandle);
            //
            // for (int i = 0; i < batchInChunk.Count; i++) {
            //
            //     var translation = translations[i];
            //     var rotation = rotations[i];
            //     var input = inputs[i];
            //     var teleport = teleportData[i];
            //     
            //     if (input.enableTeleport == 1) {
            //
            //         float rayLength = teleport.distance;
            //
            //         var rayInput = new RaycastInput() {
            //             Start = translation.Value,
            //             End = translation.Value + math.forward(rotation.Value) * rayLength,
            //             Filter = new CollisionFilter() {
            //                 BelongsTo = ~0u,
            //                 CollidesWith = ~0u,
            //                 GroupIndex = 0
            //             }
            //         };
            //
            //         if (input.engageTeleport == 1) {
            //             raycastInputs.Add(rayInput);
            //         }
            //     }
            // }
            
        }
    }

}