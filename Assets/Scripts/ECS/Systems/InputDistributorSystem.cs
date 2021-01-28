using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [AlwaysSynchronizeSystem]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(InputHandlerSystem))]
    public class InputDistributerSystem : SystemBase {

        private EntityQuery _inputDataQuery;

        protected override void OnCreate() {
            _inputDataQuery = GetEntityQuery(typeof(InputData));
            
        }

        protected override void OnUpdate() {

            if (_inputDataQuery.CalculateEntityCount() == 0) {
                Debug.LogError("Failed to find DOTS Input Handler Entity");
                
            }
            
        }
        
    }

}