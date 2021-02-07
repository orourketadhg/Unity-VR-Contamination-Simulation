using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    public struct VirusParticleElementData : IBufferElementData {
        public Entity prefab;
        public int2 particleCount;
        public float2 particleScale;
        public float2 linearEmissionForce;

    }

}