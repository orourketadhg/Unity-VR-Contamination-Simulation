using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring {

    /**
     * Authoring Component for a Virus Particle 
     */
    public class VirusParticleAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private bool isStickyParticle;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {

            // Add DecayingParticleData component to entity
            dstManager.AddComponent(entity, typeof(DecayingParticleData));
            // Add VirusParticleData component to entity
            dstManager.AddComponent(entity, typeof(VirusParticleData));

            if (isStickyParticle) {
                dstManager.AddComponent(entity, typeof(StickyParticleTag));
            }
            
        }
    }

}