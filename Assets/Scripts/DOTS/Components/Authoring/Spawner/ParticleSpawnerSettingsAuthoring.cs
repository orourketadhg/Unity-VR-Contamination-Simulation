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

        [SerializeField] private float2 spawnerDurationRange;
        [SerializeField] private float spawnerRadius;
        [SerializeField] private AnimationCurve spawnRangeCurve =AnimationCurve.Constant(0, 1, 1);
        [SerializeField] private bool looping;
        [SerializeField] private bool randomDecayingParticles;
        [SerializeField] private bool totalDecayingParticles;

        [SerializeField] private List<VirusParticle> particles;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            
            // add spawner settings
            dstManager.AddComponentData(entity, new ParticleSpawnerSettingsData() {
                spawnerDurationRange = spawnerDurationRange,
                spawnerRadius = spawnerRadius,
                spawnRadiusCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(spawnRangeCurve),
                breathingMechanicLooping = looping,
                randomDecayingVirusParticles = randomDecayingParticles,
                totalDecayingVirusParticles = totalDecayingParticles,
            });

            dstManager.AddComponentData(entity, new ParticleSpawnerInternalSettingsData());

            var virusParticleBuffer = dstManager.AddBuffer<VirusParticleElementData>(entity);
            
            // add prefabs
            foreach (var virusParticle in particles) {
                virusParticleBuffer.Add(new VirusParticleElementData() {
                    prefab = conversionSystem.GetPrimaryEntity(virusParticle.prefab),
                    particleScale = virusParticle.particleScale,
                    particleCount = virusParticle.particleCount,
                    particleCountCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(virusParticle.particleCountCurve),
                    emissionForce = virusParticle.emissionForce,
                    emissionForceCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(virusParticle.emissionForceCurve)
                });
            }
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.AddRange(particles.Select(particle => particle.prefab));

    }

    public struct ParticleSpawnerSettingsData : IComponentData {
        public float2 spawnerDurationRange;
        public float spawnerRadius;
        public BlobAssetReference<AnimationCurveBlob> spawnRadiusCurve;
        public bool breathingMechanicLooping;
        public bool randomDecayingVirusParticles;
        public bool totalDecayingVirusParticles;
    }

    public struct ParticleSpawnerInternalSettingsData : IComponentData {
        public float spawnerDuration;
        public float spawnerStartTime;
        public bool isSpawnerActive;
    }

    [Serializable]
    public class VirusParticle {
        public GameObject prefab;
        public float2 particleScale;
        public int2 particleCount;
        public AnimationCurve particleCountCurve = AnimationCurve.Constant(0, 1, 1);
        public float2 emissionForce;
        public AnimationCurve emissionForceCurve = AnimationCurve.Constant(0, 1, 1);
    }
    
}