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
        List<Entity> alreadySelecteds = new List<Entity>();

        Entities.WithNone<HasTarget>().WithAll<Unit>().ForEach((Entity unitEntity, ref Translation unitTranslation) => 
        {
            //Debug.Log("Unit " + unitEntity);
            Entity closestTargetEntity = Entity.Null;
            float3 unitPosition = unitTranslation.Value;
            float3 closestTargetPosition = float3.zero;

            Entities.WithAll<Target>().ForEach((Entity targetEntity, ref Translation targetTranslation) =>
            {
                //Debug.Log("Target " + targetEntity);

                if (closestTargetEntity == Entity.Null)
                {
                    closestTargetEntity = targetEntity;
                    closestTargetPosition = targetTranslation.Value;
                    alreadySelecteds.Add(closestTargetEntity);
                }
                else
                {
                    /// 선택되지 않은 타겟중에 가장 가까운 타겟을 고른다.
                    if (math.distance(unitPosition, targetTranslation.Value) < math.distance(unitPosition, closestTargetPosition) && !alreadySelecteds.Contains(targetEntity))
                    {
                        closestTargetEntity = targetEntity;
                        closestTargetPosition = targetTranslation.Value;
                        alreadySelecteds.Add(closestTargetEntity);
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
            }
        });
    }
}
