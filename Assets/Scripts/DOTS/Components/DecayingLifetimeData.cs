using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components {
    
    [GenerateAuthoringComponent]
    public struct DecayingLifetimeData : IComponentData {
        public float lifetime;
    }

}