using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WhenTowerSpawned : MonoBehaviour
{

    // Public fields for setting up the objects to spawn and their positions
    
    [SerializeField]
    private GameObject objectToSpawn;// Assign the prefab to spawn
    public float spawnRadius = 2.0f; // Distance from the tower for spawned objects

    // This method is called when the tower is instantiated in the scene
    private void Start()
    {
        // Check if objectToSpawn is assigned to avoid null reference errors
        //if (objectToSpawn == null)
        //{
        //    Console.log("Object to spawn is not assigned. Please assign a prefab in the inspector.");
        //    return;
        //}

        // Call the method to spawn the objects
        SpawnObjectsAroundTower();
    }

    // Spawns three objects around the tower
    private void SpawnObjectsAroundTower()
    {
        // Define three positions around the tower
        for (int i = 0; i < 3; i++)
        {
            // Calculate the angle for each object (120 degrees apart)
            float angle = i * 120 * Mathf.Deg2Rad;

            // Calculate the position based on the angle and spawn radius
            Vector3 spawnPosition = new Vector3(
                transform.position.x + Mathf.Cos(angle) * spawnRadius,
                transform.position.y,
                transform.position.z + Mathf.Sin(angle) * spawnRadius
            );

            // Instantiate the object at the calculated position with default rotation
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }

}
