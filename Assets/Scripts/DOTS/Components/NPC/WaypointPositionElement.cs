using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.NPC {
    
    /**
     * Dynamic Buffer element data about a NPC movement waypoint
     */
    public struct WaypointPositionElement : IBufferElementData {
        public float3 value;
    }

}