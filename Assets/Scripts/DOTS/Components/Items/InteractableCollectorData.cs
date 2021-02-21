using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Items {

    [GenerateAuthoringComponent]
    public struct InteractableCollectorData : IComponentData {
        public int enableCollectorInput;
        public int engageCollectorInput;
        public Entity collectedItem;
    }

}