using Unity.Collections;
using Unity.Entities;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Util {

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class RandomSystem : ComponentSystem {

        public NativeArray<Random> RandomArray { get; private set; }

        protected override void OnCreate() {
            var random = new Random[JobsUtility.MaxJobThreadCount];
            var seed = new System.Random();

            for (int i = 0; i < JobsUtility.MaxJobThreadCount; i++) {
                random[i] = new Random((uint) seed.Next());
            }

            RandomArray = new NativeArray<Random>(random, Allocator.Persistent);
        }

        protected override void OnUpdate() {
            
        }

        protected override void OnDestroy() {
            RandomArray.Dispose();
        }
        
    }

}