using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILifePointsASL : MonoBehaviour
{
    // le scripatble
    // texte nb de points
    // texte nb de vie
    [SerializeField] private PlayerInfoASL player;
    [SerializeField] private TMP_Text Nblifetxt;
    [SerializeField] private TMP_Text NbPointstxt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       NbPointstxt.text = player.PointsNb.ToString();

       Nblifetxt.text = player.LifesNb.ToString();
    }
}
