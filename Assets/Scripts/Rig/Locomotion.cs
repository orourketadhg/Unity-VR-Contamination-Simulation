using System;
using com.TUDublin.VRContaminationSimulation.Input;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.Rig {

    public class Locomotion : MonoBehaviour {
        
        [Header("Snap Turning")]
        public Transform hmd;
        public float turnAngle = 45;
        public float cooldown = 0.5f;
        
        private float _nextTurn;

        [Header("continuous Movement")] 
        public Transform referencePoint;
        public float movementSpeed;

        private InputHandler _input;
        private CharacterController _characterController;

        private void Awake() {
            _input = FindObjectOfType<InputHandler>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update() {
            SnapRotation(_input.leftJoystick);
            ContinuousMovement(_input.rightJoystick);
        }
        
        private void SnapRotation(Vector2 input) {
            if (input == Vector2.zero || Time.timeSinceLevelLoad < _nextTurn) {
                return;
            }
            
            int direction = ( input.x > 0 ) ? 1 : -1;
            transform.RotateAround(hmd.position, Vector3.up, turnAngle * direction);
            
            _nextTurn = Time.timeSinceLevelLoad + cooldown;
        }

        private void ContinuousMovement(Vector2 input) {

            if (input == Vector2.zero) {
                return;
            }

            var orientation = CalculateMovementOrientation(input);

            var movement = Vector3.zero;
            movement += orientation * ( movementSpeed * Vector3.forward );

            _characterController.Move(movement * Time.deltaTime);

        }

        private Quaternion CalculateMovementOrientation(Vector2 input) {
            float rotation = math.atan2(input.x, input.y);
            rotation *= Mathf.Rad2Deg;
            
            var orientationEuler = new Vector3(0, referencePoint.eulerAngles.y + rotation, 0);
            return Quaternion.Euler(orientationEuler);
        }
        
    }

}