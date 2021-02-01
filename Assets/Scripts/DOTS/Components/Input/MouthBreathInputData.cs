using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {
    
    public struct MouthBreathInputData : IComponentData, IBreathingMechanismInput {
        public bool Value { get; set; }
    }

}