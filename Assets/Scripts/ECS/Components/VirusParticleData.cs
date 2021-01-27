using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Entities;
using Unity.Mathematics;

namespace com.TUDublin.VRContaminationSimulation.ECS.Components {

    public struct ActiveLightParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 Scale { get; set; }
        public float2 InitialEmissionForce { get; set; }
        
    }
    
    public struct DecayingActiveLightParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 Scale { get; set; }
        public float2 InitialEmissionForce { get; set; }
        
    }
    
    public struct ActiveHeavyParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 Scale { get; set; }
        public float2 InitialEmissionForce { get; set; }
        
    }
    
    public struct DecayingActiveHeavyParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 Scale { get; set; }
        public float2 InitialEmissionForce { get; set; }
        
    }
    
    public struct PassiveLightParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 Scale { get; set; }
        public float2 InitialEmissionForce { get; set; }
        
    }
    
    public struct DecayingPassiveLightParticleData : IComponentData, IVirusParticleSettings {

        public Entity Prefab { get; set; }
        public float2 Scale { get; set; }
        public float2 InitialEmissionForce { get; set; }
        
    }

}