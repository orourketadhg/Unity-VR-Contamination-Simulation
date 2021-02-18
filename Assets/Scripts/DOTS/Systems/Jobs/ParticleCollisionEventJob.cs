using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    public struct ParticleCollisionEventJob : ICollisionEventsJob {

        [ReadOnly] public ComponentDataFromEntity<VirusParticleData> particleGroup;

        public void Execute(CollisionEvent collisionEvent) {
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;
            
            Debug.Log(entityA + " Collided With " + entityB);
        }
    }
}
