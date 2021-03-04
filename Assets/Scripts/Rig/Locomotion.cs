using com.TUDublin.VRContaminationSimulation.Input;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.Rig {

    public class Locomotion : MonoBehaviour {
        public Transform head;
        public float turnCoolDown;

        private InputHandler _input;
        
        private float _nextTurn;

        private void Awake() {
            _input = FindObjectOfType<InputHandler>();
        }

        private void Update() {
            TurnRig(_input.leftJoystick);
            
        }

        private void TurnRig(Vector2 input) {
            
            if (input == Vector2.zero || Time.timeSinceLevelLoad < _nextTurn) {
                return;
            }

            int direction = ( input.x > 0 ) ? 1 : -1;

            transform.RotateAround(head.position, Vector3.up, 45 * direction);
            
            _nextTurn = Time.timeSinceLevelLoad + turnCoolDown;
        }

    }

}