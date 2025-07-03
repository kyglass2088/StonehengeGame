using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class GravityComponentAuthoring : MonoBehaviour
{
    public float Weight;
    public float Velocity;
    public float3 TargetPos;
    public Transform Player;

    private void Awake()
    {
        TargetPos = Player.transform.position;
    }

    class Baker : Baker<GravityComponentAuthoring>
    {
        public override void Bake(GravityComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new GravityComponent()
            {
                Weight = authoring.Weight,
                Velocity = authoring.Velocity,
                TargetPos = authoring.Player.transform.position
            });
        }

    }
}
