using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    [GenerateAuthoringComponent]
    public struct ParticleData : IComponentData {
        public Entity Entity;
    }

}