using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.Input {

    public class InputHandler : MonoBehaviour {

        private VRControls _input;
        public static InputHandler Instance;

        [Header("Right XR Controller")]
        public bool rightGrip;
        public bool rightTriggerTouch;
        public bool rightTriggerPress;
        public bool rightPrimaryBtn;
        public bool rightSecondaryBtn;
        public Vector2 rightJoystick;
        
        [Header("Left XR Controller")]
        public bool leftGrip;
        public bool leftTriggerTouch;
        public bool leftTriggerPress;
        public bool leftPrimaryBtn;
        public bool leftSecondaryBtn;
        public Vector2 leftJoystick;

        private void Awake() {
            
            // Singleton 
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            _input = new VRControls();

            #region Right XR Controller

            // Right Grip
            _input.XRRight.Grip.performed += ctx => rightGrip = true;
            _input.XRRight.Grip.canceled += ctx => rightGrip = false;
            
            // Right Trigger Touch
            _input.XRRight.TriggerTouch.performed += ctx => rightTriggerTouch = true;
            _input.XRRight.TriggerTouch.canceled += ctx => rightTriggerTouch = false;
            
            // Right Trigger Press
            _input.XRRight.TriggerPress.performed += ctx => rightTriggerPress = true;
            _input.XRRight.TriggerPress.canceled += ctx => rightTriggerPress = false;
            
            // Right Primary Button
            _input.XRRight.PrimaryButton.performed += ctx => rightPrimaryBtn = true;
            _input.XRRight.PrimaryButton.canceled += ctx => rightPrimaryBtn = false;
            
            // Right Secondary Button
            _input.XRRight.SecondaryButton.performed += ctx => rightSecondaryBtn = true;
            _input.XRRight.SecondaryButton.canceled += ctx => rightSecondaryBtn = false;
            
            // Right Joystick
            _input.XRRight.Joystick.performed += ctx => rightJoystick = ctx.ReadValue<Vector2>();
            _input.XRRight.Joystick.canceled += ctx => rightJoystick = Vector2.zero;

            #endregion

            #region Left XR Controller

            // Left Grip
            _input.XRLeft.Grip.performed += ctx => leftGrip = true;
            _input.XRLeft.Grip.canceled += ctx => leftGrip = false;
            
            // Left Trigger Touch
            _input.XRLeft.TriggerTouch.performed += ctx => leftTriggerTouch = true;
            _input.XRLeft.TriggerTouch.canceled += ctx => leftTriggerTouch = false;
            
            // Left Trigger Press
            _input.XRLeft.TriggerPress.performed += ctx => leftTriggerPress = true;
            _input.XRLeft.TriggerPress.canceled += ctx => leftTriggerPress = false;
            
            // Left Primary Button
            _input.XRLeft.PrimaryButton.performed += ctx => leftPrimaryBtn = true;
            _input.XRLeft.PrimaryButton.canceled += ctx => leftPrimaryBtn = false;
            
            // Left Secondary Button
            _input.XRLeft.SecondaryButton.performed += ctx => leftSecondaryBtn = true;
            _input.XRLeft.SecondaryButton.canceled += ctx => leftSecondaryBtn = false;
            
            // Left Joystick
            _input.XRLeft.Joystick.performed += ctx => leftJoystick = ctx.ReadValue<Vector2>();
            _input.XRLeft.Joystick.canceled += ctx => leftJoystick = Vector2.zero;

            #endregion

        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }

    }

}