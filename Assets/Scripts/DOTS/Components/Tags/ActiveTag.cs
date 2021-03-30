using Unity.Entities;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.DOTS.Components.Tags {
    
    /**
     * Tag for Active virus particles entities - able to stick to objects
     */
    [GenerateAuthoringComponent]
    public struct ActiveTag : IComponentData{
    }

}