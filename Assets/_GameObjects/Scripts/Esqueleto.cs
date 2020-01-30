using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Esqueleto : MonoBehaviour
{
    //Variables Para gestionar el rango de vision.
    public float radioVision;
    public float radioAtaque;
    public float velocidad;
    //Variable para guardar al jugador.
    GameObject player;
    //Variable para guardar la posicion inicial.
    Vector3 posicionInicial;
    //Variable para guardar Animador y rigidbody2d con la rotacion congelada en z.
    Animator animaciones;
    Rigidbody2D rb2d;
    //Variable que guarda animacion de muerte.
    //public GameObject muerteEsqueleto;
    public bool muerte;
    public AudioSource explosion;
    public GameObject fogonazo;


    private void Start()
    {
        //Al iniciar no esta muerto.
        muerte = false;
        //Recupero al jugador con el tag.
        player = GameObject.FindGameObjectWithTag("Player");
        //Guardo la posicion incial.
        posicionInicial = transform.position;
        //Recuperamos los componentes Animator y Rigidbody2D.
        animaciones = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        //Por defecto el objetivo del enemigo sera su posicion incial.
        Vector3 objectivo = posicionInicial;

        //Compruebo un Raycast Del enemigo al jugador.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, radioVision, 1 << LayerMask.NameToLayer("Default"));
        //Pongo el enemigo en una layer deistinta a defaul para evitar el raycast
        //Tambien poner al objeto attack y al prefab slash una layer attack
        //sino los detecta como entorno.

        //Dibujo un rayo desde la posicion del enemigo hasta la posicion del jugador de color rojo.
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        //Si el Raycast encuentra al jugador lo pongo de objetivo.
        if (hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                objectivo = player.transform.position;
            }
        }

        //Calculo la distancia y la direccion al objetivo actual.
        float distancia = Vector3.Distance(objectivo, transform.position);
        Vector3 direccion = (objectivo - transform.position).normalized;

        //Si el player esta en rango de ataque nos detenemos y le atacamos.
        if(objectivo != posicionInicial && distancia < radioAtaque)
        {
            animaciones.SetFloat("movX", direccion.x);
            animaciones.SetFloat("movY", direccion.y);
            animaciones.Play("EnemigoCaminar", -1, 0);//Congela la animacion de andar.
            
            
        }
        else
        {
            rb2d.MovePosition(transform.position + direccion * velocidad * Time.deltaTime);
            //Establezco la animacion al moverme.
            animaciones.speed = 1;
            animaciones.SetFloat("movX", direccion.x);
            animaciones.SetFloat("movY", direccion.y);
            animaciones.SetBool("Walking", true);
        }
        if(objectivo == posicionInicial && distancia < 0.02f)
        {
            transform.position = posicionInicial;
            animaciones.SetBool("Walking", false);
        }
        
    }
    // Aqui Dibujamos 2circulos sobre el enemigo uno par el rango de vision que dicen desde donde nos persigue y otro mas pequeño que el el rango de ataque.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioVision);
        Gizmos.DrawWireSphere(transform.position, radioAtaque);

    }
    public void Inmolacion()
    {
        //Instantiate(muerteEsqueleto, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            explosion.Play();
            muerte = true;
            //Instantiate(muerteEsqueleto, transform.position, transform.rotation);
            Instantiate(fogonazo, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "espadazo")
        {
            muerte = true;
            Destroy(gameObject);

        }
        if(collision.tag == "Player")
        {
            collision.SendMessage("Atacado");
        }
    }

}
