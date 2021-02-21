using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Items {

    [GenerateAuthoringComponent]
    public struct InteractableItemData : IComponentData {
        public float3 itemPositionOffset;
        public float3 itemRotationOffset;
    }

}