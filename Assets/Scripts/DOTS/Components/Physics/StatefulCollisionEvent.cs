using System;
using com.TUDublin.VRContaminationSimulation.Common.Enums;
using Unity.Assertions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics {

    /**
     * Dynamic Buffer Element containing details about a collision event with another entity
     */
    public struct StatefulCollisionEvent : IBufferElementData, IComparable<StatefulCollisionEvent> {

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
        public CollisionEventState CollisionState;

        public StatefulCollisionEvent(Entity entityA, Entity entityB, int bodyIndexA, int bodyIndexB, ColliderKey colliderKeyA, ColliderKey colliderKeyB, float3 normal) : this() {
            _entityPair = new EntityPair() {EntityA = entityA, EntityB = entityB};
            _bodyIndices = new BodyIndexPair() {BodyIndexA = bodyIndexA, BodyIndexB = bodyIndexB};
            _colliderKeys = new ColliderKeyPair() {ColliderKeyA = colliderKeyA, ColliderKeyB = colliderKeyB};
            Normal = normal;
            CollisionState = default;
        }
        
        /**
         * Get the other entity in the collision event
         */
        public Entity GetOtherCollisionEntity(Entity entity) {
            Assert.IsTrue((entity == EntityA) || (entity == EntityB));
            var indexAndVersion = math.select(new int2(EntityB.Index, EntityB.Version), new int2(EntityA.Index, EntityA.Version), entity == EntityB);
            return new Entity { Index = indexAndVersion[0], Version = indexAndVersion[1] };
        }
        
        /**
         * Compare this collision event to another
         */
        public int CompareTo(StatefulCollisionEvent other) {
            int resultA = EntityA.CompareTo(other.EntityA);
            int resultB = EntityB.CompareTo(other.EntityB);
            
            if (resultA != 0 && resultB != 0) {
                return resultA > resultB ? resultA : resultB;
            }
            
            if (ColliderKeyA.Value != other.ColliderKeyA.Value)
            {
                return ColliderKeyA.Value < other.ColliderKeyA.Value ? -1 : 1;
            }

            if (ColliderKeyB.Value != other.ColliderKeyB.Value)
            {
                return ColliderKeyB.Value < other.ColliderKeyB.Value ? -1 : 1;
            }

            return 0;
        }
        
    }

}