using UnityEngine;

public class TowerPlaneSpawner : MonoBehaviour
{
    public GameObject planePrefab;       // Prefab of the plane
    public float spawnRadius = 50f;      // Radius within which planes spawn
    public float despawnRadius = 100f;   // Radius outside which planes despawn
    public float spawnInterval = 3f;     // Time between spawns
    public float spawnHeightOffset = 10f; // Height offset for the spawn position

    private float spawnTimer;

    void Update()
    {
        // Handle spawning at regular intervals
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnPlane();
            spawnTimer = 0f;
        }
    }

    void SpawnPlane()
    {
        if (planePrefab == null)
        {
            Debug.LogError("Plane Prefab is not assigned in TowerPlaneSpawner.");
            return;
        }

        // Calculate a random spawn point within the spawn radius
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, spawnHeightOffset, randomCircle.y) + transform.position;

        // Instantiate the plane
        GameObject newPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);

        // Configure the plane's behavior
        PlaneBehavior planeBehavior = newPlane.GetComponent<PlaneBehavior>();
        if (planeBehavior != null)
        {
            planeBehavior.targetTower = this.transform; // Set the tower as the target
            planeBehavior.despawnRadius = despawnRadius;
        }
        else
        {
            Debug.LogError("PlanePrefab is missing the PlaneBehavior script.");
        }
    }
}
