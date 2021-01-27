using UnityEngine;
using UnityEngine.InputSystem;

namespace com.TUDublin.VRContaminationSimulation.Input {

    public class InputHandler : MonoBehaviour {

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

        private void Awake() {

            Singleton();

            _input = new VRControls();

            #region Right XR Controller

            // Right Grip
            _input.XRRight.GripTouch.performed += ctx => rightGripTouch = true;
            _input.XRRight.GripTouch.canceled += ctx => rightGripTouch = !(ctx.ReadValue<float>() < 0.05f);
            _input.XRRight.GripPress.performed += ctx => rightGripPress = true;
            _input.XRRight.GripPress.canceled += ctx => rightGripPress = false;
            
            // Right Trigger Touch
            _input.XRRight.TriggerTouch.performed += ctx => rightTriggerTouch = true;
            _input.XRRight.TriggerTouch.canceled += ctx => rightTriggerTouch = false;
            _input.XRRight.TriggerPress.performed += ctx => rightTriggerPress = true;
            _input.XRRight.TriggerPress.canceled += ctx => rightTriggerPress = false;
            
            // Right Primary Button
            _input.XRRight.PrimaryTouch.performed += ctx => rightPrimaryTouch = true;
            _input.XRRight.PrimaryTouch.canceled += ctx => rightPrimaryTouch = false;
            _input.XRRight.PrimaryPress.performed += ctx => rightPrimaryPress = true;
            _input.XRRight.PrimaryPress.canceled += ctx => rightPrimaryPress = false;
            
            // Right Secondary Button
            _input.XRRight.SecondaryTouch.performed += ctx => rightSecondaryTouch = true;
            _input.XRRight.SecondaryTouch.canceled += ctx => rightSecondaryTouch = false;
            _input.XRRight.SecondaryPress.performed += ctx => rightSecondaryPress = true;
            _input.XRRight.SecondaryPress.canceled += ctx => rightSecondaryPress = false;
            
            // Right Joystick
            _input.XRRight.JoystickTouch.performed += ctx => rightJoystickTouch = true; 
            _input.XRRight.JoystickTouch.canceled += ctx => rightJoystickTouch = false;
            _input.XRRight.Joystick.performed += ctx => rightJoystick = ctx.ReadValue<Vector2>();
            _input.XRRight.Joystick.canceled += ctx => rightJoystick = Vector2.zero;

            #endregion

            #region Left XR Controller

            // Left Grip
            _input.XRLeft.GripTouch.performed += ctx => leftGripTouch = true;
            _input.XRLeft.GripTouch.canceled += ctx => leftGripTouch = !(ctx.ReadValue<float>() < 0.05f);
            _input.XRLeft.GripPress.performed += ctx => leftGripPress = true;
            _input.XRLeft.GripPress.canceled += ctx => leftGripPress = false;
            
            // Left Trigger Touch
            _input.XRLeft.TriggerTouch.performed += ctx => leftTriggerTouch = true;
            _input.XRLeft.TriggerTouch.canceled += ctx => leftTriggerTouch = false;
            _input.XRLeft.TriggerPress.performed += ctx => leftTriggerPress = true;
            _input.XRLeft.TriggerPress.canceled += ctx => leftTriggerPress = false;
            
            // Left Primary Button
            _input.XRLeft.PrimaryTouch.performed += ctx => leftPrimaryTouch = true;
            _input.XRLeft.PrimaryTouch.canceled += ctx => leftPrimaryTouch = false;
            _input.XRLeft.PrimaryPress.performed += ctx => leftPrimaryPress = true;
            _input.XRLeft.PrimaryPress.canceled += ctx => leftPrimaryPress = false;
            
            // Left Secondary Button
            _input.XRLeft.SecondaryTouch.performed += ctx => leftSecondaryTouch = true;
            _input.XRLeft.SecondaryTouch.canceled += ctx => leftSecondaryTouch = false;
            _input.XRLeft.SecondaryPress.performed += ctx => leftSecondaryPress = true;
            _input.XRLeft.SecondaryPress.canceled += ctx => leftSecondaryPress = false;
            
            // Left Joystick
            _input.XRLeft.JoystickTouch.performed += ctx => leftJoystickTouch = true; 
            _input.XRLeft.JoystickTouch.canceled += ctx => leftJoystickTouch = false;
            _input.XRLeft.Joystick.performed += ctx => leftJoystick = ctx.ReadValue<Vector2>();
            _input.XRLeft.Joystick.canceled += ctx => leftJoystick = Vector2.zero;
            
            #endregion
        }

        private void OnEnable() => _input.Enable();
        private void OnDisable() => _input.Disable();

        private void Singleton() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
    }

}