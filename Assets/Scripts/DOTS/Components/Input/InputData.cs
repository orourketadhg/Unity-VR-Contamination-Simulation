using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {
    
    public struct InputData : IComponentData {
        public int RightGripTouch;
        public int RightGripPress;
        public int RightTriggerTouch;
        public int RightTriggerPress;
        public int RightPrimaryTouch;
        public int RightPrimaryPress;
        public int RightSecondaryTouch;
        public int RightSecondaryPress;
        public int RightJoystickTouch;
        public float2 RightJoystick;
        
        public int LeftGripTouch;
        public int LeftGripPress;
        public int LeftTriggerTouch;
        public int LeftTriggerPress;
        public int LeftPrimaryTouch;
        public int LeftPrimaryPress;
        public int LeftSecondaryTouch;
        public int LeftSecondaryPress;
        public int LeftJoystickTouch;
        public float2 LeftJoystick;
        
    }

}