using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {
    
    [GenerateAuthoringComponent]
    public struct DecayingParticleData : IComponentData {
        public int isDecayingParticle;
        public float lifetime;
    }

}