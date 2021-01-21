using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    public struct ParticleSpawnerSettingData : IComponentData {
        public Entity virusParticle;

        public float initialEmissionStrength;
        public float particleLifeDuration;
    }

}