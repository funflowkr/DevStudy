using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
/*
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
*/

public class FindTargetJobSystem : JobComponentSystem
{
    private struct EntityWithPosition
    {
        public Entity Entity;
        public float3 Position;
    }

    [RequireComponentTag(typeof(Unit))]
    [ExcludeComponent(typeof(HasTarget))]
    private struct FindTargetJob : IJobForEachWithEntity<Translation>
    {
        [DeallocateOnJobCompletion][ReadOnly]
        public NativeArray<EntityWithPosition> TargetEntity;
        public EntityCommandBuffer.Concurrent entityCommandBuffer;
        //NativeList<Entity> alreadySelecteds;

        public void Execute(Entity entity, int index, [ReadOnly] ref Translation translation)
        {
            //if (alreadySelecteds.IsCreated == false)
            //    alreadySelecteds = new NativeList<Entity>();

            Entity closestTargetEntity = Entity.Null;
            float3 unitPosition = translation.Value;
            float3 closestTargetPosition = float3.zero;

            for(int i = 0 ; i < TargetEntity.Length ; i++)
            {
                EntityWithPosition ep = TargetEntity[i];
                //Debug.Log("Target " + targetEntity);

                if (closestTargetEntity == Entity.Null)
                {
                    closestTargetEntity = ep.Entity;
                    closestTargetPosition = ep.Position;
                    //alreadySelecteds.Add(closestTargetEntity);
                }
                else
                {
                    /// 선택되지 않은 타겟중에 가장 가까운 타겟을 고른다.
                    if (math.distance(unitPosition, ep.Position) < math.distance(unitPosition, closestTargetPosition) /*&& !alreadySelecteds.Contains(ep.Entity)*/)
                    {
                        closestTargetEntity = ep.Entity;
                        closestTargetPosition = ep.Position;
                        //alreadySelecteds.Add(closestTargetEntity);
                    }
                }
            }

            if (closestTargetEntity != Entity.Null)
            {
                //Debug.DrawLine(unitPosition, closestTargetPosition);
                entityCommandBuffer.AddComponent(index, entity, new HasTarget
                {
                    targetEntity = closestTargetEntity
                });
            }
        }
    }

    private EndSimulationEntityCommandBufferSystem endSimulationEntityCommandBuffer;

    protected override void OnCreate()
    {
        endSimulationEntityCommandBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        base.OnCreate();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityQuery targetQuery = GetEntityQuery(typeof(Target), ComponentType.ReadOnly<Translation>());

        NativeArray<Entity> targetEntityArray = targetQuery.ToEntityArray(Allocator.TempJob);
        NativeArray<Translation> targetTranslationArray = targetQuery.ToComponentDataArray<Translation>(Allocator.TempJob);

        NativeArray<EntityWithPosition> targetArray = new NativeArray<EntityWithPosition>(targetEntityArray.Length, Allocator.TempJob);

        for (int i = 0 ; i < targetArray.Length ; i++)
        {
            targetArray[i] = new EntityWithPosition()
            {
                Entity = targetEntityArray[i],
                Position = targetTranslationArray[i].Value
            };
        }

        targetEntityArray.Dispose();
        targetTranslationArray.Dispose();

        FindTargetJob findTargetJob = new FindTargetJob()
        {
            TargetEntity = targetArray,
            entityCommandBuffer = endSimulationEntityCommandBuffer.CreateCommandBuffer().ToConcurrent()
        };

        JobHandle jobHandle = findTargetJob.Schedule(this, inputDeps);
        endSimulationEntityCommandBuffer.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}
