using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    public class ParticleSpawnerSettings : IComponentData, IVirusParticleSpawnerSettings {
        public float2 SpawnerDurationRange { get; set; }
        public float2 SpawnRange { get; set; }
        public BlobAssetReference<AnimationCurveBlob> SpawnerRangeCurve { get; set; }
        public BlobAssetReference<AnimationCurveBlob> ParticleSpawnVolumeCurve { get; set; }
        public bool EnableDecayingVirusParticles { get; set; }

    }

}