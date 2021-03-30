using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    /**
     * Component data for interactable objects
     */
    [GenerateAuthoringComponent]
    public struct InteractableItemData : IComponentData {
        public Entity collector;
        public float3 itemPositionOffset;
        public float3 itemRotationOffset;
        public float mass; 
    }

}