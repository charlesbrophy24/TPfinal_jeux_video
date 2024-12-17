using UnityEngine;

public class ProjectileDrop : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile prefab
    public float dropInterval = 1f;  // Time between each drop
    private float nextDropTime;

    public bool isDropping = false;  // Flag to indicate whether projectiles should be dropped
    public Transform planeTransform; // Reference to the plane's transform for projectile positioning

    void Update()
    {
        if (isDropping)
        {
            // Drop projectiles at intervals
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
        if (projectilePrefab != null && planeTransform != null)
        {
            // Instantiate a projectile at the plane's position, adjusted slightly for an offset
            Vector3 dropPosition = planeTransform.position - planeTransform.forward * 2f; // Adjust the position in front of the plane
            Instantiate(projectilePrefab, dropPosition, Quaternion.identity);
            Debug.Log("Projectile Dropped!");
        }
        else
        {
            Debug.LogError("Projectile Prefab or Plane Transform not assigned in ProjectileDrop.");
        }
    }
}
