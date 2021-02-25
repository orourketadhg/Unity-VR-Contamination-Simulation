using System;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation {

    public class TestForce  : MonoBehaviour {

        public Vector3 initialForce;
        private Rigidbody _rigidbody;
        
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void Start() {
            _rigidbody.AddForce(initialForce);
        }

    }

}