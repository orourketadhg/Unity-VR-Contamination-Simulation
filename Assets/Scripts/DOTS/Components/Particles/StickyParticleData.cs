using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {
    
    public struct StickyParticleData : IComponentData {
        //public Entity value;

        public int hasPosition;
        public float3 position;
        public quaternion rotation;
    }

}