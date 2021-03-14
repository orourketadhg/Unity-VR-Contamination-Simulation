using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Physics {

    [BurstCompile]
    public struct RaycastJob : IJobParallelFor {

        [ReadOnly] public PhysicsWorld world;
        [ReadOnly] public NativeArray<RaycastInput> inputs;
        public NativeArray<RaycastHit> results;
        
        public void Execute(int index) {
            world.CastRay(inputs[index], out var hit);
            results[index] = hit;
        }
    }

}