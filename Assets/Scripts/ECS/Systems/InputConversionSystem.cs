using System;
using com.TUDublin.VRContaminationSimulation.Common.Enums;
using com.TUDublin.VRContaminationSimulation.ECS.Components;
using com.TUDublin.VRContaminationSimulation.Input;
using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    public class InputConversionSystem : SystemBase {

        private InputHandler _inputController;

        protected override void OnStartRunning() {
            _inputController = InputHandler.Instance;
            
        }

        protected override void OnUpdate() {
            
            Entities
                .WithoutBurst()
                .ForEach((ref InputData input) => {
                switch(input.type) {
                    case ControllerType.LeftHand:
                        UpdateLeftController(ref input);
                        break;
                    
                    case ControllerType.RightHand:
                        UpdateRightController(ref input);
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                    
                }
            }).Run();

        }

        private void UpdateLeftController(ref InputData input) {
            input.GripTouch = _inputController.leftGripTouch;
            input.GripPress = _inputController.leftGripPress;
            input.TriggerTouch = _inputController.leftTriggerTouch;
            input.TriggerPress = _inputController.leftTriggerPress;
            input.PrimaryTouch = _inputController.leftPrimaryTouch;
            input.PrimaryPress = _inputController.leftPrimaryPress;
            input.SecondaryTouch = _inputController.leftSecondaryTouch;
            input.SecondaryPress = _inputController.leftSecondaryPress;
            input.JoystickTouch = _inputController.leftJoystickTouch;
            input.Joystick = _inputController.leftJoystick;
        }
        private void UpdateRightController(ref InputData input) {
            input.GripTouch = _inputController.rightGripTouch;
            input.GripPress = _inputController.rightGripPress;
            input.TriggerTouch = _inputController.rightTriggerTouch;
            input.TriggerPress = _inputController.rightTriggerPress;
            input.PrimaryTouch = _inputController.rightPrimaryTouch;
            input.PrimaryPress = _inputController.rightPrimaryPress;
            input.SecondaryTouch = _inputController.rightSecondaryTouch;
            input.SecondaryPress = _inputController.rightSecondaryPress;
            input.JoystickTouch = _inputController.rightJoystickTouch;
            input.Joystick = _inputController.rightJoystick;
        } 
        
    }

}