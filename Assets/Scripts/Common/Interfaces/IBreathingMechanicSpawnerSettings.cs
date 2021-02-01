using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IBreathingMechanicSpawnerSettings {
        float2 SpawnerDuration { get; set; }
        float2 SpawnRange { get; set; }
        float2 SpawnCount { get; set; }
        BlobAssetReference<AnimationCurveBlob> SpawnRangeCurve { get; set; }
        // DynamicBuffer<ParticleData> VirusParticles { get; set; }
        BlobAssetReference<AnimationCurveBlob> ParticleSpawnVolumeCurve { get; set; }
        bool EnableDecayingVirusParticles { get; set; }
        
        
        

    }

}