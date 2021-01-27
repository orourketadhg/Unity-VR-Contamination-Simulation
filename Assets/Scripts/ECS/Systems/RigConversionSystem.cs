using com.TUDublin.VRContaminationSimulation.ECS.Components;
using com.TUDublin.VRContaminationSimulation.Rig;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    public class RigConversionSystem : SystemBase {

        private RigType[] _rig;
        
        protected override void OnCreate() {
            _rig = Object.FindObjectsOfType<RigType>();
        }

        protected override void OnUpdate() {

            Entities
                .WithoutBurst()
                .ForEach((ref Translation translation, ref Rotation rotation, in XRRigData rigData) => {
                    foreach (RigType t in _rig) {
                        if (rigData.Type != t.type) {
                            continue;
                        }

                        translation.Value = t.transform.position;
                        rotation.Value = t.transform.rotation;
                    }
                }).Run();
        }
    }
}