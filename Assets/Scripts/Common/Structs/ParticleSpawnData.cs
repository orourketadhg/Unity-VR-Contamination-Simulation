using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace com.TUDublin.VRContaminationSimulation.Common.Structs {

    public struct ParticleSpawnData {
        public Entity particle;
        public Translation position;
        public PhysicsVelocity initialVelocity;
    }

}