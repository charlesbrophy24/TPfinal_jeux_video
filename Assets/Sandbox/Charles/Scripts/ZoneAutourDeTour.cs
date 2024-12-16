using UnityEngine;
using System.Collections.Generic;

public class ZoneAutourDeTour : MonoBehaviour
{
    public GameObject planePrefab; // Reference to the plane prefab
    public float spawnRadiusX = 50f; // Radius of the spawn area along the X-axis
    public float spawnRadiusY = 50f; // Radius of the spawn area along the Y-axis
    public float spawnRadiusZ = 50f; // Radius of the spawn area along the Z-axis
    public float maxSpawnDistance = 100f; // Maximum distance from the tower where planes can spawn
    public float spawnDistance = 51f; // Minimum distance from the tower where planes spawn
    public float spawnHeightOffset = 10f; // Optional height offset to control how high or low planes spawn
    public float minSpawnHeight = 20f; // Minimum height at which planes should spawn

    private List<GameObject> spawnedPlanes = new List<GameObject>(); // List to store active planes

    // Update is called once per frame
    void Update()
    {
        // Loop through all planes and check if they should despawn
        for (int i = spawnedPlanes.Count - 1; i >= 0; i--)
        {
            GameObject plane = spawnedPlanes[i];

            // Get the position of the plane and the tower
            Vector3 planePosition = plane.transform.position;
            Vector3 towerPosition = transform.position;

            // Calculate the distance between the plane and the tower
            float distanceFromTower = Vector3.Distance(planePosition, towerPosition);

            // Debug log to check the distance for troubleshooting
            Debug.Log("Distance from tower: " + distanceFromTower);

            // If the plane is outside the max spawn distance, despawn it
            if (distanceFromTower > maxSpawnDistance)
            {
                Debug.Log("Plane despawned, exceeding max distance");
                Destroy(plane);  // Despawn the plane if it exceeds the max spawn distance
                spawnedPlanes.RemoveAt(i);  // Remove it from the list
            }
        }
    }

    // Method to spawn a plane just outside the spawn radius, but within max spawn distance
    public void SpawnPlane()
    {
        // Calculate a random position within the defined spawn range
        float randomX = Random.Range(spawnDistance, Mathf.Min(spawnDistance + spawnRadiusX, maxSpawnDistance));
        float randomZ = Random.Range(spawnDistance, Mathf.Min(spawnDistance + spawnRadiusZ, maxSpawnDistance));

        // Calculate the Y position for spawning (ensuring it is above the minimum spawn height)
        float randomY = Random.Range(transform.position.y - spawnHeightOffset, transform.position.y + spawnHeightOffset);
        randomY = Mathf.Max(randomY, transform.position.y + minSpawnHeight); // Ensure the Y position is at least minSpawnHeight

        // Calculate the final spawn position
        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, randomZ);

        // Instantiate the plane at the calculated position
        GameObject newPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);
        spawnedPlanes.Add(newPlane); // Add the plane to the list of spawned planes
    }

    // Draw Gizmos for the spawn, despawn zones, and max spawn distance in the editor
    void OnDrawGizmos()
    {
        // Set the Gizmo color for the spawn zone (Green)
        Gizmos.color = Color.green;
        // Draw the spawn zone as a wireframe cube
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRadiusX * 2, spawnRadiusY * 2, spawnRadiusZ * 2));

        // Set the Gizmo color for the max spawn zone (Blue)
        Gizmos.color = Color.blue;
        // Draw a wireframe sphere to represent the max spawn distance
        Gizmos.DrawWireSphere(transform.position, maxSpawnDistance);

        // Draw the minimum spawn height as a line (Yellow) to indicate where planes can't spawn below
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x - 10, transform.position.y + minSpawnHeight, transform.position.z - 10),
                        new Vector3(transform.position.x + 10, transform.position.y + minSpawnHeight, transform.position.z + 10));
    }
}