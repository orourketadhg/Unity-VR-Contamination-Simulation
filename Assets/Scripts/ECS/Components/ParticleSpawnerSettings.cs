using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    public class ParticleSpawnerSettings : IComponentData, IVirusParticleSpawnerSettings {
        public BlobAssetReference<AnimationCurveBlob> AnimationCurve { get; set; }
        public bool EnableDecayingVirusParticles { get; set; }

    }

}