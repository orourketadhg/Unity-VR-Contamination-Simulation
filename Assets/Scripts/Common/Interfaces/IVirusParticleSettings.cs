using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;
using AnimationCurve = Unity.Animation.AnimationCurve;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IVirusParticleSettings {

        Entity Prefab { get; set; }
        public float3 ParticleScaleMin { get; set; }
        public float3 ParticleScaleMax { get; set; }
        public float3 InitialEmissionForceMin { get; set; }
        public float3 InitialEmissionForceMax { get; set; }
        int2 ParticleSpawnCount { get; set; }

    }

}