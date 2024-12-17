using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerInfoASL player;
    [SerializeField] private GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (player.LifesNb <= 0)
        {
            gameManager.ChangementScenes("MenuDeFin");
        }
    }
}
