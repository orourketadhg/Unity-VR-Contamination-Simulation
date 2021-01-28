using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {
    
    public struct InputData : IComponentData {
        public bool RightGripTouch;
        public bool RightGripPress;
        public bool RightTriggerTouch;
        public bool RightTriggerPress;
        public bool RightPrimaryTouch;
        public bool RightPrimaryPress;
        public bool RightSecondaryTouch;
        public bool RightSecondaryPress;
        public bool RightJoystickTouch;
        public float2 RightJoystick;
        
        public bool LeftGripTouch;
        public bool LeftGripPress;
        public bool LeftTriggerTouch;
        public bool LeftTriggerPress;
        public bool LeftPrimaryTouch;
        public bool LeftPrimaryPress;
        public bool LeftSecondaryTouch;
        public bool LeftSecondaryPress;
        public bool LeftJoystickTouch;
        public float2 LeftJoystick;
        
    }

}