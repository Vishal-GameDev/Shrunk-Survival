using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public GameObject firePrefab;
    public Terrain terrain;
    public int fireCount = 5;
    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating("SpawnFire", 0f, spawnInterval);
    }

    void SpawnFire()
    {
        for (int i = 0; i < fireCount; i++)
        {
            Vector3 spawnPos = GetRandomTerrainPosition();
            Instantiate(firePrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomTerrainPosition()
    {
        float x = Random.Range(0, terrain.terrainData.size.x);
        float z = Random.Range(0, terrain.terrainData.size.z);
        float y = terrain.SampleHeight(new Vector3(x, 0, z)) + 0.1f; // Slightly above terrain

        return new Vector3(x, y, z);
    }
}
