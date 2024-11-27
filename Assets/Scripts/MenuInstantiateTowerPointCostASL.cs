using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstantiateTowerPointCostASL : MonoBehaviour
{
    [SerializeField] public GameObject tower;
    [SerializeField] public GameObject targetHand;
    [SerializeField] public PlayerInfoASL _player;


    public void HandleControllerActions(){
        Instantiate(tower, targetHand.transform.position, Quaternion.identity);
        //_player.PointsNb;
        _player.PointsNb - 5;

    }
           
  

}
