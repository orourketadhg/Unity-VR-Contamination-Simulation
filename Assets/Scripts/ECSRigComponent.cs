using Unity.Entities;
using UnityEngine;

public class TransformLinkEntity : MonoBehaviour, IConvertGameObjectToEntity {

    public XRRigLink xrRigLink;
    
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        xrRigLink.TargetEntity = entity;
        
    }
    
}
