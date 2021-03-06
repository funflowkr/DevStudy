﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.Linq;

public class FindTargetSystem : ComponentSystem
{
    protected override void OnUpdate()
    {

        //World.Active.GetOrCreateManager<EntityManager>().Get
        var entityManager = World.Active.GetOrCreateManager<EntityManager>();
        var units = entityManager.GetAllEntities().Where( r => entityManager.HasComponent<Unit>(r));
        var targets = entityManager.GetAllEntities().Where(r => entityManager.HasComponent<Target>(r));

        foreach (var unit in units)
        {
            Debug.Log(unit);
        }

        foreach (var target in targets)
        {
            Debug.Log(target);
        }
    }
}
