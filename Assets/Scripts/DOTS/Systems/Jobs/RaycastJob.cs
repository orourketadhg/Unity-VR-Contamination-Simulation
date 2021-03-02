using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Jobs {

    [BurstCompile]
    public struct RaycastJob : IJobParallelFor {

        [ReadOnly] public PhysicsWorld world;
        [ReadOnly] public NativeList<RaycastInput> inputs;
        public NativeArray<RaycastHit> results;
        
        public void Execute(int index) {
            bool hasHit = world.CastRay(inputs[index], out var hit);
            if (hasHit) {
                results[index] = hit;
            }
        }
    }

}