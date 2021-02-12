using com.TUDublin.VRContaminationSimulation.DOTS.Components;
using com.TUDublin.VRContaminationSimulation.Rig;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(BuildPhysicsWorld))]
    public class RigConversionSystem : SystemBase {

        private RigType[] _rig;
        
        protected override void OnCreate() {
            _rig = Object.FindObjectsOfType<RigType>();
        }

        protected override void OnUpdate() {

            Entities
                .WithoutBurst()
                .ForEach((ref Translation translation, ref Rotation rotation, in XRRigData rigData) => {
                    foreach (var node in _rig) {
                        if (rigData.Type != node.type) {
                            continue;
                        }

                        float3 nodePosition = node.transform.position;
                        quaternion nodeRotation = node.transform.rotation;

                        translation = new Translation() {Value = nodePosition};
                        rotation = new Rotation() {Value = nodeRotation};
                    }
                }).Run();
        }
    }
}