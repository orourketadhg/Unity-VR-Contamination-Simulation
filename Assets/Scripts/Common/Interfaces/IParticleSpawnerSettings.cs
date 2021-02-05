using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IParticleSpawnerSettings {
        float2 SpawnerDuration { get; set; }                                    // total time of breathing mechanic takes (min, max[inclusive]) 
        float SpawnerStartTime { get; set; }                                    // time at which breathing mechanic was enabled 
        BlobAssetReference<AnimationCurveBlob> SpawnRadiusCurve { get; set; }   // a curve to define the max range particles can spawn at, depending on the 
        bool BreathingMechanicLooping { get; set; }                             // breathing mechanic loops when finished
        bool RandomDecayingVirusParticles { get; set; }                         // some virus particles decay overtime
        bool TotalDecayingVirusParticles { get; set; }                          // all virus particles decay overtime
    }

}