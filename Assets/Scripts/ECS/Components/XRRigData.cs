using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    [GenerateAuthoringComponent]
    public struct XRRigData : IComponentData{
        public TargetType Type;
        
        public enum TargetType {
            Headset,
            RightController,
            LeftController
        }
    }
    
}