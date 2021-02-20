using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    /**
     * Job to collect all collision events that occur by entities with a VirusParticleData component
     */
    [BurstCompile]
    public struct VirusParticleCollisionEventCollectionJob : ICollisionEventsJob {

        public NativeList<CollisionEventElement> particleCollisionEvents;
        [ReadOnly] public ComponentDataFromEntity<VirusParticleData> particleGroup;

        public void Execute(Unity.Physics.CollisionEvent collisionEvent) {

            
            
        }
    }
    

}
