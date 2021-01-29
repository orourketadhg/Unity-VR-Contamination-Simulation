using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation.Hybrid;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Spawner {

    public abstract class BreathingMechanicSpawnerSettingsAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity where T : struct, IComponentData, IBreathingMechanicSpawnerSettings {

        [SerializeField] private Vector2 spawnerDuration;
        [SerializeField] private Vector2 spawnRange;
        [SerializeField] private AnimationCurve spawnRangeCurve;
        [SerializeField] private AnimationCurve particleSpawnVolumeCurve;
        [SerializeField] private bool enableDecayingVirusParticles;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var spawnerSettings = new T {
                SpawnerDuration = spawnerDuration,
                SpawnRange = spawnRange,
                SpawnRangeCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(spawnRangeCurve),
                ParticleSpawnVolumeCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(particleSpawnVolumeCurve),
                EnableDecayingVirusParticles = enableDecayingVirusParticles
            };

            dstManager.AddComponentData(entity, spawnerSettings);
        }
        
    }

}