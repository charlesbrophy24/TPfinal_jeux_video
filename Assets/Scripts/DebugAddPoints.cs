using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAddPoints : MonoBehaviour
{
    [SerializeField] PlayerInfoASL player;
    [SerializeField] private int pointCostNb;

    public void AddPoints()
    {
    
        player.PointsNb += pointCostNb;

    }


}
