using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [AlwaysSynchronizeSystem]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateAfter(typeof(InputConversionSystem))]
    public class InputGatheringSystem : SystemBase {

        private EntityQuery _inputDataQuery;

        protected override void OnCreate() {
            _inputDataQuery = GetEntityQuery(typeof(InputData));
            
        }

        protected override void OnUpdate() {

            if (_inputDataQuery.CalculateEntityCount() == 0) {
                return;
            }
            
        }
        
    }

}