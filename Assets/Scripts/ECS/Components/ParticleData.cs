using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    [GenerateAuthoringComponent]
    public struct ParticleSpawnerData : IComponentData {
        public Entity Entity;
    }

}