using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvionSpawn : MonoBehaviour
{
    // Start is called before the first frame update


    

    [SerializeField] private GameObject prefab;

    [SerializeField] private Vector3 ZoneSize;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject instantiated = Instantiate(prefab);

        instantiated.transform.position = new Vector3(
            
            Random.Range(transform.position.x - ZoneSize.x / 2, transform.position.x + ZoneSize.x /2),
            Random.Range(transform.position.y - ZoneSize.y / 2, transform.position.y + ZoneSize.y /2),
            Random.Range(transform.position.x - ZoneSize.x / 2, transform.position.x + ZoneSize.x /2)
        );

        

    }

    private void OnDrawGizmos() {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, ZoneSize);

    }

    

}
