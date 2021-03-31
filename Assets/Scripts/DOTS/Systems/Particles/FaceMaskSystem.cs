using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Particles {
    
    /**
     * System to control user face masks
     */
    public class FaceMaskSystem : SystemBase {

        private EndSimulationEntityCommandBufferSystem _entityCommandBufferSystem;

        protected override void OnCreate() {
            _entityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate() {

            var ecb = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
            float time = (float) Time.ElapsedTime;

            Entities
                .WithName("FaceMaskController")
                .WithBurst()
                .ForEach((Entity entity, int entityInQueryIndex, ref FaceMaskData maskData, in FaceMaskInput maskInput) => {
                    
                    // get nose and mask entities
                    var mask = maskData.faceMask;
                    var nose = maskData.maskNose;

                    // check mask and nose input + input cooldown
                    if (maskInput.enableMask == 1 && time >= maskData.lastInputTime + maskData.inputCooldown) {
                        maskData.isMaskEnabled = ( maskData.isMaskEnabled == 0 ) ? 1 : 0;
                        maskData.lastInputTime = time;
                    }
                    else if (maskInput.enableNose == 1 && time >= maskData.lastInputTime + maskData.inputCooldown) {
                        maskData.isNoseEnabled = ( maskData.isNoseEnabled == 0 ) ? 1 : 0;
                        maskData.lastInputTime = time;
                    }

                    // enable/disable mask on input change
                    if (HasComponent<Disabled>(mask) && maskData.isMaskEnabled == 1) {
                        ecb.RemoveComponent<Disabled>(entityInQueryIndex, mask);
                    }
                    else if (!HasComponent<Disabled>(mask) && maskData.isMaskEnabled == 0) {
                        ecb.AddComponent<Disabled>(entityInQueryIndex, mask);
                    }
                    
                    // enable/disable nose covering on input change
                    if (HasComponent<Disabled>(nose) && maskData.isNoseEnabled == 1) {
                        ecb.RemoveComponent<Disabled>(entityInQueryIndex, nose);
                    }
                    else if (!HasComponent<Disabled>(nose) && maskData.isNoseEnabled == 0) {
                        ecb.AddComponent<Disabled>(entityInQueryIndex, nose);
                    }
                    
                }).ScheduleParallel();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
            
        }
        
    }

}