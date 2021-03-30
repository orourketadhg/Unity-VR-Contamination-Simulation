using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    /**
     * Component data with settings for object pickup
     */
    public struct LocomotionPickupData : IComponentData {
        public int EnableCollector;
        public int CollectorDirection;
        
        public float collectorPositionOffset;
        public float collectorRadius;
        
        public Entity collectedItem;
        public float3 collectedItemPositionOffset;
        
    }

}