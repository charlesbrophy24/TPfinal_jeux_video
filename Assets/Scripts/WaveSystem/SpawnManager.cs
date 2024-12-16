using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float enemySpawnTime = 2f;

    private WaveManager _gameManager;
    private bool _canSpawn = true;
    private int _waveEnemyTotalCount;
    private int _enemiesSpawned = 0;

    void Start()
    {
        _gameManager = FindObjectOfType<WaveManager>();
    }

    public void StartSpawning()
    {
        _canSpawn = true;
        _waveEnemyTotalCount = _gameManager.GetWaveEnemyTotalCount();
        _enemiesSpawned = 0;
        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawning()
    {
        _canSpawn = false;
    }

    IEnumerator SpawnEnemy()
    {
        while (_canSpawn)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            _enemiesSpawned++;
            yield return new WaitForSeconds(enemySpawnTime);

            if (_enemiesSpawned >= _waveEnemyTotalCount)
            {
                StopSpawning();
            }
        }

        if (_canSpawn)
        {
            enemySpawnTime = Mathf.Max(1f, enemySpawnTime - 0.1f);
        }
    }
}
