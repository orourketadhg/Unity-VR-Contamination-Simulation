using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Util;
using com.TUDublin.VRContaminationSimulation.Util;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    [UpdateAfter(typeof(VirusParticleSpawnerSystem))]
    public class BrownianMotionRandomWalkSystem : SystemBase {

        protected override void OnUpdate() {
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;

            // Add dependency on VirusParticleSpawnerSystem due to random being used 
            var spawnerDependency = World.GetExistingSystem<VirusParticleSpawnerSystem>().OutDependency;
            Dependency = JobHandle.CombineDependencies(Dependency, spawnerDependency);
            
            Entities
                .WithName("ApplyParticleBrownianMotion")
                .WithBurst()
                .WithAll<VirusParticleData>()
                .ForEach((Entity entity, int entityInQueryIndex, int nativeThreadIndex, ref PhysicsVelocity pv, ref PhysicsMass pm, ref BrownianMotionData motionData, in Translation pos) => {
                    var random = randomArray[0];

                    if (motionData.enabled == 1) {
                        float randomMotionChance = random.NextFloat();

                        if (randomMotionChance < motionData.motionChance) {
                            float randomForce = random.NextFloat(motionData.force.x, motionData.force.y);
                            var randomDirection = MathUtil.PointOnUnitSphere(ref random);
                            pv.ApplyLinearImpulse(pm, randomDirection * randomForce);
                        }
                    }

                    randomArray[0] = random;
                }).Schedule();

        }
        
    }

}