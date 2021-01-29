using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Input {

    public struct SneezeInputData : IComponentData, IBreathingMechanismInput {
        public bool Value { get; set; }
    }

}