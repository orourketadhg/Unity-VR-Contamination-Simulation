using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics {

    public struct StatefulCollisionEventBuffer : IComponentData {
        public int calculateCollisionDetails;
    }

    public class StatefulCollisionEventBufferAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private bool calculateCollisionDetails;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new StatefulCollisionEventBuffer() {calculateCollisionDetails = calculateCollisionDetails ? 1 : 0});
            dstManager.AddBuffer<StatefulCollisionEvent>(entity);
        }
    }

}