using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    public abstract class VirusParticleAuthoringBase<T> : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs 
        where T : struct, IComponentData, IVirusParticleSettings {

        [SerializeField] private GameObject prefab;
        [SerializeField] private float2 particleScaleRange;
        [SerializeField] private float2 initialLinearEmissionForceRange;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new T(){
                    Prefab = conversionSystem.GetPrimaryEntity(prefab),
                    ParticleScale = particleScaleRange,
                    InitialLinearEmissionForce = initialLinearEmissionForceRange
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) => referencedPrefabs.Add(prefab);
    }

}