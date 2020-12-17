using System;
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
                switch(input.Hand) {
                    case ControllerHand.Left:
                        input.Grip = _inputController.leftGrip;
                        input.Trigger = _inputController.leftTrigger;
                        input.PrimaryButton = _inputController.leftPrimaryBtn;
                        input.SecondaryButton = _inputController.leftSecondaryBtn;
                        input.Joystick = _inputController.leftJoystick;
                        break;
                    
                    case ControllerHand.Right:
                        input.Grip = _inputController.rightGrip;
                        input.Trigger = _inputController.rightTrigger;
                        input.PrimaryButton = _inputController.rightPrimaryBtn;
                        input.SecondaryButton = _inputController.rightSecondaryBtn;
                        input.Joystick = _inputController.rightJoystick;
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                    
                }
            }).Run();

        }
    }

}