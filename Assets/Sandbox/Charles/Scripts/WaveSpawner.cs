using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject planePrefab; // Assign your plane prefab here
    public Transform targetTower;  // Target tower for planes to move towards
    public float spawnRadius = 20f; // Radius around the spawner to spawn planes

    [Header("Wave Settings")]
    public int planesPerWave = 5; // Starting number of planes per wave
    public float timeBetweenWaves = 5f; // Time between waves
    private int currentWave = 1;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            Debug.Log($"Starting Wave {currentWave}");

            for (int i = 0; i < planesPerWave; i++)
            {
                SpawnPlane();
                yield return new WaitForSeconds(0.5f); // Delay between each plane spawn
            }

            currentWave++;
            planesPerWave += 2; // Increase difficulty each wave
            yield return new WaitForSeconds(timeBetweenWaves); // Wait for next wave
        }
    }

    void SpawnPlane()
    {
        // Random position around the spawner within a radius
        Vector3 spawnPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
        spawnPosition.y = 0f; // Keep planes on ground level

        // Instantiate the plane
        GameObject plane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);

        // Assign the target tower
        PlaneMover planeMover = plane.GetComponent<PlaneMover>();
        if (planeMover != null)
        {
            planeMover.SetTarget(targetTower);
        }
    }
}