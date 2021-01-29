using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;
using AnimationCurve = Unity.Animation.AnimationCurve;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IVirusParticleSettings {

        Entity Prefab { get; set; }
        float2 Scale { get; set; }
        float2 InitialEmissionForce { get; set; }
        BlobAssetReference<AnimationCurveBlob> EmissionCurve { get; set; }

    }

}