using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTowerASL : MonoBehaviour
{
    [SerializeField] private SpawnTower spawnTower;

    public void ClickButon(GameObject tower)
    {
        spawnTower.towerPrefab = tower;
    }


}
