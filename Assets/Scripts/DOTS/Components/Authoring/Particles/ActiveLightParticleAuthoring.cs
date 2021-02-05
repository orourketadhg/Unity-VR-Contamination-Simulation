using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    [ConverterVersion("TOR", 2)]
    [AddComponentMenu("VR CS/Particles/Active Light Particle Data")]
    public class ActiveLightParticleAuthoring : VirusParticleAuthoringBase<ActiveLightParticleData> {
        
    }
    
    public struct ActiveLightParticleData : IComponentData, IVirusParticleSettings {
        public Entity Prefab { get; set; }
        public float2 ParticleScale { get; set; }
        public float2 InitialLinearEmissionForce { get; set; }
    }

}