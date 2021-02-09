using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial class VirusParticleSpawnerSystem: SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        private EntityQuery _spawnerQuery;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            var queryDesc = new EntityQueryDesc() {
                All = new [] {
                    ComponentType.ReadOnly<LocalToWorld>(),
                    ComponentType.ReadOnly<ParticleSpawnerSettingsData>(),
                    ComponentType.ReadOnly<VirusParticleElementData>(),
                    ComponentType.ReadOnly<BreathingMechanicInputData>(), 
                }
            };
            _spawnerQuery = GetEntityQuery(queryDesc);
        }
        
        protected override void OnUpdate() {
            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;
            float deltaTime = (float) Time.ElapsedTime;

            var spawnerLocalToWorldHandle = GetComponentTypeHandle<LocalToWorld>();
            var spawnerSettingsHandle = GetComponentTypeHandle<ParticleSpawnerSettingsData>();
            var virusParticleBufferHandle = GetBufferTypeHandle<VirusParticleElementData>();
            var spawnerInputHandle = GetComponentTypeHandle<BreathingMechanicInputData>();

            var particleSpawnJobHandle = new VirusParticleSpawnJob() {
                randomArray = randomArray,
                ecb = ecb,
                inputHandle = spawnerInputHandle,
                spawnerLocalToWorldHandle = spawnerLocalToWorldHandle,
                spawnerSettingsHandle = spawnerSettingsHandle,
                virusParticleBufferHandle = virusParticleBufferHandle
            };
            
            var particleSpawnJobHandleDependency = particleSpawnJobHandle.ScheduleParallel(_spawnerQuery, 1, Dependency);
            
            Dependency = JobHandle.CombineDependencies(Dependency, particleSpawnJobHandleDependency);
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
    }
    
}