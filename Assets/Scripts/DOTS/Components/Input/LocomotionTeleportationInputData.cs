using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {

    /**
     * Store input data about XR Rig Teleportation
     */
    [GenerateAuthoringComponent]
    public struct LocomotionTeleportationInputData : IComponentData {
        public int enableTeleport;
        public int engageTeleport;
    }

}