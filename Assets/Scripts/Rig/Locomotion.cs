using System;
using com.TUDublin.VRContaminationSimulation.Input;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.Rig {

    public class Locomotion : MonoBehaviour {
        
        [Header("Setup")]
        public Transform HMDCamera;
        
        [Header("Snap Turning")]
        public float turnAngle = 45;
        public float rotationInputCooldown = 0.5f;
        
        [Header("Continuous Movement")]
        public float movementSpeed;

        private float _nextTurn;
        private InputHandler _input;
        private CharacterController _characterController;

        private void Awake() {
            // get components from scene 
            _input = FindObjectOfType<InputHandler>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update() {
            SnapRotation(_input.leftJoystick);
            ContinuousMovement(_input.rightJoystick);
        }
        
        /**
         * Apply snap rotation to XR rig
         */
        private void SnapRotation(Vector2 input) {
            // check if able to rotate
            if (input == Vector2.zero || Time.timeSinceLevelLoad < _nextTurn) return;

            // get direction of rotation
            int direction = ( input.x > 0 ) ? 1 : -1;
            
            // apply snap rotation
            transform.RotateAround(HMDCamera.position, Vector3.up, turnAngle * direction);
            
            // update cooldown
            _nextTurn = Time.timeSinceLevelLoad + rotationInputCooldown;
        }

        /**
         * Apply continuous movement to XR rig 
         */
        private void ContinuousMovement(Vector2 input) {

            // check if able to move
            if (input == Vector2.zero) return;

            // get orientation of movement 
            var orientation = CalculateMovementOrientation(input);

            // calculate movement 
            var movement = Vector3.zero;
            movement += orientation * ( movementSpeed * Vector3.forward );

            // apply movement
            _characterController.Move(movement * Time.deltaTime);

        }

        /**
         * Normalize input to orientation of HMDCamera 
         */
        private Quaternion CalculateMovementOrientation(Vector2 input) {
            // calculate desired direction based on input
            float rotation = math.atan2(input.x, input.y);
            rotation *= Mathf.Rad2Deg;
            
            // add desired direction to Y rotation of HMDCamera
            var orientationEuler = new Vector3(0, HMDCamera.eulerAngles.y + rotation, 0);
            return Quaternion.Euler(orientationEuler);
        }
        
    }

}