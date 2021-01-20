using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.Rig {

    public class RigToEntity : MonoBehaviour {

        public RigType[] rig;
        private EntityManager _manager;
        private EntityQuery _rigQuery;

        private void Start() {
            _manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _rigQuery = _manager.CreateEntityQuery(ComponentType.ReadOnly<XRRigData>());
            _rigQuery.SetChangedVersionFilter(typeof(XRRigData));
        }

        private void LateUpdate() {
            var entities = _rigQuery.ToEntityArray(Allocator.Temp);
            foreach (Entity entity in entities) {
                XRRigData rigType = _manager.GetComponentData<XRRigData>(entity);

                foreach (RigType node in rig) {
                    if (node.type != rigType.Type) {
                        continue;
                    }

                    _manager.SetComponentData(entity, new Translation() {
                        Value = node.transform.position
                    });
                    _manager.SetComponentData(entity, new Rotation() {
                        Value = node.transform.rotation
                    });
                }
            }
        }
    }

}