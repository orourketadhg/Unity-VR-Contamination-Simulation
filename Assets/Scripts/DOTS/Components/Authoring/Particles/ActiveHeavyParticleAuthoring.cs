using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    [ConverterVersion("TOR", 2)]
    [AddComponentMenu("VR CS/Particles/Active Heavy Particle Data")]
    public class ActiveHeavyParticleAuthoring : VirusParticleAuthoringBase<ActiveHeavyParticleData> {
        
    }
    
    public struct ActiveHeavyParticleData : IComponentData, IVirusParticleSettings {
        public Entity Prefab { get; set; }
        public float2 ParticleScale { get; set; }
        public float2 InitialLinearEmissionForce { get; set; }
    }

}