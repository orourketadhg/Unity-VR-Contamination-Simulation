using com.TUDublin.VRContaminationSimulation.DOTS.Components.NPC;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.NPC {

    public class NPCLocomotionSystem : SystemBase {

        protected override void OnUpdate() {

            float dt = Time.DeltaTime;
            
            Entities
                .WithName("NPCLocomotion")
                .WithBurst()
                .ForEach((Entity entity, ref Translation position, ref Rotation rotation, ref NPCLocomotionData locomotionData, in DynamicBuffer<WaypointPositionElement> waypoints, in LocalToWorld ltw) => {

                    if (Vector3.SqrMagnitude(waypoints[locomotionData.waypointIndex].value - position.Value) <= locomotionData.stopThreshold) {
                        locomotionData.waypointIndex += 1;
                        locomotionData.waypointIndex %= waypoints.Length;
                    }
                    
                    var dirToTarget = waypoints[locomotionData.waypointIndex].value - position.Value;
                    var desired = math.normalizesafe(dirToTarget) * locomotionData.movementSpeed;
                    var force = desired - locomotionData.velocity;

                    var acceleration = force / locomotionData.mass;
                    locomotionData.velocity += acceleration * dt;
                    position.Value += locomotionData.velocity * dt;
                    
                    // rotation.Value= quaternion.Euler(dirToTarget);
                    rotation.Value = quaternion.LookRotation(position.Value + locomotionData.velocity * 10, ltw.Up);

                }).ScheduleParallel();
            
        }
        
    }

}