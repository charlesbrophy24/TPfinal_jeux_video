using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstantiateTowerPointCostASL : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private GameObject targetHand;
    [SerializeField] private PlayerInfoASL player;
    [SerializeField] private int pointCostNb;
    //[SerializeField] private int pointCostMin;
    



    public void HandleControllerActions(){
     
        if (player.PointsNb >= pointCostNb){
            Instantiate(tower, targetHand.transform.position, Quaternion.identity);
            player.PointsNb -= pointCostNb;
        }
        //_player.PointsNb;
    }
}
