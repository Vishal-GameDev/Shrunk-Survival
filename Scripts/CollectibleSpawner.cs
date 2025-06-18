using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject healthPrefab;
    public Terrain terrain;
    public int spawnCount = 10;
    public float minSpawnDistance = 5f; // Minimum distance between pickups

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        SpawnHealthPacks();
    }

    void SpawnHealthPacks()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos;
            int attempts = 0;
            do
            {
                spawnPos = GetRandomSpawnPosition();
                attempts++;
                if (attempts > 20) break; // Prevent infinite loops
            }
            while (IsTooClose(spawnPos));

            spawnedPositions.Add(spawnPos);
            Instantiate(healthPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainHeight = terrain.terrainData.size.y;

        float x = Random.Range(0, terrainWidth);
        float z = Random.Range(0, terrainLength);
        float y = terrain.SampleHeight(new Vector3(x, 0, z)) + 1f; // Adjust height

        return new Vector3(x, y, z);
    }

    bool IsTooClose(Vector3 newPos)
    {
        foreach (Vector3 pos in spawnedPositions)
        {
            if (Vector3.Distance(newPos, pos) < minSpawnDistance)
                return true;
        }
        return false;
    }
}
