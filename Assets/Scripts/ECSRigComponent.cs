using Unity.Entities;
using UnityEngine;

namespace Com.TUDublin.VRContaminationSimulation {

    public class ECSRigComponent : MonoBehaviour, IConvertGameObjectToEntity {

        public XRRigLink xrRigLink;
    
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            xrRigLink.TargetEntity = entity;
        
        }
    
    }

}
