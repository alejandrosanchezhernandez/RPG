using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piedra : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento en unidades del mundo")]
    public float velocidad;

    Rigidbody2D rb2d;
    GameObject player;
    Vector3 objetivo, direccion;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();

        if(player != null)
        {
            objetivo = player.transform.position;
            direccion = (objetivo - transform.position).normalized;
        }
        
    }
    private void FixedUpdate()
    {
        if (objetivo != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + (direccion * velocidad) * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
        if(collision.transform.tag == "Pared")
        {
            Destroy(gameObject);
        }
        if(collision.transform.tag == "espadazo")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Player")
        {
            collision.SendMessage("Atacado");
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
}
