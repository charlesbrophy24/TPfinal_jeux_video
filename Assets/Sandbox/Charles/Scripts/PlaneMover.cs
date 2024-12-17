using UnityEngine;

public class PlaneMover : MonoBehaviour
{
    private Transform targetTower;
    public float speed = 5f;

    public void SetTarget(Transform target)
    {
        targetTower = target;
    }

    void Update()
    {
        if (targetTower != null)
        {
            // Move towards the target tower
            transform.position = Vector3.MoveTowards(transform.position, targetTower.position, speed * Time.deltaTime);

            // Look towards the target
            Vector3 direction = targetTower.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}