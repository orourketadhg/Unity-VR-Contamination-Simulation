using com.TUDublin.VRContaminationSimulation.DOTS.Components.NPC;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Authoring {

    public class NPCLocomotionAuthoring : MonoBehaviour, IConvertGameObjectToEntity {

        [SerializeField] private float mass; 
        [SerializeField] private float movementSpeed;
        [SerializeField] private float stopThreshold;
        
        [SerializeField] private Vector3[] waypoints;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {

            dstManager.AddComponentData(entity, new NPCLocomotionData() {
                velocity = float3.zero,
                mass = mass,
                movementSpeed = movementSpeed,
                stopThreshold = stopThreshold,
                waypointIndex = 0,
            });

            var waypointBuffer = dstManager.AddBuffer<WaypointPositionElement>(entity);
            foreach (var waypoint in waypoints) {
                waypointBuffer.Add(new WaypointPositionElement() {value = waypoint});
            }
            
        }
    }

}