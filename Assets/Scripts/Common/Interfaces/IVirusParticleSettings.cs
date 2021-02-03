using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IVirusParticleSettings {

        Entity Prefab { get; set; }
        public float2 ParticleScale { get; set; }
        public float2 InitialEmissionForce { get; set; }

    }

}