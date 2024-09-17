using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_Prefab;
    [SerializeField] float m_SpawnRate;
    private float m_SpawnTime;

    private void Update()
    {
        if (m_SpawnTime < Time.time)
        {
            Instantiate(m_Prefab);
            m_SpawnTime = Time.time + m_SpawnRate;
        }
    }
}
