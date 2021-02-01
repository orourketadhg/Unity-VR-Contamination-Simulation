using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using com.TUDublin.VRContaminationSimulation.Common.Structs;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Spawner;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public class BreathingMechanicSpawnerSystem: SystemBase {
        
        private EndFixedStepSimulationEntityCommandBufferSystem _entityCommandBuffer;

        private EntityQuery _spawnerQuery;
        
        protected override void OnCreate() {
            _entityCommandBuffer = World.GetOrCreateSystem<EndFixedStepSimulationEntityCommandBufferSystem>();

            _spawnerQuery = GetEntityQuery(typeof(MouthBreathSpawnerSettingsData));

        }

        protected override void OnUpdate() {
            
            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            
            // create a queue to store new particleSpawnData
            var particleSpawnDataQueue = new NativeQueue<ParticleSpawnData>(Allocator.Temp);
            
            // create the job to generate the spawn data
            var job = new GenerateVirusParticleSpawnData<MouthBreathSpawnerSettingsData>() {
                particleSpawnData = particleSpawnDataQueue.AsParallelWriter()
            };

            // schedule and complete the spawn data generation
            job.ScheduleParallel(_spawnerQuery, 1, Dependency).Complete();

            // check if any data was generated
            if (!particleSpawnDataQueue.IsEmpty()) {

                // convert native queue to native array
                var particleData = particleSpawnDataQueue.ToArray(Allocator.Temp);
                var particleEntities = new NativeQueue<Entity>(Allocator.Temp);
                
            }
        }
        
        [BurstCompile]
        private struct GenerateVirusParticleSpawnData<TSpawnerSettings> : IJobEntityBatch {

            // Store generated data for new particles
            [WriteOnly] public NativeQueue<ParticleSpawnData>.ParallelWriter particleSpawnData;


            public void Execute(ArchetypeChunk batchInChunk, int batchIndex) {
                var newParticle = new ParticleSpawnData() {
                    
                };

                particleSpawnData.Enqueue(newParticle);
            }
        }
        
    }

    
    
}