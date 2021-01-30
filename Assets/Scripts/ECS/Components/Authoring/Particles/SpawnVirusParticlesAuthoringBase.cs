using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Particles {
    
    public abstract class SpawnVirusParticlesAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs where T: struct, IComponentData, IVirusParticleSettings {

        [SerializeField] private GameObject prefab;
        [SerializeField] private float2 particleScaleRange;
        [SerializeField] private float2 emissionForceRange;
        [SerializeField] private int2 particleSpawnCount;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var spawnSettings = new T {
                Prefab = conversionSystem.GetPrimaryEntity(prefab),
                ParticleScaleRange = particleScaleRange,
                InitialEmissionForceRange = emissionForceRange,
                ParticleSpawnCount = particleSpawnCount
                
            };
            
            dstManager.AddComponentData(entity, spawnSettings);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.Add(prefab);
    }

}