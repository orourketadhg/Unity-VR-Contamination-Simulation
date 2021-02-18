using com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles;
using com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(EndFramePhysicsSystem))]
    public class ParticleCollisionHandlerSystem : SystemBase {

        private BeginFixedStepSimulationEntityCommandBufferSystem _entityCommandBufferSystem;
        private StepPhysicsWorld _stepPhysicsWorld;
        private BuildPhysicsWorld _buildPhysicsWorld;
        private EntityQuery _particleQuery;
        

        protected override void OnCreate() {
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginFixedStepSimulationEntityCommandBufferSystem>();

        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBufferSystem.CreateCommandBuffer();
            
            var ParicleCollisionJob = new VirusParticleCollisionEventJob() {
                ecb = ecb,
                ParticleDataGroup = GetComponentDataFromEntity<VirusParticleData>(),
                TranslationGroup = GetComponentDataFromEntity<Translation>(),
                RotationGroup = GetComponentDataFromEntity<Rotation>()
            }.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, Dependency);
            
            Dependency = JobHandle.CombineDependencies(Dependency, ParicleCollisionJob);
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

}