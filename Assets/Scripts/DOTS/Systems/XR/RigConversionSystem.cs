using com.TUDublin.VRContaminationSimulation.DOTS.Components.XR;
using com.TUDublin.VRContaminationSimulation.Rig;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.XR {

    /**
     * System to perform GameObject to ECS XR rig Link
     */
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class RigConversionSystem : SystemBase {

        private RigType[] _rig;
        
        protected override void OnCreate() {
            // Find GameObject XR rig 
            _rig = Object.FindObjectsOfType<RigType>();
        }

        protected override void OnUpdate() {

            Entities
                .WithName("RigConversion")
                .WithoutBurst()
                .ForEach((ref Translation translation, ref Rotation rotation, ref PhysicsVelocity velocity, in RigData rigData) => {
                    foreach (var node in _rig) {
                        if (rigData.Type != node.type) {
                            continue;
                        }

                        // get position and rotation
                        float3 nodePosition = node.transform.position;
                        quaternion nodeRotation = node.transform.rotation;

                        // apply position and rotation
                        translation = new Translation() {Value = nodePosition};
                        rotation = new Rotation() {Value = nodeRotation};
                        
                        // reset physics velocity on ECS rig 
                        velocity = new PhysicsVelocity();
                    }
                }).Run();
        }
    }
}