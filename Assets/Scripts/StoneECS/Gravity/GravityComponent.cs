using Unity.Entities;
using Unity.Mathematics;

public struct GravityComponent : IComponentData
{
    public float Weight;
    public float Velocity;
    public float3 TargetPos;
}
