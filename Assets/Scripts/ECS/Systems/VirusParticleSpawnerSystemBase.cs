using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public abstract class VirusParticleSpawnerSystemBase<T> : SystemBase where T : struct, IComponentData, IVirusParticleSpawnSettings{

        protected override void OnUpdate() {
            throw new System.NotImplementedException();
        }
    }

}