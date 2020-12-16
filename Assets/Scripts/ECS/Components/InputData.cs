using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    [GenerateAuthoringComponent]
    public struct InputData : IComponentData {

        public ControllerHand Hand;
        
        public bool Grip;
        public bool Trigger;
        public bool PrimaryButton;
        public bool SecondaryButton;
        public float2 Joystick;
        
    }

    public enum ControllerHand {
        Left,
        Right
    }

}