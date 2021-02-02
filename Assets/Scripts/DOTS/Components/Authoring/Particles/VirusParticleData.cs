using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    public struct VirusParticleData : IBufferElementData, IVirusParticleSettings {
        public Entity Prefab { get; set; }
        public float3 ParticleScaleMin { get; set; }
        public float3 ParticleScaleMax { get; set; }
        public float3 InitialEmissionForceMin { get; set; }
        public float3 InitialEmissionForceMax { get; set; }
        public int2 ParticleSpawnCount { get; set; }
    }

}