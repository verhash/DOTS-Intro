using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;
using Unity.Transforms;
using UnityEngine;

public partial struct SpawnerSystem : ISystem
{
    private double WaveDuration;
    private double WavePause;
    private double LastWave;
    private bool ShouldSpawn;

    public void OnCreate(ref SystemState state) 
    {
        LastWave = SystemAPI.Time.ElapsedTime;
        WaveDuration = 5;
        WavePause = 5;
        ShouldSpawn = true;
    }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state) 
    {
        double TimeSinceLastWave = SystemAPI.Time.ElapsedTime - LastWave;

        if (ShouldSpawn)
        {
            if (TimeSinceLastWave > WaveDuration)
            {
                ShouldSpawn = false;
            }
        }
        else
        {
            if (TimeSinceLastWave > (WaveDuration + WavePause))
            {
                ShouldSpawn = true;
            }
        }

        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
            {
                if (ShouldSpawn == true)
                {
                    Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                    float3 pos = new float3(spawner.ValueRW.Random.NextFloat(-5, 5), spawner.ValueRO.SpawnPosition.y, 0);
                    state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
                }

                spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
            }
        }
    }
}
