using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    [GenerateAuthoringComponent]
    public struct LocomotionTeleportationData : IComponentData {
        public float distance;
        public Entity indicator;
    }

}