using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {

    public abstract class SpawnVirusParticlesAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs where T: struct, IComponentData, IVirusParticleSpawnSettings {

        public GameObject prefab;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var spawnSettings = new T {
                Prefab = conversionSystem.GetPrimaryEntity(prefab)
            };
            ConfigureSpawnSettings(ref spawnSettings);
            dstManager.AddComponentData(entity, spawnSettings);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.Add(prefab);

        protected virtual void ConfigureSpawnSettings(ref T spawnSettings) { }

    }

}