using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation;
using Unity.Animation.Hybrid;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using AnimationCurve = UnityEngine.AnimationCurve;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner {

    [ConverterVersion("TOR", 2)]
    [AddComponentMenu("VR CS/Spawners/Particle Spawner Settings Data")]
    public class ParticleSpawnerSettingsAuthoring: MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private float2 spawnerDuration;
        [SerializeField] private AnimationCurve spawnRangeCurve;
        [SerializeField] private bool looping;
        [SerializeField] private bool randomDecayingParticles;
        [SerializeField] private bool totalDecayingParticles;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            
            var spawnerSettings = new ParticleSpawnerSettingsData() {
                SpawnerDuration = spawnerDuration,
                SpawnerStartTime = 0,
                SpawnRadiusCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(spawnRangeCurve),
                BreathingMechanicLooping = looping,
                RandomDecayingVirusParticles = randomDecayingParticles,
                TotalDecayingVirusParticles = totalDecayingParticles,
            };

            dstManager.AddComponentData(entity, spawnerSettings);
        }
    }

    public struct ParticleSpawnerSettingsData : IComponentData, IParticleSpawnerSettings {
        public float2 SpawnerDuration { get; set; }
        public float SpawnerStartTime { get; set; }
        public BlobAssetReference<AnimationCurveBlob> SpawnRadiusCurve { get; set; }
        
        public bool BreathingMechanicLooping { get; set; }
        public bool RandomDecayingVirusParticles { get; set; }
        public bool TotalDecayingVirusParticles { get; set; }
    }
    
}