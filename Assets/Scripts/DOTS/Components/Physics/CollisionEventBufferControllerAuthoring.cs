using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics {

    public struct CollisionEventBufferController : IComponentData {
        public int calculateCollisionDetails;
    }

    public class CollisionEventBufferControllerAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private bool doCalculateCollisionDetails;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new CollisionEventBufferController() {calculateCollisionDetails = doCalculateCollisionDetails ? 1 : 0});
            dstManager.AddBuffer<CollisionEventElement>(entity);
        }
    }

}