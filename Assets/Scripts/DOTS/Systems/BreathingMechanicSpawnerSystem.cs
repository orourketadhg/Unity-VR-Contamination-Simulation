using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using com.TUDublin.VRContaminationSimulation.Common.Structs;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
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

            _spawnerQuery = GetEntityQuery(ComponentType.ReadOnly<IBreathingMechanicSpawnerSettings>(),
                ComponentType.ReadOnly<IVirusParticleSettings>());

        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBuffer.CreateCommandBuffer().AsParallelWriter();
            var mouthBreathInput = GetSingleton<MouthBreathInputData>();

            var input = GetSingleton<MouthBreathInputData>();

        }
        
        [BurstCompile]
        private struct GenerateVirusParticleSpawnData<TSpawnerSettings> : IJobChunk {

            // Store generated data for new particles
            [WriteOnly] private NativeQueue<ParticleSpawnData>.ParallelWriter particleSpawnData;
            
            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex) {

                var newParticle = new ParticleSpawnData() {
                    
                };

                particleSpawnData.Enqueue(newParticle);
            }
        }
        
    }

    
    
}