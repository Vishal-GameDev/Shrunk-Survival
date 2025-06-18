using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab; // Assign the Key Prefab in Inspector
    public Terrain terrain; // Assign your Terrain in Inspector

    private GameObject spawnedKey; // Keep track of the spawned key

    void Start()
    {
        SpawnKey();
    }

    void SpawnKey()
    {
        if (spawnedKey != null) return; // Prevent multiple keys from spawning

        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainHeight = terrain.SampleHeight(new Vector3(terrainWidth / 2, 0, terrainLength / 2)); // Get center height

        Vector3 spawnPosition = new Vector3(
            Random.Range(0, terrainWidth),
            terrainHeight + 1,  // Ensure it spawns above the ground
            Random.Range(0, terrainLength)
        );

        spawnedKey = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
    }
}
