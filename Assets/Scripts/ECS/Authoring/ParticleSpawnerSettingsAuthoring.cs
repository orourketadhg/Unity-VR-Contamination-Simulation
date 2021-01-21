using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {
    
    [AddComponentMenu("VRCS/Authoring/ParticleData")]
    [ConverterVersion("TOR", 1)]
    public class ParticleAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

        public GameObject particlePrefab;
        public float initialEmissionStrength;
        public float particleLifeDuration;
        

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
            referencedPrefabs.Add(particlePrefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {

            dstManager.AddComponentData(entity, new ParticleData() {
                virusParticle = conversionSystem.GetPrimaryEntity(particlePrefab),
                initialEmissionStrength = initialEmissionStrength,
                particleLifeDuration = particleLifeDuration
            });
            
        }
    }

}

