using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {
    
    [GenerateAuthoringComponent]
    public struct DecayingLifetimeData : IComponentData {
        public float spawnTime;
        public float lifetime;
    }

}