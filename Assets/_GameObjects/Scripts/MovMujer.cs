using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovMujer : MonoBehaviour
{
    //Iniciacion de dialogo.
    public bool dialogoActivo;
    public string[] inicioDialogo;
    public string[] inicioMision;
    public string[] despedida;
    public string nombre;
    public string[] lore;
    
    public Canvas dialogo;
    public Text conversacion;
    Coroutine auxcorutine;
    //Variables para gestinar rango de vion de ataque y velocidad.
    public float radioVision;
    public float radioAtaque;
    public float velocidad;
    //Variable para guardar al jugador.
    GameObject player;
    //Variable para guardar la posicion inicial.
    Vector3 posicionInicial;
    //Variable para guardar Anamiator y RigidBody2D con la rotacion congelada en Z.
    Animator animaciones;
    Rigidbody2D rb2d;
    //Sistema de dialogo.
    /*[SerializeField]
    public string nombre;
    [SerializeField]
    public string[] listaDeOraciones;*/
    
    
    

    
    void Start()
    {
        //Recuperamos texto de la conversacion.
        //conversacion = GetComponent<Text>();
        //Al iniciar busco un GameObject con el tag Player:
        player = GameObject.FindGameObjectWithTag("Player");
        //Guardo mi posicion inicial.
        posicionInicial = transform.position;
        //Recupero los componentes Animator y RigidBody2D.
        animaciones = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        //Por defecto el objetivo sera su posicion inicial
        Vector3 objetivo = posicionInicial;
        //Lanzo un Raycast al juegador.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, radioVision, 1 << LayerMask.NameToLayer("Default"));

        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        if (hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                objetivo = player.transform.position;
            }
        }

        float distancia = Vector3.Distance(objetivo, transform.position);
        Vector3 direccion = (objetivo - transform.position).normalized;

        if(objetivo != posicionInicial && distancia < radioAtaque)
        {
            
            animaciones.SetFloat("movX", direccion.x);
            animaciones.SetFloat("movY", direccion.y);
            animaciones.Play("IdleStandUp");
            StartCoroutine("Frases");

            //dialogo.enabled = true;
            AbrirDialogo();
            
            //StartCoroutine(Frases(valor));
            
            
        }
        if(objetivo != posicionInicial && distancia > radioAtaque)
        {
            CerrarDialogo();
            //StopCoroutine(Frases());
        }
        
    }
    public void Historia()
    {
        foreach(string frase in lore)
        {
            conversacion.text = "Hola" + lore;
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);
        Gizmos.DrawWireSphere(transform.position, radioAtaque);
    }

    /*public IEnumerator Frases(int valor, float time = 0.1f)
    {

        //conversacion.text = nombre + "Hola, hoy despertaste mas tarde de lo habitual";
        dialogo.enabled = true;
        string[] orden;

        if(valor == 0)
        {
            orden = inicioDialogo;
        }
        else if(valor == 1)
        {
            orden = inicioMision;
        }
        else
        {
            orden = despedida;
        }
        int total = orden.Length;
        string reset = "";
        dialogoActivo = true;
        //Debug.Log("Hola, hoy despertaste mas tarde de lo habitual");
        //yield return new WaitForSeconds(4f);
        //Debug.Log("Esta mañana perdi las llaves en el prado donde pastan las vacas, ve a buscarlas profavor");
        //yield return new WaitForSeconds(4f);
        //Debug.Log("Estoy desesperada");
        yield return null;
        for(int i = 0; i < total; i++)
        {
            reset = "";

            if (dialogoActivo)
            {
                for (int s = 0; s < orden[i].Length; s++)
                {
                    if (dialogoActivo)
                    {
                        reset = string.Concat(reset, orden[i][s]);
                        conversacion.text = reset;
                        yield return new WaitForSeconds(time);
                    }
                    else yield break;
                }
                yield return new WaitForSeconds(0.4f);
            }
            else yield break;
        }
        yield return new WaitForSeconds(0.4f);
        Debug.Log("Cerrar Dialogo");
        
    }*/
    IEnumerator EsperaSolapacionDialogo()
    {
        yield return new WaitForEndOfFrame();
    }
    public void CerrarDialogo()
    {
        dialogoActivo = false;
        dialogo.enabled = false;
        animaciones.Play("MujerIdleDown");
    }
    public void AbrirDialogo()
    {
        dialogoActivo = true;
        dialogo.enabled = true;
        conversacion.text = nombre + inicioDialogo[0];
        Debug.Log("Hola");

        /*if (dialogoActivo)
        {
            CerrarDialogo();
            StartCoroutine("EsperaSolapacionDialogo");
        }
        else
        {
            dialogoActivo = false;
        }*/
        
    }
    
}
