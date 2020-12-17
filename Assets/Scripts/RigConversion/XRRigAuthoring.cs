using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.RigConversion {

    public class EntityRigLink : MonoBehaviour, IConvertGameObjectToEntity {

        public XRRigLink xrRigLink;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            xrRigLink.TargetEntity = entity;
            
        }
    }

}
