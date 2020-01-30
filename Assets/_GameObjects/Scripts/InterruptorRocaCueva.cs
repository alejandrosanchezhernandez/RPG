using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterruptorRocaCueva : MonoBehaviour
{
    public Transform inicio;
    public Transform destino;
    public float velocidad;
    public float tiempo = 0;
    public Vector3 cuevaCerrada;
    public Vector3 cuevaAbierta;
    public GameObject jugador;
    public Animator animacionpiedra;
    public AudioSource rocaCueva;
    
    
    public GameObject piedra;
    public bool cerrado;
    public bool abierto;



    void Start()
    {
        //animacionpiedra = GetComponent<Animator>();
        cerrado = false;
        abierto = true;

    }

    void Update()
    {

        tiempo += Time.deltaTime * velocidad;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && abierto)
        {
            CerrarCueva();
        }
        


    }
    
    public void CerrarCueva()
    {
        //tiempo += Time.deltaTime * velocidad;
        abierto = false;
        cerrado = true;
        rocaCueva.Play();
        animacionpiedra.Play("RocaCerrarCueva");
        
        //cuevaCerrada = Vector3.Lerp(inicio.position, destino.position, tiempo);
        //piedra.transform.position = cuevaCerrada;
    }
    public void AbrirCueva()
    {
        abierto = true;
        cerrado = false;
        animacionpiedra.Play("RocaAbrirCueva");
        //cuevaAbierta = Vector3.Lerp(destino.position, inicio.position, tiempo);
        //piedra.transform.position = cuevaAbierta;
    }
}
