using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Particles {

    public struct VirusParticleData : IBufferElementData, IVirusParticleSettings {
        public Entity Prefab { get; set; }
        public float2 ParticleScaleRange { get; set; }
        public float2 InitialEmissionForceRange { get; set; }
        public int2 ParticleSpawnCount { get; set; }
    }

}