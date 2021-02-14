using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation
{
    public class Sticky : MonoBehaviour {

        [SerializeField] private Vector3 initialVelocity;
        private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start() {
            _rigidbody.AddForce(initialVelocity);
        }

        private void OnCollisionEnter(Collision other) {
            transform.parent = other.transform; 
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }

    }
}
