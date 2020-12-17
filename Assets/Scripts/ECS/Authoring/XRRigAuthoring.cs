using com.TUDublin.VRContaminationSimulation.RigConversion;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {

    public class XRRigAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        public XRRigLink xrRigLink;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            
            xrRigLink.TargetEntity = entity;

        }
    }

}
