using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public struct Spawner : IComponentData
{
    public Entity Prefab;
    public float2 SpawnPosition; // Better Vector2
    public int SpawnAmount;
    public float NextSpawnTime;
    public float SpawnRate;
    public Random Random;
}
