using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring {

    public class VirusParticleAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private bool isStickyParticle;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {

            dstManager.AddComponent(entity, typeof(DecayingParticleData));
            dstManager.AddComponent(entity, typeof(VirusParticleData));

            if (isStickyParticle) {
                dstManager.AddComponent(entity, typeof(StickyParticleTag));
            }
            
        }
    }

}