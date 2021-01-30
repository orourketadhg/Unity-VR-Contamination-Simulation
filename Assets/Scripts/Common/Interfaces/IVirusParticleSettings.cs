using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;
using AnimationCurve = Unity.Animation.AnimationCurve;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IVirusParticleSettings {

        Entity Prefab { get; set; }
        float2 ParticleScaleRange { get; set; }
        float2 InitialEmissionForceRange { get; set; }
        int2 ParticleSpawnCount { get; set; }

    }

}