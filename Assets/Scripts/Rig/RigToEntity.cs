using System;
using System.Collections.Generic;
using com.TUDublin.VRContaminationSimulation.ECS.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.Rig {

    public class RigToEntity : MonoBehaviour {

        public Transform[] rig;
        private EntityManager _manager;
        private EntityQuery _rigQuery;
        private readonly Dictionary<Transform, Entity> _rigEntityPairs = new Dictionary<Transform, Entity>();

        private void Start() {
            _manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _rigQuery = _manager.CreateEntityQuery(ComponentType.ReadOnly<XRRigData>());
            _rigQuery.SetChangedVersionFilter(typeof(XRRigData));
            
            var entities = _rigQuery.ToEntityArray(Allocator.Temp);
            foreach (Entity entity in entities) {
                XRRigData entityRigType = _manager.GetComponentData<XRRigData>(entity);
                foreach (Transform node in rig) {
                    RigType xrRigType = node.GetComponent<RigType>();
                    
                    if (xrRigType.type != entityRigType.Type) {
                        continue;
                    }
                    
                    _rigEntityPairs.Add(node.transform, entity);
                }
            }

        }

        private void LateUpdate() {
            UpdateRigPositions();
        }

        private void UpdateRigPositions() {
            foreach (var pair in _rigEntityPairs) {
                _manager.SetComponentData(pair.Value, new Translation() {
                    Value = pair.Key.position
                });
                _manager.SetComponentData(pair.Value, new Rotation() {
                    Value = pair.Key.rotation
                });
            }
        }
        
    }

}