using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {

    /**
     * Component Data for face mask 
     */
    [GenerateAuthoringComponent]
    public struct FaceMaskData : IComponentData {
        public Entity faceMask;
        public Entity maskNose;
        public int isMaskEnabled;
        public int isNoseEnabled;
        public float inputCooldown;
        public float lastInputTime;
    }

}