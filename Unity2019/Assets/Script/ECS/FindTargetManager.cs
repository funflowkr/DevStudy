﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;

public class FindTargetManager : MonoBehaviour
{

    [SerializeField] private Mesh _unitMesh;
    [SerializeField] private Mesh _targetMesh;

    [SerializeField] private Material _unitMat;
    [SerializeField] private Material _targetMat;

    private static EntityManager _entityManager;

    // Start is called before the first frame update
    void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        SpwanUnitEntity();

        for (int i = 0 ; i < 10 ; ++i)
        {
            SpwanTargetEntity();
        }
    }

    private void SpwanUnitEntity()
    {
        Entity entity = _entityManager.CreateEntity(
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(Scale),
            typeof(Unit)
            );

        SetEntityComponentData(entity, new float3(UnityEngine.Random.Range(-8, 8f), UnityEngine.Random.Range(-5, 5f), 0f), _unitMesh, _unitMat);
        _entityManager.SetComponentData(entity, new Scale { Value = 1.5f });
    }

    private void SpwanTargetEntity()
    {
        Entity entity = _entityManager.CreateEntity(
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(Scale),
            typeof(Target)
            );

        SetEntityComponentData(entity, new float3(UnityEngine.Random.Range(-8, 8f), UnityEngine.Random.Range(-5, 5f), 0f), _targetMesh, _targetMat);
        _entityManager.SetComponentData(entity, new Scale { Value = 0.5f });
    }

    private void SetEntityComponentData(Entity entity, float3 spawnPosition, Mesh mesh, Material material)
    {
        _entityManager.SetSharedComponentData<RenderMesh>(entity, new RenderMesh
        {
            material = material,
            mesh = mesh,
        });


        _entityManager.SetComponentData<Translation>(entity, new Translation
        {
            Value = spawnPosition
        });
    }
}


public struct Unit : IComponentData { }
public struct Target : IComponentData { }

public struct HasTarget : IComponentData
{
    public Entity targetEntity;
}
