using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Physics {
    
    /**
     * Authoring Component for Stateful Collision events
     */
    public class StatefulCollisionEventBufferAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private bool calculateCollisionDetails;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            dstManager.AddComponentData(entity, new StatefulCollisionEventBufferTag());
            dstManager.AddBuffer<StatefulCollisionEvent>(entity);
        }
    }
    
    /**
     * Tag for entities using stateful collision events
     */
    public struct StatefulCollisionEventBufferTag : IComponentData {
    }

}