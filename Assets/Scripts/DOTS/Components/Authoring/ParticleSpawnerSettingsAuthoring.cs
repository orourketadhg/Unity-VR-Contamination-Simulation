using System;
using System.Collections.Generic;
using System.Linq;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using Unity.Animation.Hybrid;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using AnimationCurve = UnityEngine.AnimationCurve;
using Random = UnityEngine.Random;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring {
    
    /**
     * Authoring Component for virus particle spawners
     */
    public class ParticleSpawnerSettingsAuthoring: MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs {

        [Header("Spawner Settings")]
        [SerializeField] private float2 spawnerDurationRange;
        [SerializeField] private float spawnerRadius;
        [SerializeField] private AnimationCurve spawnRangeCurve =AnimationCurve.Constant(0, 1, 1);
        [SerializeField] private bool looping;
        [SerializeField] private bool isSpawnerActive;
        [SerializeField] private float inputCooldown = 0.5f;

        [Header("Particle Decaying")]
        [SerializeField] private bool totalDecayingParticles;
        [SerializeField] private bool randomDecayingParticles;
        [SerializeField][Range(0f, 1f)] private float randomDecayChance;
        
        [SerializeField] private List<VirusParticle> particles;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            
            // add spawner settings to entity
            dstManager.AddComponentData(entity, new ParticleSpawnerSettingsData() {
                spawnerDurationRange = spawnerDurationRange,
                spawnerRadius = spawnerRadius,
                spawnRadiusCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(spawnRangeCurve),
                breathingMechanicLooping = looping ? 1 : 0,
                totalDecayingVirusParticles = totalDecayingParticles ? 1 : 0,
                randomDecayingVirusParticles = randomDecayingParticles ? 1 : 0,
                randomDecayChance = randomDecayChance
            });

            // add spawner internal settings to entity 
            dstManager.AddComponentData(entity, new ParticleSpawnerInternalSettingsData() {
                isSpawnerActive = isSpawnerActive ? 1 : 0,
                spawnerDuration = isSpawnerActive ? Random.Range(spawnerDurationRange.x, spawnerDurationRange.y) : 0,
                spawnerStartTime = 0,
                inputCooldown = inputCooldown,
                timeOfLastInput = 0
                
            });
            
            // add prefabs to Dynamic buffer on entity
            var virusParticleBuffer = dstManager.AddBuffer<VirusParticleElement>(entity);
            foreach (var virusParticle in particles) {
                virusParticleBuffer.Add(new VirusParticleElement() {
                    prefab = conversionSystem.GetPrimaryEntity(virusParticle.prefab),
                    decayTime = virusParticle.decayTime,
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
    
    /**
     * Spawning data about a Virus Particle
     */
    [Serializable]
    public class VirusParticle {
        public GameObject prefab;
        public float2 decayTime;
        public float2 particleScale;
        
        public int2 particleCount;
        public AnimationCurve particleCountCurve = AnimationCurve.Constant(0, 1, 1);
        
        public float2 emissionForce;
        public AnimationCurve emissionForceCurve = AnimationCurve.Constant(0, 1, 1);
    }
    
}