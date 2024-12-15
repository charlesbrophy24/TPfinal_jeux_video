using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Add this line

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInfoASL player;
    [SerializeField] private int LifeOnResset = 10; // Correct the typo from "Resset" to "Reset" if desired

    public void ChangementScenes(string nomScene)
    {
        SceneManager.LoadScene(nomScene);
    }

    public void Retour()
    {
        player.PointsNb = 0;
        player.LifesNb = LifeOnResset;
        ChangementScenes("Menu"); // Corrected this line to call the instance method
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}

