using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour
{
    public AudioSource sonidovida;
    
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            sonidovida.Play();
            Destroy(gameObject);
        }
    }


}
