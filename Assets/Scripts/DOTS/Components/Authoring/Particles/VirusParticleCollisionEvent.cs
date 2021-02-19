using Unity.Assertions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring.Particles {

    public struct VirusParticleCollisionEvent : IBufferElementData {

        private readonly EntityPair _entityPair;
        private readonly BodyIndexPair _bodyIndices;
        private readonly ColliderKeyPair _colliderKeys;
        
        public Entity EntityA => _entityPair.EntityA;
        public Entity EntityB => _entityPair.EntityB;
        public int BodyIndexA => _bodyIndices.BodyIndexA;
        public int BodyIndexB => _bodyIndices.BodyIndexB;
        public ColliderKey ColliderKeyA => _colliderKeys.ColliderKeyA;
        public ColliderKey ColliderKeyB => _colliderKeys.ColliderKeyB;
        public float3 Normal;

        public VirusParticleCollisionEvent(Entity entityA, Entity entityB, int bodyIndexA, int bodyIndexB, ColliderKey colliderKeyA, ColliderKey colliderKeyB, float3 normal) {
            _entityPair = new EntityPair() {EntityA = entityA, EntityB = entityB};
            _bodyIndices = new BodyIndexPair() {BodyIndexA = bodyIndexA, BodyIndexB = bodyIndexB};
            _colliderKeys = new ColliderKeyPair() {ColliderKeyA = colliderKeyA, ColliderKeyB = colliderKeyB};
            Normal = normal;
        }
        
    }

}