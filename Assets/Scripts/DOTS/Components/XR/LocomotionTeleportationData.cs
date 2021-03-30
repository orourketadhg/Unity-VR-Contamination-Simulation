using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    /**
     * Component data for XR rig teleportation
     */
    [GenerateAuthoringComponent]
    public struct LocomotionTeleportationData : IComponentData {
        public float distance;
        public Entity indicator;
    }

}