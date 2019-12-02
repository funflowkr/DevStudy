using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.Linq;
using Unity.Rendering;

public class FindTargetSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entities.WithNone<HasTarget>().WithAll<Unit>().ForEach((Entity unitEntity, ref Translation unitTranslation) => 
        {
            Debug.Log("Unit " + unitEntity);
            Entity closestTargetEntity = Entity.Null;
            float3 unitPosition = unitTranslation.Value;
            float3 closestTargetPosition = float3.zero;

            Entities.WithAll<Target>().ForEach((Entity targetEntity, ref Translation targetTranslation) =>
            {
                Debug.Log("Target " + targetEntity);

                if (closestTargetEntity == Entity.Null)
                {
                    closestTargetEntity = targetEntity;
                    closestTargetPosition = targetTranslation.Value;
                }
                else
                {
                    if (math.distance(unitPosition, targetTranslation.Value) < math.distance(unitPosition, closestTargetPosition))
                    {
                        closestTargetEntity = targetEntity;
                        closestTargetPosition = targetTranslation.Value;
                    }
                }
            });

            if (closestTargetEntity != Entity.Null)
            {
                Debug.DrawLine(unitPosition, closestTargetPosition);
                PostUpdateCommands.AddComponent(unitEntity, new HasTarget
                {
                    targetEntity = closestTargetEntity
                });
                //World.Active.EntityManager.GetComponentData<Translation>(closestTargetEntity);
            }
        });
    }
}
