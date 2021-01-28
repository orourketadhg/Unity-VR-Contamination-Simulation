using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [GenerateAuthoringComponent]
    public class BreathComponent : IComponentData {
        public int IsBreathing;
    }

}