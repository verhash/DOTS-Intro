using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AsteroidAuthoring : MonoBehaviour
{
    public float AsteroidSpeed;

    public class AsteroidAuthoringBaker : Baker<AsteroidAuthoring>
    {
        public override void Bake(AsteroidAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new AsteroidMoveSpeed { Value = authoring.AsteroidSpeed });
        }
    }
}

public struct AsteroidMoveSpeed : IComponentData
{
    public float Value;
}