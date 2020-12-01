using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class TransformLink : MonoBehaviour {

    public Entity TargetEntity;
    public SyncType syncType;

    private EntityManager _entityManager;
    
    void Start() {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

    }
    
    void Update() {
        SyncTarget();
    }

    private void SyncTarget() {
        switch (syncType) {
            case SyncType.EntityToThis:
                transform.position = _entityManager.GetComponentData<LocalToWorld>(TargetEntity).Position;
                transform.rotation = _entityManager.GetComponentData<LocalToWorld>(TargetEntity).Rotation;
                break;
                
            case SyncType.ThisToEntityLocal:
                _entityManager.SetComponentData(TargetEntity, new Translation() {
                    Value = transform.localPosition
                });
                _entityManager.SetComponentData(TargetEntity, new Rotation() {
                    Value = transform.localRotation
                });
                
                break;
        }
    }
    
    public enum SyncType {
        EntityToThis,
        ThisToEntityLocal
    }
    
}
