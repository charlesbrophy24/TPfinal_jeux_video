using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvionSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;    // Prefab to spawn
    [SerializeField] private Vector3 ZoneSize;     // Size of the zone in which to spawn the prefab

    private GameObject currentInstance;            // Track the current spawned instance

    void Start()
    {
        // Initially spawn an object at the start if needed
        SpawnPrefab();
    }

    void Update()
    {
        // Check if the current instance is null (destroyed) and spawn a new one
        if (currentInstance == null)
        {
            SpawnPrefab();
        }
    }

    // Method to spawn the prefab in a random position within the zone
    private void SpawnPrefab()
    {
        // Randomize position within the zone size
        Vector3 spawnPosition = new Vector3(
            Random.Range(transform.position.x - ZoneSize.x / 2, transform.position.x + ZoneSize.x / 2),
            Random.Range(transform.position.y - ZoneSize.y / 2, transform.position.y + ZoneSize.y / 2),
            Random.Range(transform.position.z - ZoneSize.z / 2, transform.position.z + ZoneSize.z / 2) // Corrected axis here
        );

        // Instantiate the prefab
        currentInstance = Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    // Draw the zone in the editor for visualization
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, ZoneSize);
    }
}
