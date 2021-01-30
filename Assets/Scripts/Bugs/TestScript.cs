using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation
{
    public class TestScript : blah {
    }
    
    public class blah : MonoBehaviour {
        public Test[] aArray;
        [SerializeField] private Test[] bArray;
    }

    [System.Serializable]
    public class Test {
        public GameObject a;
        public int b;
        public float c;
    }

}
