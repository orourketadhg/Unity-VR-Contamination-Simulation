using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {

    [GenerateAuthoringComponent]
    public struct LocomotionTeleportationInputData : IComponentData {
        public int enableTeleport;
        public int engageTeleport;
    }

}