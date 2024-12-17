using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;    // Car prefab to spawn
    public Transform tower;         // The tower the cars will drive towards
    public float spawnRadius = 20f; // Distance from the tower to spawn cars
    public float minDistanceFromTower = 10f; // Minimum distance cars should spawn from the tower
    public int carsPerWave = 5;     // Number of cars per wave
    public float waveInterval = 5f; // Time between waves
    public float carSpeed = 10f;    // Speed of the cars

    private List<GameObject> spawnedCars = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    // Spawns waves of cars
    IEnumerator SpawnWaves()
    {
        while (true)
        {
            // Spawn a wave of cars
            for (int i = 0; i < carsPerWave; i++)
            {
                SpawnCar();
                yield return new WaitForSeconds(0.5f); // Delay between car spawns
            }

            // Wait for the next wave
            yield return new WaitForSeconds(waveInterval);
        }
    }

    // Spawns a single car
    void SpawnCar()
    {
        // Generate a random position on the NavMesh, at the specified distance from the tower
        Vector3 spawnPos = GetRandomSpawnPosition();

        // Instantiate the car at the spawn position
        GameObject car = Instantiate(carPrefab, spawnPos, Quaternion.identity);

        // Set the car's destination towards the tower
        NavMeshAgent agent = car.GetComponent<NavMeshAgent>();
        agent.destination = tower.position;
        agent.speed = carSpeed;

        // Add to the list of spawned cars
        spawnedCars.Add(car);
    }

    // Get a random spawn position on the NavMesh within the specified radius
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPos = Vector3.zero;
        bool found = false;

        // Try to find a valid spawn position within the spawn area
        while (!found)
        {
            float randomDistance = Random.Range(minDistanceFromTower, spawnRadius);
            float randomAngle = Random.Range(0f, 360f);

            // Calculate spawn position based on random angle and distance
            Vector3 direction = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle));
            spawnPos = tower.position + direction * randomDistance;

            // Check if the position is on the NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPos, out hit, 5f, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
                found = true;
            }
        }

        return spawnPos;
    }

    // Optional: Remove cars after some time or if they reach the tower (if needed)
    void Update()
    {
        for (int i = 0; i < spawnedCars.Count; i++)
        {
            if (spawnedCars[i] == null)
            {
                spawnedCars.RemoveAt(i);
                i--; // Adjust index due to removal
            }
        }
    }
}