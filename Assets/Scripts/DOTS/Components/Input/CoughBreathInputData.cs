using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {
    
    public struct CoughBreathInputData: IComponentData, IBreathingMechanicInput {
        public bool Input;
    }

}