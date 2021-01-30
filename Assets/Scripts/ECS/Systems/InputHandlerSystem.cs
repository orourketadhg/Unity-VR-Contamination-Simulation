﻿using com.TUDublin.VRContaminationSimulation.ECS.Components;
using com.TUDublin.VRContaminationSimulation.ECS.Components.Input;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [AlwaysUpdateSystem]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public class InputHandlerSystem : SystemBase, VRControls.IXRRightActions, VRControls.IXRLeftActions {
        
        private EntityQuery _inputDataQuery;
        private EntityQuery _mouthBreathInputDataQuery;
        private EntityQuery _noseBreathInputDataQuery;
        private EntityQuery _sneezeInputDataQuery;
        private EntityQuery _coughInputDataQuery;

        private VRControls _input;
        
        #region Right Controller Variables

        private bool _rightGripTouch;
        private bool _rightGripPress;
        private bool _rightTriggerTouch;
        private bool _rightTriggerPress;
        private bool _rightPrimaryTouch;
        private bool _rightPrimaryPress;
        private bool _rightSecondaryTouch;
        private bool _rightSecondaryPress;
        private bool _rightJoystickTouch;
        private Vector2 _rightJoystick;

        #endregion

        #region Left Controller Variables
        
        private bool _leftGripTouch;
        private bool _leftGripPress;
        private bool _leftTriggerTouch;
        private bool _leftTriggerPress;
        private bool _leftPrimaryTouch;
        private bool _leftPrimaryPress;
        private bool _leftSecondaryTouch;
        private bool _leftSecondaryPress;
        private bool _leftJoystickTouch;
        private Vector2 _leftJoystick;
        
        #endregion

        #region Additional Input Binding Variables

        private bool _mouthBreath;
        private bool _noseBreath;
        private bool _sneeze;
        private bool _cough;

        #endregion
        
        protected override void OnCreate() {
            _inputDataQuery = GetEntityQuery(typeof(InputData));
            _mouthBreathInputDataQuery = GetEntityQuery(typeof(MouthBreathInputData));
            _noseBreathInputDataQuery = GetEntityQuery(typeof(NoseBreathInputData));
            _sneezeInputDataQuery = GetEntityQuery(typeof(SneezeInputData));
            _coughInputDataQuery = GetEntityQuery(typeof(CoughBreathInput));
            
            _input = new VRControls();
            _input.XRLeft.SetCallbacks(this);
            _input.XRRight.SetCallbacks(this);
        }

        protected override void OnStartRunning() => _input.Enable();
        
        protected override void OnUpdate() {

            // update InputData component on found singleton entity; 
            if (_inputDataQuery.CalculateEntityCount() == 0) {
                EntityManager.CreateEntity(typeof(InputData));
            }
            
            _inputDataQuery.SetSingleton(new InputData() {
                
                // Right Controller Input
                RightGripTouch = _rightGripTouch,
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
                LeftGripTouch = _leftGripTouch,
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

            // update BreathInputData component on found singleton entity; 
            if (_mouthBreathInputDataQuery.CalculateEntityCount() == 0) {
                EntityManager.CreateEntity(typeof(MouthBreathInputData));
            }
            
            _mouthBreathInputDataQuery.SetSingleton(new MouthBreathInputData() {
                Value = _mouthBreath
            });
            
            // update BreathInputData component on found singleton entity; 
            if (_noseBreathInputDataQuery.CalculateEntityCount() == 0) {
                EntityManager.CreateEntity(typeof(NoseBreathInputData));
            }
            
            _noseBreathInputDataQuery.SetSingleton(new NoseBreathInputData() {
                Value = _mouthBreath
            });
            
            // update BreathInputData component on found singleton entity; 
            if (_sneezeInputDataQuery.CalculateEntityCount() == 0) {
                EntityManager.CreateEntity(typeof(SneezeInputData));
            }
            
            _sneezeInputDataQuery.SetSingleton(new SneezeInputData() {
                Value = _mouthBreath
            });
            
            // update BreathInputData component on found singleton entity; 
            if (_coughInputDataQuery.CalculateEntityCount() == 0) {
                EntityManager.CreateEntity(typeof(CoughBreathInput));
            }
            
            _coughInputDataQuery.SetSingleton(new CoughBreathInput() {
                Value = _mouthBreath
            });
            
            
        }
        
        protected override void OnStopRunning() => _input.Disable();

        #region Right XR Controller

        void VRControls.IXRRightActions.OnGripTouch(InputAction.CallbackContext context) => _rightGripTouch = context.performed;
        void VRControls.IXRRightActions.OnGripPress(InputAction.CallbackContext context) => _rightGripPress = context.performed;
        void VRControls.IXRRightActions.OnTriggerTouch(InputAction.CallbackContext context) => _rightTriggerTouch = context.performed;
        void VRControls.IXRRightActions.OnTriggerPress(InputAction.CallbackContext context) => _rightTriggerPress = context.performed;
        void VRControls.IXRRightActions.OnPrimaryTouch(InputAction.CallbackContext context) => _rightPrimaryTouch = context.performed;
        void VRControls.IXRRightActions.OnPrimaryPress(InputAction.CallbackContext context) => _rightPrimaryPress = context.performed;
        void VRControls.IXRRightActions.OnSecondaryTouch(InputAction.CallbackContext context) => _rightSecondaryTouch = context.performed;
        void VRControls.IXRRightActions.OnSecondaryPress(InputAction.CallbackContext context) => _rightSecondaryPress = context.performed;
        void VRControls.IXRRightActions.OnJoystickTouch(InputAction.CallbackContext context) => _rightJoystickTouch = context.performed;
        void VRControls.IXRRightActions.OnJoystick(InputAction.CallbackContext context) => _rightJoystick = context.ReadValue<Vector2>();

        #endregion
        
        #region Left XR Controller
        
        void VRControls.IXRLeftActions.OnGripTouch(InputAction.CallbackContext context) => _leftGripTouch = context.performed;
        void VRControls.IXRLeftActions.OnGripPress(InputAction.CallbackContext context) => _leftGripPress = context.performed;
        void VRControls.IXRLeftActions.OnTriggerTouch(InputAction.CallbackContext context) => _leftTriggerTouch = context.performed;
        void VRControls.IXRLeftActions.OnTriggerPress(InputAction.CallbackContext context) => _leftTriggerPress = context.performed;
        void VRControls.IXRLeftActions.OnPrimaryTouch(InputAction.CallbackContext context) => _leftPrimaryTouch = context.performed;
        void VRControls.IXRLeftActions.OnPrimaryPress(InputAction.CallbackContext context) => _leftPrimaryPress = context.performed;
        void VRControls.IXRLeftActions.OnSecondaryTouch(InputAction.CallbackContext context) => _leftSecondaryTouch = context.performed;
        void VRControls.IXRLeftActions.OnSecondaryPress(InputAction.CallbackContext context) => _leftSecondaryPress = context.performed;
        void VRControls.IXRLeftActions.OnJoystickTouch(InputAction.CallbackContext context) => _leftJoystickTouch = context.performed;
        void VRControls.IXRLeftActions.OnJoystick(InputAction.CallbackContext context) => _leftJoystick = context.ReadValue<Vector2>();
        
        #endregion
        
        #region Additional Input Bindings

        public void OnMouthBreath(InputAction.CallbackContext context) {
            if (context.performed) _mouthBreath = !_mouthBreath;
        }

        public void OnNoseBreath(InputAction.CallbackContext context) {
            if (context.performed) _noseBreath = !_noseBreath;
        }

        public void OnSneeze(InputAction.CallbackContext context) {
            if (context.performed) _sneeze = !_sneeze;
        }

        public void OnCough(InputAction.CallbackContext context) {
            if (context.performed) _cough = !_cough;
        }

        #endregion

    }

}