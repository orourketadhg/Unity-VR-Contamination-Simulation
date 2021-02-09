using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {
    
    [GenerateAuthoringComponent]
    public struct BreathingMechanicInputData : IComponentData {
        public bool Value;
    }

}