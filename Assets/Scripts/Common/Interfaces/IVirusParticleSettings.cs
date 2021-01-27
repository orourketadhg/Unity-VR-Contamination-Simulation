using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.Common.Interfaces {

    public interface IVirusParticleSpawnSettings {

        Entity Prefab { get; set; }
        float2 Scale { get; set; }
        float2 InitialEmissionForce { get; set; }

        
    }

}