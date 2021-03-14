using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {

    [GenerateAuthoringComponent]
    public struct FaceMaskInput : IComponentData {
        public int isMaskEnabled;
        public int isNoseCoveringEnabled;
        public float inputCooldown;
        public float lastInputTime;
    }

}