using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    [GenerateAuthoringComponent]
    public struct VirusParticleData : IComponentData {
        public float2 virusParticleLifetimeRange;
        [HideInInspector] public float remainingLifetime;

    }

}