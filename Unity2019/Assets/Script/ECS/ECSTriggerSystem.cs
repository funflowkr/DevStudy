using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

public class ECSCollisionSystem : JobComponentSystem
{
    private struct CollisionJob : ICollisionEventsJob
    {
        public ComponentDataFromEntity<PhysicsVelocity> physicsVelocityEntities;
        public ComponentDataFromEntity<Translation> translationEntities;

        public void Execute(CollisionEvent collisionEvent)
        {
            if (physicsVelocityEntities.HasComponent(collisionEvent.Entities.EntityA))
            {
                Translation translation = translationEntities[collisionEvent.Entities.EntityA];
                translation.Value = new Vector3(translation.Value.x, 5f, translation.Value.z);
                translationEntities[collisionEvent.Entities.EntityA] = translation;
            }

            if (physicsVelocityEntities.HasComponent(collisionEvent.Entities.EntityB))
            {
                Translation translation = translationEntities[collisionEvent.Entities.EntityB];
                translation.Value = new Vector3(translation.Value.x, 5f, translation.Value.z);
                translationEntities[collisionEvent.Entities.EntityB] = translation;
            }
        }
    }

    private BuildPhysicsWorld BuildPhysicsWorld;
    private StepPhysicsWorld StepPhysicsWorld;

    protected override void OnCreate()
    {
        BuildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        StepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        CollisionJob job = new CollisionJob()
        {
            physicsVelocityEntities = GetComponentDataFromEntity<PhysicsVelocity>(),
            translationEntities = GetComponentDataFromEntity<Translation>(),
        };
        return job.Schedule(StepPhysicsWorld.Simulation, ref BuildPhysicsWorld.PhysicsWorld, inputDeps);
    }

}
