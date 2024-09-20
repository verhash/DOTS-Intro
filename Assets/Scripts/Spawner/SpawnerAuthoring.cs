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

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Spawner
            {
                Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                SpawnPosition = new float2(0,3),
                SpawnAmount = authoring.AmountToSpawnATime,
                NextSpawnTime = 0,
                SpawnRate = authoring.SpawnRate,
                Random = new Unity.Mathematics.Random(1)
            });
        }
    }
}
