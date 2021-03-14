using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {

    [GenerateAuthoringComponent]
    public struct FaceMaskData : IComponentData {
        public Entity faceMask;
        public Entity maskNose;
    }

}