using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {
    
    public class LocomotionPickupAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private bool invertCollector;
        [SerializeField] private float collectionOffset;
        [SerializeField] private float collectorRadius;
        [SerializeField] private float3 collectedItemPositionOffset;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new LocomotionPickupData() {
                EnableCollector = 0,
                CollectorDirection = invertCollector ? 1 : -1,
                collectorPositionOffset = collectionOffset,
                collectorRadius = collectorRadius,
                collectedItemPositionOffset = collectedItemPositionOffset,
                collectedItem = Entity.Null
            });
        }
    }
    
    public struct LocomotionPickupData : IComponentData {
        public int EnableCollector;
        public int CollectorDirection;
        
        public float collectorPositionOffset;
        public float collectorRadius;
        
        public Entity collectedItem;
        public float3 collectedItemPositionOffset;
        
    }

}