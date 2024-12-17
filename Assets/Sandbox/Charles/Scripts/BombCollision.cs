using UnityEngine;

public class BombCollision : MonoBehaviour
{
    public GameObject newObjectPrefab; // Reference to the object to instantiate
    public float destroyDelay = 0f; // Optional delay before destruction (if needed)

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bomb collides with something (you can add specific conditions if needed)
        if (gameObject.CompareTag(null))
        {
            // Instantiate the new object at the bomb's position and rotation
            Instantiate(newObjectPrefab, gameObject.transform.position, gameObject.transform.rotation);

            // Optionally, destroy the bomb with a delay
            Destroy(gameObject, destroyDelay);
        }
        else if (gameObject.CompareTag("Tour")){

            Instantiate(newObjectPrefab, gameObject.transform.position, gameObject.transform.rotation);

        }
    }
}
