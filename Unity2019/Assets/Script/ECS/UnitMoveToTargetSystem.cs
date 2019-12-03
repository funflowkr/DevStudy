using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class UnitMoveToTargetSystem : ComponentSystem
{
    private float moveSpeed = 10f;
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref HasTarget hasTarget, ref Translation translation) => 
        {

            if (World.DefaultGameObjectInjectionWorld.EntityManager.Exists(hasTarget.targetEntity))
            {
                Translation targetTranslation = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Translation>(hasTarget.targetEntity);

                float3 dir = math.normalize(targetTranslation.Value - translation.Value);
                translation.Value += dir * moveSpeed * Time.DeltaTime;

                if (math.distance(targetTranslation.Value, translation.Value) < 0.2f)
                {
                    PostUpdateCommands.DestroyEntity(hasTarget.targetEntity);
                    PostUpdateCommands.RemoveComponent(entity, typeof(HasTarget));
                }
            }
            else
            {
                PostUpdateCommands.RemoveComponent(entity, typeof(HasTarget));
            }
        });
    }
}
