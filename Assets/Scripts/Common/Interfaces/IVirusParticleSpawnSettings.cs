using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    public interface IVirusParticleSpawnSettings {

        Entity Prefab { get; set; }

    }

}