using com.TUDublin.VRContaminationSimulation.Common.Enums;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    /**
     * Data component specifying XR rig part type
     */
    [GenerateAuthoringComponent]
    public struct RigData : IComponentData{
        public RigHardwareType Type;
    }
    
}