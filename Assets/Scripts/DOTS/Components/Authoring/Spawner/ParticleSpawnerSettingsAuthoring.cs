using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using Unity.Animation;
using Unity.Animation.Hybrid;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using AnimationCurve = UnityEngine.AnimationCurve;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner {

    [ConverterVersion("TOR", 2)]
    [AddComponentMenu("VR CS/Spawners/Particle Spawner Settings Data")]
    public class ParticleSpawnerSettingsAuthoring: MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs {

        [SerializeField] private float2 spawnerDuration;
        [SerializeField] private AnimationCurve spawnRangeCurve;
        [SerializeField] private bool looping;
        [SerializeField] private bool randomDecayingParticles;
        [SerializeField] private bool totalDecayingParticles;

        [SerializeField] private List<GameObject> virusParticles;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            
            // add spawner settings
            dstManager.AddComponentData(entity, new ParticleSpawnerSettingsData() {
                SpawnerDuration = spawnerDuration,
                SpawnerStartTime = 0,
                SpawnRadiusCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(spawnRangeCurve),
                BreathingMechanicLooping = looping,
                RandomDecayingVirusParticles = randomDecayingParticles,
                TotalDecayingVirusParticles = totalDecayingParticles,
            });

            var virusParticleBuffer = dstManager.AddBuffer<VirusParticleElementData>(entity);
            
            // add prefabs
            foreach (var virusParticle in virusParticles) {
                virusParticleBuffer.Add(new VirusParticleElementData() {
                    value = conversionSystem.GetPrimaryEntity(virusParticle),
                });
            }
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.AddRange(virusParticles);
    }

    public struct ParticleSpawnerSettingsData : IComponentData {
        public float2 SpawnerDuration;
        public float SpawnerStartTime;
        public BlobAssetReference<AnimationCurveBlob> SpawnRadiusCurve;
        public bool BreathingMechanicLooping;
        public bool RandomDecayingVirusParticles;
        public bool TotalDecayingVirusParticles;
    }
    
}