using UnityEngine;
using Unity.Entities;
using Unity.Physics;

public class JumpBallSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Entities.ForEach((ref PhysicsVelocity physicsVelocity) =>
            {
                physicsVelocity.Linear.y = 5;
            });
        }
    }
}
