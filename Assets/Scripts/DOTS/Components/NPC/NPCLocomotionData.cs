using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.NPC {

    /**
     * Component Data about NPC movement 
     */
    public struct NPCLocomotionData : IComponentData {
        public float3 velocity;
        public float mass;
        
        public float movementSpeed;
        public float stopThreshold;
        
        public int waypointIndex;
    }

}