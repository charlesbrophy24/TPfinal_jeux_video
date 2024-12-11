using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public ZoneAutourDeTour tower; // Reference to the Tower
    

    void Start()
    {
        // Spawn planes every 3 seconds starting after 1 second
        InvokeRepeating("SpawnAvion", 1f, 3f);
    }

    


    // Method to spawn a plane via the Tower
    void SpawnAvion()
    {

        tower.SpawnPlane();
    }
}
