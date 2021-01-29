using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Animation;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Authoring {

    [AddComponentMenu("VR CS/Particles/Active Heavy Particle Data")]
    [ConverterVersion("TOR", 1)]
    public class ActiveHeavyParticleAuthoring : SpawnVirusParticlesAuthoringBase<ActiveHeavyParticleData> {
        
    }
    
    public struct ActiveHeavyParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 Scale { get; set; }
        public float2 InitialEmissionForce { get; set; }

    }

}