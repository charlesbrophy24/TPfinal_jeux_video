using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;  // Reference to the tower prefab

    void Start()
    {
        // Spawn the tower at the center of the map (0, 0, 0)
        Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
    }
}
