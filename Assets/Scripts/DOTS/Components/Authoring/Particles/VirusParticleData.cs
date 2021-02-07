using Unity.Entities;
using Unity.Mathematics;


namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    [GenerateAuthoringComponent]
    public struct VirusParticleData : IComponentData {
        public float2 particleCount;
        public float2 particleScale;
        public float2 linearEmissionForce;
    }
    
}