using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner {
    
    public struct NoseBreathSpawnerSettingsData : IComponentData, IBreathingMechanicSpawnerSettings {
        public float2 SpawnerDuration { get; set; }
        public float SpawnerStartTime { get; set; }
        public BlobAssetReference<AnimationCurveBlob> SpawnRadiusCurve { get; set; }
        public bool BreathingMechanicLooping { get; set; }
        public bool RandomDecayingVirusParticles { get; set; }
        public bool TotalDecayingVirusParticles { get; set; }
    }

    [AddComponentMenu("VR CS/Spawners/Nose Breath Spawner Settings Data")]
    [ConverterVersion("TOR", 1)]
    public class NoseBreathSpawnerSettingsAuthoring : BreathingMechanicSpawnerSettingsAuthoringBase<NoseBreathSpawnerSettingsData> {
        
    }

}