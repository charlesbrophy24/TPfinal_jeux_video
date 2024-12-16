using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private PlayerInfoASL player;
    


    //partit pour les vagues

    private int _currentWave = 1;
    private int _waveEnemyTotalCount;
    private int _enemiesRemaining;
    private int _waveEnemyMultiplier = 10;




    void Start()
    {
        CalculateEnemyWaveCount();
        _enemiesRemaining = _waveEnemyTotalCount;
        StartCoroutine(StartWave());
    }

    void CalculateEnemyWaveCount()
    {
        _waveEnemyTotalCount = _currentWave * _waveEnemyMultiplier;
    }

    IEnumerator StartWave()
    {
        _uiManager.UpdateWave(_currentWave, _waveEnemyTotalCount);
        yield return new WaitForSeconds(2); // Show wave text for 2 seconds
        _uiManager.HideWaveText();
        _spawnManager.StartSpawning();
    }

    public void UpdateEnemyCount()
    {
        _enemiesRemaining--;
        _uiManager.UpdateEnemyCount(_enemiesRemaining);

        if (_enemiesRemaining == 0)
        {
            StartCoroutine(WaveCleared());
        }
    }

    IEnumerator WaveCleared()
    {
        _uiManager.ShowWaveClearedText();
        yield return new WaitForSeconds(2); // Show wave cleared text for 2 seconds
        _currentWave++;
        CalculateEnemyWaveCount();
        _enemiesRemaining = _waveEnemyTotalCount;
        _uiManager.HideWaveClearedText();
        StartCoroutine(StartWave());
    }

    public int GetWaveEnemyTotalCount()
    {
        return _waveEnemyTotalCount;
    }

    //FIN pour les vagues
}
