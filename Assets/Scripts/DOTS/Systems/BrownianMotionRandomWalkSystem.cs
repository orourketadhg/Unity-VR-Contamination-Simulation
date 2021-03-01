using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Util;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class BrownianMotionRandomWalkSystem : SystemBase {

        protected override void OnCreate() {

        }

        protected override void OnUpdate() {
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;
            float deltaTime = Time.DeltaTime;

            Entities
                .WithName("ApplyParticleBrownianMotion")
                .WithBurst()
                .WithAll<VirusParticleData, LightTag>()
                .ForEach((Entity entity, int entityInQueryIndex, int nativeThreadIndex, ref PhysicsVelocity pv, in PhysicsMass pm, in BrownianMotionData bm) => {
                    var random = randomArray[nativeThreadIndex];
                    
                    if (bm.enableWalk == 1) {
                        var randomImpulse = random.NextFloat3() * 10;
                        pv.ApplyLinearImpulse(in pm, randomImpulse);
                    }

                    randomArray[nativeThreadIndex] = random;
                }).Schedule();

        }
        
    }

}