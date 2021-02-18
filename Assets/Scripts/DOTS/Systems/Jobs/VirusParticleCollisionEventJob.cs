using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {
    
    /**
     * Job to collect all collision events that occur by entities with a VirusParticleData component
     */
    [BurstCompile]
    public struct VirusParticleCollisionEventJob : ICollisionEventsJob {

        public EntityCommandBuffer ecb;
        
        [ReadOnly] public ComponentDataFromEntity<VirusParticleData> ParticleDataGroup;
        public ComponentDataFromEntity<Translation> TranslationGroup;
        public ComponentDataFromEntity<Rotation> RotationGroup;

        public void Execute(CollisionEvent collisionEvent) {
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;

            bool isAParticle = ParticleDataGroup.HasComponent(entityA);
            bool isBParticle = ParticleDataGroup.HasComponent(entityB);

            if (isAParticle || isBParticle) {
                // create rigid transforms of entities A & B
                var rigidTransformA = new RigidTransform(RotationGroup[entityA].Value, TranslationGroup[entityA].Value);
                var rigidTransformB = new RigidTransform(RotationGroup[entityB].Value, TranslationGroup[entityB].Value);
                
                // create Body Frames of entities A & B
                var bodyA = new BodyFrame(rigidTransformA);
                var bodyB = new BodyFrame(rigidTransformB);
            
                // create hinge joint component between A & B
                var joint = PhysicsJoint.CreateHinge(bodyA, bodyB);

                // add joint to entity with virus particle data 
                ecb.AddComponent(isAParticle ? entityA : entityB, joint);
            }
        }
    }
}
