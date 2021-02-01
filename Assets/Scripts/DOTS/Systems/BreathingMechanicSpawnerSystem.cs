using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using Unity.Entities;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class BreathingMechanicSpawnerSystem: SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();
            
        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var input = GetSingleton<MouthBreathInputData>();

            // Entities
            //     .ForEach((Entity entity, int entityInQueryIndex, ref MouthBreathSpawnerSettingsData mouthBreathSpawnerSettingsData, in LocalToWorld position) => {
            //         var spawnerVirusParticles = GetBuffer<VirusParticleData>(entity);
            //         
            //         if (input.Value) {
            //             ecb.Instantiate(entityInQueryIndex, spawnerVirusParticles[0].Prefab);
            //         }
            //         
            //     }).ScheduleParallel();
            //
            // _entityCommandBuffer.AddJobHandleForProducer(Dependency);

        }
        
    }

    
    
}