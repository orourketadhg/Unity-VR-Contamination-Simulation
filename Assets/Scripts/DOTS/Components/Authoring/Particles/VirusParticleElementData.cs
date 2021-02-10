using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    public struct VirusParticleElementData : IBufferElementData {
        public Entity prefab;
        public float2 particleScale;
        public int2 particleCount;
        public BlobAssetReference<AnimationCurveBlob> particleCountCurve;
        public float2 emissionForce;
        public BlobAssetReference<AnimationCurveBlob> emissionForceCurve;

    }

}