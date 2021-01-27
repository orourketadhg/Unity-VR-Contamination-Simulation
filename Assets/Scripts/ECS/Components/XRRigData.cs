using com.TUDublin.VRContaminationSimulation.Common.Enums;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    [GenerateAuthoringComponent]
    public struct XRRigData : IComponentData{
        public RigHardwareType Type;
    }
    
}