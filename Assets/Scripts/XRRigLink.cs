using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Com.TUDublin.VRContaminationSimulation {

    public class XRRigLink : MonoBehaviour {

        public Entity TargetEntity;
        public SyncType syncType;

        private EntityManager _entityManager;

        private void Start() {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        }

        private void Update() {
            SyncToTarget();
        }

        private void SyncToTarget() {
            switch (syncType) {
                case SyncType.RootEntity:
                    transform.position = _entityManager.GetComponentData<LocalToWorld>(TargetEntity).Position;
                    transform.rotation = _entityManager.GetComponentData<LocalToWorld>(TargetEntity).Rotation;
                    break;
            
                case SyncType.LocalEntity:
                    _entityManager.SetComponentData(TargetEntity, new Translation() {
                        Value = transform.localPosition
                    });
            
                    _entityManager.SetComponentData(TargetEntity, new Rotation() {
                        Value = transform.localRotation
                    });
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException();
                
            }
        }
    
        public enum SyncType {
            RootEntity,
            LocalEntity
        }
    
    }

}
