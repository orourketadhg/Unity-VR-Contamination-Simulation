using System;
using System.Collections.Generic;
using System.Linq;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using Unity.Animation.Hybrid;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner {

    public abstract class BreathingMechanicSpawnerSettingsAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs where T : struct, IComponentData, IBreathingMechanicSpawnerSettings {

        [SerializeField] private float2 spawnerDuration;
        [SerializeField] private AnimationCurve spawnRangeCurve;
        [SerializeField] private bool looping;
        [SerializeField] private bool randomDecayingParticles;
        [SerializeField] private bool totalDecayingParticles;
        public VirusParticleSettings[] virusParticles;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            
            var spawnerSettings = new T {
                SpawnerDuration = spawnerDuration,
                SpawnerStartTime = 0,
                SpawnRadiusCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(spawnRangeCurve),
                BreathingMechanicLooping = looping,
                RandomDecayingVirusParticles = randomDecayingParticles,
                TotalDecayingVirusParticles = totalDecayingParticles,
            };
            
            var virusParticleBuffer = dstManager.AddBuffer<VirusParticleData>(entity);

            foreach (VirusParticleSettings particle in virusParticles) {
                virusParticleBuffer.Add(new VirusParticleData() {
                    Prefab = conversionSystem.GetPrimaryEntity(particle.prefab),
                    ParticleScale = particle.particleScaleRange,
                    InitialEmissionForce = particle.initialEmissionForceRange
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

    }

}