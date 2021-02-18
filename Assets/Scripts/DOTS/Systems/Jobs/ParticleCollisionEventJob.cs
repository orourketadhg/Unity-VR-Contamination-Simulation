using Unity.Burst;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    [BurstCompile]
    public struct ParticleCollisionEventJob : ICollisionEventsJob {

        public void Execute(CollisionEvent collisionEvent) {
            
        }
    }
}
