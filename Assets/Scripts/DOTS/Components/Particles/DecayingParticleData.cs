using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {
    
    /**
     * Component Data for decaying virus particles
     */
    [GenerateAuthoringComponent]
    public struct DecayingParticleData : IComponentData {
        public int isDecayingParticle;
        public float lifetime;
    }

}