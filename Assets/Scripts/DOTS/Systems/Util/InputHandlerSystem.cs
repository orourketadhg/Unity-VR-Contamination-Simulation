using com.TUDublin.VRContaminationSimulation.DOTS.Components.Input;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Items;
using com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Systems.Util {

    [AlwaysUpdateSystem]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class InputHandlerSystem : SystemBase, VRControls.IXRRightActions, VRControls.IXRLeftActions {
        
        private EntityQuery _inputDataQuery;

        private VRControls _input;
        
        #region Right Controller Variables
        
        private int _rightGripPress;
        private int _rightTriggerTouch;
        private int _rightTriggerPress;
        private int _rightPrimaryTouch;
        private int _rightPrimaryPress;
        private int _rightSecondaryTouch;
        private int _rightSecondaryPress;
        private int _rightJoystickTouch;
        private Vector2 _rightJoystick;

        #endregion

        #region Left Controller Variables
        
        private int _leftGripPress;
        private int _leftTriggerTouch;
        private int _leftTriggerPress;
        private int _leftPrimaryTouch;
        private int _leftPrimaryPress;
        private int _leftSecondaryTouch;
        private int _leftSecondaryPress;
        private int _leftJoystickTouch;
        private Vector2 _leftJoystick;
        
        #endregion
        
        protected override void OnCreate() {
            _inputDataQuery = GetEntityQuery(typeof(InputData));

            _input = new VRControls();
            _input.XRLeft.SetCallbacks(this);
            _input.XRRight.SetCallbacks(this);
        }

        protected override void OnStartRunning() => _input.Enable();
        
        protected override void OnUpdate() {

            // update InputData component on found singleton entity; 
            if (_inputDataQuery.CalculateEntityCount() == 0) {
                var inputManager = EntityManager.CreateEntity(typeof(InputData));
                EntityManager.SetName(inputManager, "Input Manager");
            }
            
            _inputDataQuery.SetSingleton(new InputData() {
                
                // Right Controller Input
                RightGripPress = _rightGripPress,
                RightTriggerTouch = _rightTriggerTouch,
                RightTriggerPress = _rightTriggerPress,
                RightPrimaryTouch = _rightPrimaryTouch,
                RightPrimaryPress = _rightPrimaryPress,
                RightSecondaryTouch = _rightSecondaryTouch,
                RightSecondaryPress = _rightSecondaryPress,
                RightJoystickTouch = _rightJoystickTouch,
                RightJoystick = _rightJoystick,
                
                // left Controller Input
                LeftGripPress = _leftGripPress,
                LeftTriggerTouch = _leftTriggerTouch,
                LeftTriggerPress = _leftTriggerPress,
                LeftPrimaryTouch = _leftPrimaryTouch,
                LeftPrimaryPress = _leftPrimaryPress,
                LeftSecondaryTouch = _leftSecondaryTouch,
                LeftSecondaryPress = _leftSecondaryPress,
                LeftJoystickTouch = _leftJoystickTouch,
                LeftJoystick = _leftJoystick
            });

            Entities
                .WithName("BreathingMechanicInputDistribution")
                .WithoutBurst()
                .ForEach((Entity entity, ref BreathingMechanicInputData inputData) => {
                    if (HasComponent<MouthBreathTag>(entity)) {
                        inputData.Value = _rightPrimaryPress;
                    }
                    else if (HasComponent<CoughTag>(entity)) {
                        inputData.Value = _rightSecondaryPress;
                    }
                    else if (HasComponent<NoseBreathTag>(entity)) {
                        inputData.Value = _leftPrimaryPress;
                    }
                    else if (HasComponent<SneezeTag>(entity)) {
                        inputData.Value = _leftSecondaryPress;
                    }
                }).Run();
            
            Entities
                .WithName("ItemCollectorInputDistribution")
                .WithoutBurst()
                .ForEach((Entity entity, ref InteractableCollectorData collector) => {
                    if (HasComponent<LeftHandTag>(entity)) {
                        collector.EnableCollector = _leftGripPress;
                    } 
                    else if (HasComponent<RightHandTag>(entity)) {
                        collector.EnableCollector = _rightGripPress;
                    }
                }).Run();
        }
        
        protected override void OnStopRunning() => _input.Disable();

        #region Right XR Controller
        
        void VRControls.IXRRightActions.OnGripPress(InputAction.CallbackContext context) => _rightGripPress = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnTriggerTouch(InputAction.CallbackContext context) => _rightTriggerTouch = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnTriggerPress(InputAction.CallbackContext context) => _rightTriggerPress = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnPrimaryTouch(InputAction.CallbackContext context) => _rightPrimaryTouch = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnPrimaryPress(InputAction.CallbackContext context) => _rightPrimaryPress = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnSecondaryTouch(InputAction.CallbackContext context) => _rightSecondaryTouch = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnSecondaryPress(InputAction.CallbackContext context) => _rightSecondaryPress = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnJoystickTouch(InputAction.CallbackContext context) => _rightJoystickTouch = context.performed ? 1 : 0;
        void VRControls.IXRRightActions.OnJoystick(InputAction.CallbackContext context) => _rightJoystick = context.ReadValue<Vector2>();

        #endregion
        
        #region Left XR Controller
        
        void VRControls.IXRLeftActions.OnGripPress(InputAction.CallbackContext context) => _leftGripPress = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnTriggerTouch(InputAction.CallbackContext context) => _leftTriggerTouch = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnTriggerPress(InputAction.CallbackContext context) => _leftTriggerPress = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnPrimaryTouch(InputAction.CallbackContext context) => _leftPrimaryTouch = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnPrimaryPress(InputAction.CallbackContext context) => _leftPrimaryPress = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnSecondaryTouch(InputAction.CallbackContext context) => _leftSecondaryTouch = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnSecondaryPress(InputAction.CallbackContext context) => _leftSecondaryPress = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnJoystickTouch(InputAction.CallbackContext context) => _leftJoystickTouch = context.performed ? 1 : 0;
        void VRControls.IXRLeftActions.OnJoystick(InputAction.CallbackContext context) => _leftJoystick = context.ReadValue<Vector2>();
        
        #endregion
    }

}