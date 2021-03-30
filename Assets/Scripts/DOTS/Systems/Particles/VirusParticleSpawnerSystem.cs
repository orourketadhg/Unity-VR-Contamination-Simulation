using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Util;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Particles {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class VirusParticleSpawnerSystem: SystemBase {

        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;
        
        private EntityQuery _playerSpawnerQuery;
        private EntityQuery _npcSpawnerQuery;

        public JobHandle OutDependency => Dependency;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            var playerQueryDesc = new EntityQueryDesc() {
                All = new [] {
                    ComponentType.ReadOnly<LocalToWorld>(),
                    ComponentType.ReadOnly<ParticleSpawnerSettingsData>(),
                    typeof(ParticleSpawnerInternalSettingsData),
                    ComponentType.ReadOnly<VirusParticleElement>(),
                    ComponentType.ReadOnly<RespiratoryMechanicInputData>(), 
                }
            };

            var npcQueryDesc = new EntityQueryDesc() {
                All = new [] {
                    ComponentType.ReadOnly<LocalToWorld>(),
                    ComponentType.ReadOnly<ParticleSpawnerSettingsData>(),
                    typeof(ParticleSpawnerInternalSettingsData),
                    ComponentType.ReadOnly<VirusParticleElement>(),
                    ComponentType.ReadOnly<NPCTag>(),
                }
            };
            
            _playerSpawnerQuery = GetEntityQuery(playerQueryDesc);
            _npcSpawnerQuery = GetEntityQuery(npcQueryDesc);
        }
        
        protected override void OnUpdate() {
            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var randomArray = World.GetExistingSystem<RandomSystem>().RandomArray;
            float time = (float) Time.ElapsedTime;

            var spawnerInternalSettingsHandle = GetComponentTypeHandle<ParticleSpawnerInternalSettingsData>();
            var spawnerLocalToWorldHandle = GetComponentTypeHandle<LocalToWorld>(true);
            var spawnerSettingsHandle = GetComponentTypeHandle<ParticleSpawnerSettingsData>(true);
            var spawnerInputHandle = GetComponentTypeHandle<RespiratoryMechanicInputData>(true);
            var virusParticleBufferHandle = GetBufferTypeHandle<VirusParticleElement>(true);

            var playerParticleSpawnJob = new UserVirusParticleSpawnJob() {
                randomArray = randomArray,
                ecb = ecb,
                time = time,
                spawnerLocalToWorldHandle = spawnerLocalToWorldHandle,
                spawnerSettingsHandle = spawnerSettingsHandle,
                spawnerInternalSettingsHandle = spawnerInternalSettingsHandle,
                spawnerInputHandle = spawnerInputHandle,
                virusParticleBufferHandle = virusParticleBufferHandle
            };

            var npcParticleSpawnJob = new NpcVirusParticleSpawnerJob() {
                randomArray = randomArray,
                ecb = ecb,
                time = time,
                spawnerLocalToWorldHandle = spawnerLocalToWorldHandle,
                spawnerSettingsHandle = spawnerSettingsHandle,
                spawnerInternalSettingsHandle = spawnerInternalSettingsHandle,
                virusParticleBufferHandle = virusParticleBufferHandle
            };
            
            var playerParticleSpawnJobHandle = playerParticleSpawnJob.ScheduleParallel(_playerSpawnerQuery, 1, Dependency);
            
            var npcParticleSpawnJobHandle = npcParticleSpawnJob.ScheduleParallel(_npcSpawnerQuery, 1, playerParticleSpawnJobHandle);
            
            Dependency = JobHandle.CombineDependencies(Dependency, playerParticleSpawnJobHandle);
            Dependency = JobHandle.CombineDependencies(Dependency, npcParticleSpawnJobHandle);
            
            _entityCommandBuffer.AddJobHandleForProducer(Dependency);
        }
    }
    
}