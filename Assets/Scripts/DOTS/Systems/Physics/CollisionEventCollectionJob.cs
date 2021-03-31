using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Physics {
    
    /**
     * Job to collect all collision events that occur between entities
     */
    [BurstCompile]
    public struct CollisionEventCollectionJob : ICollisionEventsJob {

        public NativeList<StatefulCollisionEvent> statefulCollisionEvents;
        public ComponentDataFromEntity<StatefulCollisionEventBufferTag> collisionEventBuffers;
        
        public void Execute(CollisionEvent collisionEvent) {

            // save new collision events
            var statefulCollisionEvent = new StatefulCollisionEvent(
                collisionEvent.EntityA, collisionEvent.EntityB, 
                collisionEvent.BodyIndexA, collisionEvent.BodyIndexB, 
                collisionEvent.ColliderKeyA, collisionEvent.ColliderKeyB, 
                collisionEvent.Normal);
            
            statefulCollisionEvents.Add(statefulCollisionEvent);

        }
    }
    

}
