using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlaneDestroy : MonoBehaviour
{
    public float timeToDestroy = 5f; // Time in seconds before the object is destroyed

    void Start()
    {
        // Destroy the GameObject this script is attached to after the specified time
        Destroy(gameObject, timeToDestroy);
    }
}
