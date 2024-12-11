using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMenuASL : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public void ShowHideCanvas()
    {
        canvas.enabled = !canvas.enabled;
    }
}
