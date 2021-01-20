using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {
    
    [AddComponentMenu("VRCS/Authoring/ParticleData")]
    [ConverterVersion("TOR", 1)]
    public class ParticleAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

        public GameObject particlePrefab;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
            referencedPrefabs.Add(particlePrefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            ParticleData data = new ParticleData() {
                Entity = conversionSystem.GetPrimaryEntity(particlePrefab),
            };

            dstManager.AddComponentData(entity, data);
        }
    }

}

