using UnityEngine;

public class PlaneBehavior : MonoBehaviour
{
    [HideInInspector] public Transform targetTower; // Set by the spawner
    [HideInInspector] public float despawnRadius;   // Set by the spawner
    public float speed = 10f;                       // Plane movement speed
    public float elevationOffset = 5f;              // Elevation above the target tower
    public float dropRange = 20f;                   // Range at which the plane drops a projectile
    public GameObject projectilePrefab;             // Projectile prefab to drop

    public ProjectileDrop projectileDropScript;     // Reference to the ProjectileDrop script

    private Vector3 direction;
    private bool hasDroppedProjectile = false;      // Flag to ensure we only drop once

    void Start()
    {
        if (targetTower != null)
        {
            // Calculate direction towards the tower with elevation
            Vector3 elevatedTarget = targetTower.position + Vector3.up * elevationOffset;
            direction = (elevatedTarget - transform.position).normalized;

            // Orient the plane to face the tower
            transform.LookAt(elevatedTarget);
        }
        else
        {
            Debug.LogError("Target Tower not set for PlaneBehavior.");
        }

        // Ensure ProjectileDrop script is attached and set up
        if (projectileDropScript == null)
        {
            Debug.LogError("ProjectileDrop script not assigned in PlaneBehavior.");
        }
    }

    void Update()
    {
        // Move the plane forward in its current direction
        transform.position += direction * speed * Time.deltaTime;

        // Check for projectile drop if within range
        if (!hasDroppedProjectile && Vector3.Distance(transform.position, targetTower.position) <= dropRange)
        {
            StartProjectileDrop();  // Call method to start projectile drop
            hasDroppedProjectile = true; // Ensure it only drops once
        }

        // Despawn if outside the radius
        if (Vector3.Distance(transform.position, targetTower.position) > despawnRadius)
        {
            Destroy(gameObject);
        }
    }

    void StartProjectileDrop()
    {
        // Set up and trigger the projectile drop from the ProjectileDrop script
        if (projectileDropScript != null)
        {
            projectileDropScript.StartDropping();  // Start dropping projectiles
        }
    }
}

