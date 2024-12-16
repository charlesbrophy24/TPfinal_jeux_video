using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text waveClearedText;
    [SerializeField] private TMP_Text waveCountText;
    [SerializeField] private TMP_Text enemyCountText;

    public void UpdateWave(int currentWave, int waveEnemyTotalCount)
    {
        waveText.text = "Wave " + currentWave;
        waveCountText.text = "Wave: " + currentWave;
        enemyCountText.text = "Enemies: " + waveEnemyTotalCount;
        waveText.gameObject.SetActive(true);
    }

    public void HideWaveText()
    {
        waveText.gameObject.SetActive(false);
    }

    public void ShowWaveClearedText()
    {
        waveClearedText.gameObject.SetActive(true);
    }

    public void HideWaveClearedText()
    {
        waveClearedText.gameObject.SetActive(false);
    }

    public void UpdateEnemyCount(int enemiesRemaining)
    {
        enemyCountText.text = "Enemies: " + enemiesRemaining;
    }
}

