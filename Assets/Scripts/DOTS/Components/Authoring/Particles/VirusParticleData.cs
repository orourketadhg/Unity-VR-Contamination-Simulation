using Unity.Entities;
using Unity.Mathematics;


namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    [GenerateAuthoringComponent]
    public struct VirusParticleData : IComponentData {
        public Entity particleJoint;
        public float spawnTime;
    }
    
}