using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    /**
     * Job to collect all collision events that occur between entities
     */
    [BurstCompile]
    public struct CollisionEventCollectionJob : ICollisionEventsJob {

        public NativeList<StatefulCollisionEvent> statefulCollisionEvents;
        public ComponentDataFromEntity<CollisionEventBuffer> collisionEventBuffers;

        [ReadOnly] public NativeHashSet<Entity> entitiesWithCollisionBuffers;
        [ReadOnly] public PhysicsWorld physicsWorld;

        public void Execute(Unity.Physics.CollisionEvent collisionEvent) {

            var statefulCollisionEvent = new StatefulCollisionEvent(
                collisionEvent.EntityA, collisionEvent.EntityB, 
                collisionEvent.BodyIndexA, collisionEvent.BodyIndexB, 
                collisionEvent.ColliderKeyA, collisionEvent.ColliderKeyB, 
                collisionEvent.Normal);

            bool calculateDetails = false;

            if (entitiesWithCollisionBuffers.Contains(collisionEvent.EntityA)) {
                if (collisionEventBuffers[collisionEvent.EntityA].calculateCollisionDetails != 0) {
                    calculateDetails = true;
                }
            }
            
            if (entitiesWithCollisionBuffers.Contains(collisionEvent.EntityB)) {
                if (collisionEventBuffers[collisionEvent.EntityB].calculateCollisionDetails != 0) {
                    calculateDetails = true;
                }
            }

            if (calculateDetails) {
                var details = collisionEvent.CalculateDetails(ref physicsWorld);
                statefulCollisionEvent.collisionDetails = new StatefulCollisionEvent.CollisionDetails(
                    details.EstimatedContactPointPositions.Length,
                    details.EstimatedImpulse,
                    details.AverageContactPointPosition
                );
            }
            
            statefulCollisionEvents.Add(statefulCollisionEvent);

        }
    }
    

}
