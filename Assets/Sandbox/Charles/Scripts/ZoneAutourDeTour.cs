using UnityEngine;
using System.Collections.Generic;

public class ZoneAutourDeTour : MonoBehaviour
{
    public GameObject planePrefab; // Reference to the plane prefab
    public float spawnRadiusX = 50f; // Radius of the spawn area along the X-axis
    public float spawnRadiusY = 50f; // Radius of the spawn area along the Y-axis
    public float spawnRadiusZ = 50f; // Radius of the spawn area along the Z-axis
    public float despawnRadiusX = 55f; // Radius at which planes should despawn along the X-axis
    public float despawnRadiusY = 55f; // Radius at which planes should despawn along the Y-axis
    public float despawnRadiusZ = 55f; // Radius at which planes should despawn along the Z-axis
    public float spawnDistance = 51f; // Distance from the tower where planes spawn

    private List<GameObject> spawnedPlanes = new List<GameObject>(); // List to store active planes

    // Update is called once per frame
    void Update()
    {
        // Update all spawned planes
        for (int i = spawnedPlanes.Count - 1; i >= 0; i--)
        {
            GameObject plane = spawnedPlanes[i];
            // Check distance between the plane and the tower
            if (Vector3.Distance(plane.transform.position, transform.position) > Mathf.Max(despawnRadiusX, despawnRadiusY, despawnRadiusZ))
            {
                Destroy(plane);  // Despawn the plane if it exceeds the despawn radius
                spawnedPlanes.RemoveAt(i);  // Remove it from the list
            }
        }
    }

    // Method to spawn a plane just outside the spawn radius
    public void SpawnPlane()
    {
        // Calculate a random spawn position just outside the spawn radius
        Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(spawnDistance, spawnDistance + spawnRadiusX),
            Random.Range(spawnDistance, spawnDistance + spawnRadiusY),
            Random.Range(spawnDistance, spawnDistance + spawnRadiusZ)
        );

        // Instantiate the plane at the calculated position
        GameObject newPlane = Instantiate(planePrefab, spawnPosition, Quaternion.identity);
        spawnedPlanes.Add(newPlane); // Add the plane to the list of spawned planes
    }

    // Draw Gizmos for the spawn and despawn zones in the editor
    void OnDrawGizmos()
    {
        // Set the Gizmo color for the spawn zone (Green)
        Gizmos.color = Color.green;
        // Draw the spawn zone as a cube
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRadiusX * 2, spawnRadiusY * 2, spawnRadiusZ * 2));

        // Set the Gizmo color for the despawn zone (Red)
        Gizmos.color = Color.red;
        // Draw the despawn zone as a cube
        Gizmos.DrawWireCube(transform.position, new Vector3(despawnRadiusX * 2, despawnRadiusY * 2, despawnRadiusZ * 2));
    }
}