using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstantiateTowerASL : MonoBehaviour
{
    [SerializeField] public GameObject tower;
    [SerializeField] public GameObject targetHand;

    private void HandleControllerActions()
    {
        Instantiate(tower, targetHand.transform.position, Quaternion.identity);
    }
           
  

}
