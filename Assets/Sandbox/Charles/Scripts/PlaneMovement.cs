using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public Transform target;  // The target the plane will fly towards
    public float speed = 10f;  // Movement speed
    public float rotationSpeed = 5f;  // How fast the plane rotates towards the target
    public float attackRange = 10f;  // Distance at which the plane will start dropping projectiles

    public ProjectileDrop projectileDropScript;  // Reference to the ProjectileDrop script

    private Vector3 directionAfterTarget;  // Direction to keep flying after reaching the target
    private bool isFlyingPastTarget = false; // To track if the plane has passed the target

    void Update()
    {
        // Check the distance between the plane and the target
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // If the plane is within the attack range, enable projectile dropping
        if (distanceToTarget <= attackRange)
        {
            if (!projectileDropScript.isDropping)
            {
                projectileDropScript.StartDropping();  // Start dropping projectiles
            }
        }
        else
        {
            if (projectileDropScript.isDropping)
            {
                projectileDropScript.StopDropping();  // Stop dropping projectiles if the plane leaves the range
            }
        }

        // Plane movement logic: move towards the target first
        if (!isFlyingPastTarget)
        {
            // Set the plane's Y to its current Y value so it stays at the same height
            Vector3 targetPosition = target.position;
            targetPosition.y = transform.position.y;  // Keep the Y constant for the plane

            // Move the plane towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Rotate the plane to face the target (smooth rotation)
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);  // Create rotation towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // If we've reached the target, start flying in the direction it was moving
            if (transform.position == targetPosition)
            {
                // Calculate the direction the plane was moving towards when it reached the target
                directionAfterTarget = (target.position - transform.position).normalized;
                isFlyingPastTarget = true; // Start flying past the target
            }
        }
        else
        {
            // Continue moving in the direction the plane was traveling after reaching the target
            transform.position += directionAfterTarget * speed * Time.deltaTime;

            // Optionally, rotate the plane to keep facing the direction it's flying
            Quaternion targetRotation = Quaternion.LookRotation(directionAfterTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}