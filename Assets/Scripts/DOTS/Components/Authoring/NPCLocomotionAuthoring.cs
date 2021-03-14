using com.TUDublin.VRContaminationSimulation.DOTS.Components.NPC;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring {

    public class NPCLocomotionAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private float movementSpeed;
        [SerializeField] private float stopThreshold;
        [SerializeField] private Vector3[] waypoints;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {

            dstManager.AddComponentData(entity, new NPCLocomotionData() {
                movementSpeed = movementSpeed,
                stopThreshold = stopThreshold
            });

            var waypointBuffer = dstManager.AddBuffer<WaypointPositionElement>(entity);
            foreach (var waypoint in waypoints) {
                waypointBuffer.Add(new WaypointPositionElement() {value = waypoint});
            }
            
        }
    }

}