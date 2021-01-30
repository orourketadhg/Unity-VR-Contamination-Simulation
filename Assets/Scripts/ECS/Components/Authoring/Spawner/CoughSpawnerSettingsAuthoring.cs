using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Particles;
using Unity.Animation;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Spawner {
    
    public struct CoughSpawnerSettingsData : IComponentData, IBreathingMechanicSpawnerSettings {
        public float2 SpawnerDuration { get; set; }
        public float2 SpawnRange { get; set; }
        public float2 SpawnCount { get; set; }
        public BlobAssetReference<AnimationCurveBlob> SpawnRangeCurve { get; set; }
        //public DynamicBuffer<ParticleData> VirusParticles { get; set; }
        public BlobAssetReference<AnimationCurveBlob> ParticleSpawnVolumeCurve { get; set; }
        public bool EnableDecayingVirusParticles { get; set; }
    }

    [AddComponentMenu("VR CS/Spawners/Cough Spawner Settings Data")]
    [ConverterVersion("TOR", 1)]
    public class CoughSpawnerSettingsAuthoring : BreathingMechanicSpawnerSettingsAuthoringBase<CoughSpawnerSettingsData> {
        
    }

}