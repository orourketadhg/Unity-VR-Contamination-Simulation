using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {

    [AddComponentMenu("VRCS/Authoring/XRRigData")]
    [ConverterVersion("TOR", 1)]
    public class XRRigAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
                
        }
        
    }

}