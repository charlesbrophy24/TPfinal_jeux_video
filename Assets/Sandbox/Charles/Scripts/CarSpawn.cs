using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using Unity.Mathematics;

public class CarSpawn : MonoBehaviour
{

    public float spawnTimer = 1;
    public GameObject prefabToSpawn;
    private float timer;

    public MRUKAnchor.SceneLabels spawnLabels;

    public float minEdgeDistance = 0.3f;

    public float normalOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
        return;

        timer += Time.deltaTime;
        if (timer > spawnTimer){

            SpawnGhosts();
            timer -= spawnTimer;

        }


    }


    public void SpawnGhosts(){

        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        
        room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP, minEdgeDistance, LabelFilter.Included(spawnLabels), out Vector3 pos, out Vector3 norm);

        Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
        randomPositionNormalOffset.y = 0;

        Instantiate(prefabToSpawn, randomPositionNormalOffset, Quaternion.identity);

    }
}
