using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

public class ECSRayCastTestManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.white, 1f);
            Debug.Log(RayCast(ray.origin, ray.direction, 100f));
        }
    }

    private Entity RayCast(Vector3 start, Vector3 direction, float distance)
    {
        BuildPhysicsWorld buildPhysicsWorld = World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
        CollisionWorld collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;

        RaycastInput raycastInput = new RaycastInput
        {
            Start = start,
            End = start + direction * distance,
            Filter = new CollisionFilter()
            {
                BelongsTo = ~0u, // A bit mask describing which layers this collider belongs to.
                CollidesWith = ~0u, //A bit mask describing which layers this collider can collide with. // all 1s, so all layers, collide with everything
                GroupIndex = 1 // An override for the bit mask checks. If the value in both objects is equal and positive, the objects always collide. If the value in both objects is equal and negative, the objects never collide.
            }
        };

        Unity.Physics.RaycastHit raycastHit = new Unity.Physics.RaycastHit();

        if (collisionWorld.CastRay(raycastInput, out raycastHit))
        {
            Entity hitEntity = buildPhysicsWorld.PhysicsWorld.Bodies[raycastHit.RigidBodyIndex].Entity;
            return hitEntity;
        }
        else
            return Entity.Null;
    }
}
