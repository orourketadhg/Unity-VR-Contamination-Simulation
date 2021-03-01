using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {

    [GenerateAuthoringComponent]
    public struct BrownianMotionData : IComponentData {
        public int enableWalk;
    }

}