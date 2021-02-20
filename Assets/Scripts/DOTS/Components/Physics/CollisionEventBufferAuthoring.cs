using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics {

    public struct CollisionEventBuffer : IComponentData {
        public int calculateCollisionDetails;
    }

    public class CollisionEventBufferAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private bool doCalculateCollisionDetails;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new CollisionEventBuffer() {calculateCollisionDetails = doCalculateCollisionDetails ? 1 : 0});
            dstManager.AddBuffer<StatefulCollisionEvent>(entity);
        }
    }

}