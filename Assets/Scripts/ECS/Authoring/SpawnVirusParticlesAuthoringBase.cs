using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation.Hybrid;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {

    public abstract class SpawnVirusParticlesAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs where T: struct, IComponentData, IVirusParticleSettings {

        public GameObject prefab;
        [Min(0.001f)] public Vector2 scale;
        [Min(0.001f)] public Vector2 emissionForce;
        public AnimationCurve emissionCurve;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            var spawnSettings = new T {
                Prefab = conversionSystem.GetPrimaryEntity(prefab),
                Scale = scale,
                InitialEmissionForce = emissionForce,
                EmissionCurve = conversionSystem.BlobAssetStore.GetAnimationCurve(emissionCurve)
            };
            
            Configure(ref spawnSettings);
            dstManager.AddComponentData(entity, spawnSettings);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.Add(prefab);

        protected virtual void Configure(ref T spawnSettings) { }
    }

}