using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.Input {

    public class InputHandler : MonoBehaviour {

        private VRControls _input;
        public static InputHandler Instance;

        [Header("Right XR Controller")]
        public bool rightGrip;
        public bool rightTrigger;
        public bool rightPrimaryBtn;
        public bool rightSecondaryBtn;
        public Vector2 rightJoystick;
        
        [Header("Left XR Controller")]
        public bool leftGrip;
        public bool leftTrigger;
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

            // Right Grip
            _input.XRRight.Grip.performed += ctx => rightGrip = true;
            _input.XRRight.Grip.canceled += ctx => rightGrip = false;
            
            // Right Trigger
            _input.XRRight.Trigger.performed += ctx => rightTrigger = true;
            _input.XRRight.Trigger.canceled += ctx => rightTrigger = false;
            
            // Right Primary Button
            _input.XRRight.PrimaryButton.performed += ctx => rightPrimaryBtn = true;
            _input.XRRight.PrimaryButton.canceled += ctx => rightPrimaryBtn = false;
            
            // Right Secondary Button
            _input.XRRight.SecondaryButton.performed += ctx => rightSecondaryBtn = true;
            _input.XRRight.SecondaryButton.canceled += ctx => rightSecondaryBtn = false;
            
            // Right Joystick
            _input.XRRight.Joystick.performed += ctx => rightJoystick = ctx.ReadValue<Vector2>();
            _input.XRRight.Joystick.canceled += ctx => rightJoystick = Vector2.zero;
            
            // Left Grip
            _input.XRLeft.Grip.performed += ctx => leftGrip = true;
            _input.XRLeft.Grip.canceled += ctx => leftGrip = false;
            
            // Left Trigger
            _input.XRLeft.Trigger.performed += ctx => leftTrigger = true;
            _input.XRLeft.Trigger.canceled += ctx => leftTrigger = false;
            
            // Left Primary Button
            _input.XRLeft.PrimaryButton.performed += ctx => leftPrimaryBtn = true;
            _input.XRLeft.PrimaryButton.canceled += ctx => leftPrimaryBtn = false;
            
            // Left Secondary Button
            _input.XRLeft.SecondaryButton.performed += ctx => leftSecondaryBtn = true;
            _input.XRLeft.SecondaryButton.canceled += ctx => leftSecondaryBtn = false;
            
            // Left Joystick
            _input.XRLeft.Joystick.performed += ctx => leftJoystick = ctx.ReadValue<Vector2>();
            _input.XRLeft.Joystick.canceled += ctx => leftJoystick = Vector2.zero;

        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }

    }

}