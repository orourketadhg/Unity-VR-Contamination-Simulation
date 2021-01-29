﻿using com.TUDublin.VRContaminationSimulation.Common.Interfaces;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Systems;

namespace com.TUDublin.VRContaminationSimulation.ECS.Systems {

    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(BuildPhysicsWorld))]
    public abstract class VirusParticleSpawnerSystemBase<T> : SystemBase where T : struct, IComponentData, IVirusParticleSettings{

        protected override void OnUpdate() {
        }
        
    }

}