﻿using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components.Authoring.Particles {

    [AddComponentMenu("VR CS/Particles/Active Heavy Particle Data")]
    [ConverterVersion("TOR", 1)]
    public class ActiveHeavyParticleAuthoring : SpawnVirusParticlesAuthoringBase<ActiveHeavyParticleData> {
    }
    
    public struct ActiveHeavyParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 ParticleScaleRange { get; set; }
        public float2 InitialEmissionForceRange { get; set; }
        public int2 ParticleSpawnCount { get; set; }

    }

}