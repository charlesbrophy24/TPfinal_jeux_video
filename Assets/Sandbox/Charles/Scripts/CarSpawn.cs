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

    public MRUKAnchor.SceneLabels spawnLabels; // Ensure this includes a label for "Table" surfaces.
    public float minEdgeDistance = 0.3f;
    public float normalOffset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!MRUK.Instance || !MRUK.Instance.IsInitialized)
            return;

        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            SpawnGhosts();
            timer -= spawnTimer;
        }
    }

    public void SpawnGhosts()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        // Attempt to find a random position specifically on table surfaces.
        if (room.GenerateRandomPositionOnSurface(
                MRUK.SurfaceType.FACING_UP, // Tables usually face up.
                minEdgeDistance, 
                LabelFilter.Included(spawnLabels), // Ensure your labels include "Table".
                out Vector3 pos, 
                out Vector3 norm))
        {
            // Adjust position to add offset based on the surface normal.
            Vector3 tableTopPosition = pos + norm * normalOffset;

            // Optional: Reset height if necessary to ensure alignment.
            // For example, if you have a flat table at a known height.
            tableTopPosition.y = Mathf.Max(tableTopPosition.y, 1.0f); // Replace `1.0f` with your table's height.

            Instantiate(prefabToSpawn, tableTopPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No suitable table surface found for spawning!");
        }
    }
}

