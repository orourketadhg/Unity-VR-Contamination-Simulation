using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    [GenerateAuthoringComponent]
    public struct InteractableItemData : IComponentData {
        public Entity collector;
        public float3 itemPositionOffset;
        public float3 itemRotationOffset;
        public float mass; 
    }

}