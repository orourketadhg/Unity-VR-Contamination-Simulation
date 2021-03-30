using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {

    /**
     * Store input data about Face Mask equipping
     */
    [GenerateAuthoringComponent]
    public struct FaceMaskInput : IComponentData {
        public int enableMask;
        public int enableNose;
    }

}