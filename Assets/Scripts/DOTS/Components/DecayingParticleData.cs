using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components {
    
    [GenerateAuthoringComponent]
    public struct DecayingParticleData : IComponentData {
        public int isDecayingParticle;
        public float lifetime;
    }

}