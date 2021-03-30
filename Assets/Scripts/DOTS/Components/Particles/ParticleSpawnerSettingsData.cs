using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {

    /**
     * Component data storing settings for virus particle spawners
     */
    public struct ParticleSpawnerSettingsData : IComponentData {
        public float2 spawnerDurationRange;
        public float spawnerRadius;
        public BlobAssetReference<AnimationCurveBlob> spawnRadiusCurve;
        public int breathingMechanicLooping;
        public int totalDecayingVirusParticles;
        public int randomDecayingVirusParticles;
        public float randomDecayChance;
    }

}