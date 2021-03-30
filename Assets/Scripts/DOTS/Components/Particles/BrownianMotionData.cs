using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {
    
    /**
     * Component Data for brownian motion
     */
    [GenerateAuthoringComponent]
    public struct BrownianMotionData : IComponentData {
        public int enabled;
        public float2 force;
        [Range(0, 1)] public float motionChance;
        
    }

}