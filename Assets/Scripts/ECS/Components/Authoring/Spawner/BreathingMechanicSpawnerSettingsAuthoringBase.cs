using System;
using System.Collections.Generic;
using System.Linq;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Particles;
using Unity.Animation.Hybrid;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Spawner {

    public abstract class BreathingMechanicSpawnerSettingsAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs where T : struct, IComponentData, IBreathingMechanicSpawnerSettings {

        [SerializeField] private float2 spawnerDuration;
        [SerializeField] private float2 spawnRange;
        [SerializeField] private float2 spawnCount;
        [SerializeField] private AnimationCurve spawnRangeCurve;
        [SerializeField] private AnimationCurve particleSpawnVolumeCurve;
        [SerializeField] private bool enableDecayingVirusParticles;
        public VirusParticleSettings[] virusParticles;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            
            var spawnerSettings = new T {
                SpawnerDuration = spawnerDuration,
                SpawnRange = spawnRange,
                SpawnCount = spawnCount,
                SpawnRangeCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(spawnRangeCurve),
                ParticleSpawnVolumeCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(particleSpawnVolumeCurve),
                EnableDecayingVirusParticles = enableDecayingVirusParticles,
            };
            
            var virusParticleBuffer = dstManager.AddBuffer<VirusParticleData>(entity);

            foreach (VirusParticleSettings particle in virusParticles) {
                virusParticleBuffer.Add(new VirusParticleData() {
                    Prefab = conversionSystem.GetPrimaryEntity(particle.prefab),
                    ParticleScaleRange = particle.particleScaleRange,
                    ParticleSpawnCount = particle.particleSpawnCount,
                    InitialEmissionForceRange = particle.initialEmissionForceRange
                });
            }
            
            dstManager.AddComponentData(entity, spawnerSettings);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.AddRange(virusParticles.Select(particle => particle.prefab));
        
    }

    [Serializable]
    public class VirusParticleSettings {
        public GameObject prefab;
        public float2 particleScaleRange;
        public float2 initialEmissionForceRange;
        public int2 particleSpawnCount;

    }

}