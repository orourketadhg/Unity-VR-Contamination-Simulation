using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation.Hybrid;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {

    public abstract class SpawnVirusParticlesAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs where T: struct, IComponentData, IVirusParticleSettings {

        public GameObject prefab;
        [Min(0.001f)] public Vector2 particleScaleRange;
        [Min(0.001f)] public Vector2 emissionForceRange;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var spawnSettings = new T {
                Prefab = conversionSystem.GetPrimaryEntity(prefab),
                ParticleScaleRange = particleScaleRange,
                InitialEmissionForceRange = emissionForceRange
            };
            
            dstManager.AddComponentData(entity, spawnSettings);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.Add(prefab);

    }

}