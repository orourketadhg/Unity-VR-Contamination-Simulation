using System;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace com.TUDublin.VRContaminationSimulation.RigConversion {

    public class XRRigLink : MonoBehaviour {

        public Entity TargetEntity;
        public SyncType syncType;

        private EntityManager _entityManager;

        private void Start() {
            // get worlds Entity Manager
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        }

        private void Update() {
            // every frame update the position of the ECS rig
            SyncToTarget();
        }

        private void SyncToTarget() {
            switch (syncType) {
                // sync entities rotation and position to this - ROOT
                case SyncType.RootEntity:
                    transform.position = _entityManager.GetComponentData<LocalToWorld>(TargetEntity).Position;
                    transform.rotation = _entityManager.GetComponentData<LocalToWorld>(TargetEntity).Rotation;
                    break;
            
                // sync entities local rotation and position to this - child
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
