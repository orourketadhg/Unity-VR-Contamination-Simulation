﻿using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {
    
    [GenerateAuthoringComponent]
    public struct BrownianMotionData : IComponentData {
        public float2 force;
        [Range(0, 1)] public float motionChance;
        
    }

}