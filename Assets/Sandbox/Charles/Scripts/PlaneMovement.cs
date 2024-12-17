using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public GameObject towerPrefab;      // Prefab for the tower
    public GameObject targetPrefab;     // Prefab for the target
    public float speed = 10f;           // Movement speed
    public float rotationSpeed = 5f;    // Plane rotation speed
    public float attackRange = 10f;     // Range for dropping projectiles
    public float maxDistanceFromTower = 100f;  // Max distance plane can fly from the tower

    public ProjectileDrop projectileDropScript;  // Reference to ProjectileDrop script

    private Transform tower;            // Instantiated tower reference
    private Transform target;           // Instantiated target reference
    private Vector3 previousDirection;  // Store the last direction for smooth flight
    private bool isFlyingPastTarget = false;

    void Start()
    {
        // Instantiate tower and target prefabs at runtime
        if (towerPrefab != null && targetPrefab != null)
        {
            GameObject towerObject = Instantiate(towerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            tower = towerObject.transform;

            GameObject targetObject = Instantiate(targetPrefab, new Vector3(30, 0, 30), Quaternion.identity);
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Tower or Target Prefab is missing!");
        }
    }

    void Update()
    {
        if (tower == null || target == null) return;  // Ensure prefabs are instantiated

        // Distance check between plane and target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Handle projectile dropping
        if (distanceToTarget <= attackRange)
        {
            if (!projectileDropScript.isDropping)
            {
                projectileDropScript.StartDropping();
            }
        }
        else
        {
            if (projectileDropScript.isDropping)
            {
                projectileDropScript.StopDropping();
            }
        }

        // Movement logic
        if (!isFlyingPastTarget)
        {
            MoveTowardsTarget();
        }
        else
        {
            FlyPastTarget();
        }

        // Check for destruction when too far from the tower
        if (Vector3.Distance(transform.position, tower.position) > maxDistanceFromTower)
        {
            Debug.Log("Plane destroyed for exceeding max distance from tower");
            Destroy(gameObject);
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y; // Keep the plane at its current height

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Smoothly rotate the plane to face the target
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Save the direction for flying past the target
        previousDirection = direction;

        // Start flying past the target when close
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            isFlyingPastTarget = true;
        }
    }

    private void FlyPastTarget()
    {
        transform.position += previousDirection * speed * Time.deltaTime;

        // Keep rotating smoothly in the previous direction
        Quaternion targetRotation = Quaternion.LookRotation(previousDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
