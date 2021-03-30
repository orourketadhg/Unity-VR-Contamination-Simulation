using Unity.Entities;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Particles {

    /**
     * Component Data for the internal workings of the virus particle spawner  
     */
    public struct ParticleSpawnerInternalSettingsData : IComponentData {
        public float spawnerDuration;
        public float spawnerStartTime;
        public int isSpawnerActive;
        public float inputCooldown;
        public float timeOfLastInput;
    }

}