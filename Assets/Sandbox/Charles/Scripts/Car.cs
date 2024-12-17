using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Car : MonoBehaviour
{

    public NavMeshAgent agent;
    public float speed = 1;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.transform.position;
        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }
}
