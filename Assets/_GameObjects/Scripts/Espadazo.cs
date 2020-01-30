using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espadazo : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D c1)
    {
        if (c1.tag == "Enemy")
        {
            c1.SendMessage("SiendoAtacado");
            Debug.Log("Socorro");
            

        }
        if(c1.tag == "esqueleto")
        {
            c1.SendMessage("Inmolacion");
        }
    }
}
