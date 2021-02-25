using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation
{
    public class Sticky : MonoBehaviour {
        private Rigidbody _rigidbody;
        private Collider _collider;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision other) {

            if (gameObject.GetComponent<HingeJoint>() != null) {
                Unstick();
            }
            
            var joint = gameObject.AddComponent<HingeJoint>();
            joint.connectedBody = other.rigidbody;
            _rigidbody.mass = 0.001f;
            _rigidbody.freezeRotation = true;
            _rigidbody.velocity = Vector3.zero;
            _collider.material.bounciness = 0;
        }

        private void Unstick() {
            Destroy(GetComponent<HingeJoint>());
            _rigidbody.mass = 1;
        }

    }
}
