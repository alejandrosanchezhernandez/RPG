using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasDialogos : MonoBehaviour
{
    Canvas canvasDialogos;

    void Start()
    {
        canvasDialogos = GetComponent<Canvas>();
        canvasDialogos.enabled = false;
    }

    void Update()
    {
        
    }
}
