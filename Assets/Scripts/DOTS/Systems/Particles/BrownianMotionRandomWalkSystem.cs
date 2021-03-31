using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Util;
using com.TUDublin.VRContaminationSimulation.Util;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Particles {

    /**
     * System to perform Brownian motion via random walk to light virus particle entities
     */
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    [UpdateAfter(typeof(VirusParticleSpawnerSystem))]
    public class BrownianMotionRandomWalkSystem : SystemBase {

        protected override void OnUpdate() {
            // Add dependency on VirusParticleSpawnerSystem due to random being used 
            var spawnerDependency = World.GetExistingSystem<VirusParticleSpawnerSystem>().OutDependency;
            Dependency = JobHandle.CombineDependencies(Dependency, spawnerDependency);
            
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;
            
            Entities
                .WithName("ApplyParticleBrownianMotion")
                .WithBurst()
                .WithAll<VirusParticleData, LightTag>()
                .ForEach((Entity entity, int entityInQueryIndex, int nativeThreadIndex, ref PhysicsVelocity pv, ref PhysicsMass pm, ref BrownianMotionData motionData, in Translation pos) => {
                    // get thread random
                    var random = randomArray[nativeThreadIndex];

                    // only apply motion if enabled
                    if (motionData.enabled == 1) {
                        float randomMotionChance = random.NextFloat();

                        // only apply motion on chance
                        if (randomMotionChance < motionData.motionChance) {
                            // apply force in random direction
                            float randomForce = random.NextFloat(motionData.force.x, motionData.force.y);
                            var randomDirection = MathUtil.PointOnUnitSphere(ref random);
                            pv.ApplyLinearImpulse(pm, randomDirection * randomForce);
                        }
                    }

                    // return thread random
                    randomArray[nativeThreadIndex] = random;
                }).Schedule();

        }
        
    }

}