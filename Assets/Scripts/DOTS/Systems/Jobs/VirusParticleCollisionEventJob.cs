using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    /**
     * Job to collect all collision events that occur by entities with a VirusParticleData component
     */
    [BurstCompile]
    public struct VirusParticleCollisionEventJob : ICollisionEventsJob {

        [ReadOnly] public ComponentDataFromEntity<VirusParticleData> particleGroup;

        public void Execute(CollisionEvent collisionEvent) {

            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;

            bool isAParticle = particleGroup.HasComponent(entityA);
            bool isBParticle = particleGroup.HasComponent(entityB);
            
            if (isAParticle && !isBParticle) {
                var particleCollisionEvent = new VirusParticleCollisionEvent(entityA, entityB, collisionEvent.BodyIndexA, collisionEvent.BodyIndexB, collisionEvent.ColliderKeyA, collisionEvent.ColliderKeyB, collisionEvent.Normal);
            }
            else if (isBParticle && !isAParticle) {
                var particleCollisionEvent = new VirusParticleCollisionEvent(entityB, entityA, collisionEvent.BodyIndexB, collisionEvent.BodyIndexA, collisionEvent.ColliderKeyB, collisionEvent.ColliderKeyA, collisionEvent.Normal);
            }
            
        }
    }
    

}
