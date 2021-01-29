using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    public struct BreathInputData : IComponentData, IBreathingMechanismInput {
        public bool Value { get; set; }
    }

}