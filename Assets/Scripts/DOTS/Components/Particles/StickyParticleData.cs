using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {
    
    public struct StickyParticleData : IComponentData {
        public int hasPosition;
        public float3 position;
        public quaternion rotation;
    }

}