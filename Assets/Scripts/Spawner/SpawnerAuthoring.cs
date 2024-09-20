using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnRate;
    public int AmountToSpawnATime;
    public List<Prefab> Asteroids;
    public float AsteroidLifeTime;

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = new float2(0,7),
                SpawnAmount = authoring.AmountToSpawnATime,
                NextSpawnTime = 0,
                SpawnRate = authoring.SpawnRate,
                Random = new Unity.Mathematics.Random(1)
            });

            AddComponent(entity, new AsteroidLifeTime
            {
                Value = authoring.AsteroidLifeTime
            });
        }
    }
}

public struct AsteroidLifeTime : IComponentData
{
    public float Value;
}

