using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {

    [GenerateAuthoringComponent]
    public struct FaceMaskInput : IComponentData {
        public int enableMask;
        public int enableNose;
    }

}