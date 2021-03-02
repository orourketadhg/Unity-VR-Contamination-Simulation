using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.XR {

    [GenerateAuthoringComponent]
    public struct LocomotionTeleportationData : IComponentData {
        public Entity Indicator;
        public float distance;
    }

}