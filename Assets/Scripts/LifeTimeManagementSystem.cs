using Unity.Burst;
using Unity.Entities;

[UpdateInGroup(typeof(LateSimulationSystemGroup), OrderLast = true)]
public partial struct IsDestroyManagementSystem : ISystem
{
    void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<IsDestroying>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach (var (tag, entity) in SystemAPI.Query<IsDestroying>().WithEntityAccess())
        {
            ecb.DestroyEntity(entity);
        }

        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

}


[BurstCompile]
public partial struct LifeTimeManagementSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state) 
    {
        state.RequireForUpdate<LifeTime>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);
        float deltaTime = SystemAPI.Time.DeltaTime;

        new LifeJob
        {
            ecb = ecb,
            DeltaTime = deltaTime,
        }.Schedule();

        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}

[BurstCompile]
public partial struct LifeJob : IJobEntity
{
    public EntityCommandBuffer ecb;
    public float DeltaTime;

    public void Execute(Entity entity, ref LifeTime lifeTime)
    {
        lifeTime.Value -= DeltaTime;

        if (lifeTime.Value <= 0) 
        {
            ecb.AddComponent<IsDestroying>(entity);
        }
    }
}
