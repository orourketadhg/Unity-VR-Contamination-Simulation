using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.TUDublin.VRContaminationSimulation.Input {

    public class InputHandler : MonoBehaviour, VRControls.IXRRightActions, VRControls.IXRLeftActions {

        private VRControls _input;
        public static InputHandler Instance;

        [Header("Right XR Controller")]
        public bool rightGripTouch;
        public bool rightGripPress;
        public bool rightTriggerTouch;
        public bool rightTriggerPress;
        public bool rightPrimaryTouch;
        public bool rightPrimaryPress;
        public bool rightSecondaryTouch;
        public bool rightSecondaryPress;
        public bool rightJoystickTouch;
        public Vector2 rightJoystick;
        
        [Header("Left XR Controller")]
        public bool leftGripTouch;
        public bool leftGripPress;
        public bool leftTriggerTouch;
        public bool leftTriggerPress;
        public bool leftPrimaryTouch;
        public bool leftPrimaryPress;
        public bool leftSecondaryTouch;
        public bool leftSecondaryPress;
        public bool leftJoystickTouch;
        public Vector2 leftJoystick;

        [Header("Additional Input")] 
        public bool mouthBreath;
        public bool noseBreath;
        public bool sneeze;
        public bool cough;

        private void Awake() => _input = new VRControls();
        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();
        private void OnApplicationPause(bool pauseStatus) {
            if (pauseStatus) {
                _input.Disable();
            }
            else {
                _input.Enable();
            }
        }

        #region Right XR Controller

        void VRControls.IXRRightActions.OnGripTouch(InputAction.CallbackContext context) => rightGripTouch = context.performed;
        void VRControls.IXRRightActions.OnGripPress(InputAction.CallbackContext context) => rightGripPress = context.performed;
        void VRControls.IXRRightActions.OnTriggerTouch(InputAction.CallbackContext context) => rightTriggerTouch = context.performed;
        void VRControls.IXRRightActions.OnTriggerPress(InputAction.CallbackContext context) => rightTriggerPress = context.performed;
        void VRControls.IXRRightActions.OnPrimaryTouch(InputAction.CallbackContext context) => rightPrimaryTouch = context.performed;
        void VRControls.IXRRightActions.OnPrimaryPress(InputAction.CallbackContext context) => rightPrimaryPress = context.performed;
        void VRControls.IXRRightActions.OnSecondaryTouch(InputAction.CallbackContext context) => rightSecondaryTouch = context.performed;
        void VRControls.IXRRightActions.OnSecondaryPress(InputAction.CallbackContext context) => rightSecondaryPress = context.performed;
        void VRControls.IXRRightActions.OnJoystickTouch(InputAction.CallbackContext context) => rightJoystickTouch = context.performed;
        void VRControls.IXRRightActions.OnJoystick(InputAction.CallbackContext context) => rightJoystick = context.ReadValue<Vector2>();

        #endregion
        
        #region Left XR Controller

        void VRControls.IXRLeftActions.OnGripTouch(InputAction.CallbackContext context) => leftGripTouch = context.performed;
        void VRControls.IXRLeftActions.OnGripPress(InputAction.CallbackContext context) => leftGripPress = context.performed;
        void VRControls.IXRLeftActions.OnTriggerTouch(InputAction.CallbackContext context) => leftTriggerTouch = context.performed;
        void VRControls.IXRLeftActions.OnTriggerPress(InputAction.CallbackContext context) => leftTriggerPress = context.performed;
        void VRControls.IXRLeftActions.OnPrimaryTouch(InputAction.CallbackContext context) => leftPrimaryTouch = context.performed;
        void VRControls.IXRLeftActions.OnPrimaryPress(InputAction.CallbackContext context) => leftPrimaryPress = context.performed;
        void VRControls.IXRLeftActions.OnSecondaryTouch(InputAction.CallbackContext context) => leftSecondaryTouch = context.performed;
        void VRControls.IXRLeftActions.OnSecondaryPress(InputAction.CallbackContext context) => leftSecondaryPress = context.performed;
        void VRControls.IXRLeftActions.OnJoystickTouch(InputAction.CallbackContext context) => leftJoystickTouch = context.performed;
        void VRControls.IXRLeftActions.OnJoystick(InputAction.CallbackContext context) => leftJoystick = context.ReadValue<Vector2>();

        #endregion

        #region Additional Input Bindings
        
        public void OnMouthBreath(InputAction.CallbackContext context) {
            if (context.performed) mouthBreath = !mouthBreath;
        }

        public void OnNoseBreath(InputAction.CallbackContext context) {
            if (context.performed) noseBreath = !noseBreath;
        }

        public void OnSneeze(InputAction.CallbackContext context) {
            if (context.performed) sneeze = !sneeze;
        }

        public void OnCough(InputAction.CallbackContext context) {
            if (context.performed) cough = !cough;
        }

        #endregion
        
    }

}