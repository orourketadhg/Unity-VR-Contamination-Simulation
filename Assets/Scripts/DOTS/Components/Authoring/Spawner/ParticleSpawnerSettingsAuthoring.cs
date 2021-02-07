using System;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private List<VirusParticle> particles;
        
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
            foreach (var virusParticle in particles) {
                virusParticleBuffer.Add(new VirusParticleElementData() {
                    prefab = conversionSystem.GetPrimaryEntity(virusParticle.prefab),
                    particleCount = virusParticle.particleCount,
                    particleScale = virusParticle.particleScale,
                    linearEmissionForce = virusParticle.linearEmissionForce
                });
            }
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.AddRange(particles.Select(particle => particle.prefab));

    }

    public struct ParticleSpawnerSettingsData : IComponentData {
        public float2 SpawnerDuration;
        public float SpawnerStartTime;
        public BlobAssetReference<AnimationCurveBlob> SpawnRadiusCurve;
        public bool BreathingMechanicLooping;
        public bool RandomDecayingVirusParticles;
        public bool TotalDecayingVirusParticles;
    }

    [Serializable]
    public class VirusParticle {
        public GameObject prefab;
        public int2 particleCount;
        public float2 particleScale;
        public float2 linearEmissionForce;
    }
    
}