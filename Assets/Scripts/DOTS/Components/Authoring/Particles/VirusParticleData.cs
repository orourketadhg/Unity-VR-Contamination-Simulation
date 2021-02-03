using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    public struct VirusParticleData : IBufferElementData, IVirusParticleSettings {
        public Entity Prefab { get; set; }
        public float2 ParticleScale { get; set; }
        public float2 InitialEmissionForce { get; set; }

    }

}