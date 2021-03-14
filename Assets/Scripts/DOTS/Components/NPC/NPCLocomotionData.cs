using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.NPC {

    public struct NPCLocomotionData : IComponentData {
        public float movementSpeed;
        public float stopThreshold;
    }

}