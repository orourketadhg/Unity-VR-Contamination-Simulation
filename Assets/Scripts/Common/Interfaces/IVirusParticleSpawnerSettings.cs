using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IVirusParticleSpawnerSettings {
        float2 SpawnerDurationRange { get; set; }
        float2 SpawnRange { get; set; }
        BlobAssetReference<AnimationCurveBlob> SpawnerRangeCurve { get; set; }
        BlobAssetReference<AnimationCurveBlob> ParticleSpawnVolumeCurve { get; set; }
        bool EnableDecayingVirusParticles { get; set; }
        

    }

}