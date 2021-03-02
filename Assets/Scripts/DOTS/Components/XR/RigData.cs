using com.TUDublin.VRContaminationSimulation.Common.Enums;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    [GenerateAuthoringComponent]
    public struct RigData : IComponentData{
        public RigHardwareType Type;
    }
    
}