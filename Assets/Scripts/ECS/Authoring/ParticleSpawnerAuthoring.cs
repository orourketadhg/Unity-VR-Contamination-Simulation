using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation {

    [AddComponentMenu("VRVTS/Particle Spawner")]
    [ConverterVersion("TOR", 1)]
    public class ParticleSpawnerAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

        public GameObject particlePrefab;

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
            referencedPrefabs.Add(particlePrefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            ParticleSpawnerData spawnerData = new ParticleSpawnerData() {
                Entity = conversionSystem.GetPrimaryEntity(particlePrefab),
            };

            dstManager.AddComponentData(entity, spawnerData);
            
        }
    }

}

