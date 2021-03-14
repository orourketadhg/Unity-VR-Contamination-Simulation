using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.NPC {
    
    public struct WaypointPositionElement : IBufferElementData {
        public float3 value;
    }

}