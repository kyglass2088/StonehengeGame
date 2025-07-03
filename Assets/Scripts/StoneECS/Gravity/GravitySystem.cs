using Unity.Entities;
using Unity.Transforms;

public partial struct GravitySystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach (var (mytemp, gravity) in SystemAPI.Query<RefRW<LocalTransform>,
            RefRW<GravityComponent>>())
        {
            gravity.ValueRW.Velocity -= gravity.ValueRO.Weight * dt;
            mytemp.ValueRW.Position.y += gravity.ValueRO.Velocity * dt;
            mytemp.ValueRW.Position.x += 0.01f; // gravity.ValueRO.TargetPos.x;
            mytemp.ValueRW.Position.y += 0.011f;// gravity.ValueRO.TargetPos.y;
            mytemp.ValueRW.Position.z += 0.011f;// gravity.ValueRO.TargetPos.x;
            

            //transform.ValueRW.Position = Vector3.MoveTowards(transform.ValueRW.Position,
            //    gravity.ValueRO.TargetPos, 5 * dt);
        }
    }
}