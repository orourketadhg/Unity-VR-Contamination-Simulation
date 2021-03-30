using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {
    
    /**
     * Store input data about a Respiratory Mechanic
     */
    [GenerateAuthoringComponent]
    public struct RespiratoryMechanicInputData : IComponentData {
        public int Value;
    }

}