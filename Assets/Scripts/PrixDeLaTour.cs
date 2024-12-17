using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to use TMP_Text

public class PrixDeLaTour : MonoBehaviour
{
    //[SerializeField] private GameObject tower;
    //[SerializeField] private GameObject targetHand;
    //[SerializeField] 
    [SerializeField] private PlayerInfoASL player;
    [SerializeField] private int pointCostNb;
    //[SerializeField] private TMP_Text text;
    [SerializeField] private float activeDuration = 5f;
    void Awake()
    {
        if (player.PointsNb >= pointCostNb)
        {
           
            player.PointsNb -= pointCostNb;
            Debug.Log("Tour instanciée avec succès. Points restants : " + player.PointsNb);
        }
        else if (player.PointsNb < pointCostNb)
        {
            StartCoroutine(EnableTagObjectsForSeconds(activeDuration)); // Start the coroutine
            Destroy(gameObject);
            
        }
    }

    private IEnumerator EnableTagObjectsForSeconds(float seconds)
    {
        // Find all objects with the "NotEnoughPoints" tag
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NotEnoughPoints");

        // Enable them
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(seconds);

        // Disable them
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }



    //private IEnumerator ShowTextForSeconds(float seconds)
    //{
    //    GameObject[] objects = GameObject.FindGameObjectsWithTag("NotEnoughPoints");
    //
    //    // Enable them
    //    foreach (GameObject obj in objects)
    //    {
    //        obj.SetActive(true);
    //    }
    //
    //    // Wait for the specified duration
    //    yield return new WaitForSeconds(activeDuration);
//
 //       // Disable them
 //       foreach (GameObject obj in objects)
 //       {
 //           obj.SetActive(false);
 //       }
  //  }
}

