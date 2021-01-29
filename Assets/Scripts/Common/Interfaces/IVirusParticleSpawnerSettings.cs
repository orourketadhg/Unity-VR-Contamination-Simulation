using Unity.Animation;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IVirusParticleSpawnerSettings {
        BlobAssetReference<AnimationCurveBlob> AnimationCurve { get; set; }

        bool EnableDecayingVirusParticles { get; set; }

    }

}