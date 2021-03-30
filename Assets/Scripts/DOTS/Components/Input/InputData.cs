using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Input {
    
    /**
     * Debug Component for displaying XR Controller input 
     */
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
        public int RightJoystickPress;
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
        public int LeftJoystickPress;
        public float2 LeftJoystick;
        
    }

}