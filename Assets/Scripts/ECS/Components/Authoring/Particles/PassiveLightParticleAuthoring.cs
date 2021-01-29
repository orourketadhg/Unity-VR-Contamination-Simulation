using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Particles {

    [AddComponentMenu("VR CS/Particles/Passive Light Particle Data")]
    [ConverterVersion("TOR", 1)]
    public class PassiveLightParticleAuthoring : SpawnVirusParticlesAuthoringBase<PassiveLightParticleData> {
    }
    
    public struct PassiveLightParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 ParticleScaleRange { get; set; }
        public float2 InitialEmissionForceRange { get; set; }

    }

}