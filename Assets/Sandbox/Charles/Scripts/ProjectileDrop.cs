using UnityEngine;

public class ProjectileDrop : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile prefab
    public float dropInterval = 1f;  // Time between each drop
    private float nextDropTime;

    public bool isDropping = false;  // Flag to indicate whether projectiles should be dropped

    void Update()
    {
        if (isDropping)
        {
            // Drop projectiles at intervals while in range
            if (Time.time >= nextDropTime)
            {
                DropProjectile();
                nextDropTime = Time.time + dropInterval;
            }
        }
    }

    public void StartDropping()
    {
        isDropping = true;  // Start dropping projectiles
    }

    public void StopDropping()
    {
        isDropping = false;  // Stop dropping projectiles
    }

    void DropProjectile()
    {
        // Instantiate a projectile at the plane's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Apply a downward force to simulate gravity falling
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.down * 10f;  // Adjust speed as needed
        }
    }
}
