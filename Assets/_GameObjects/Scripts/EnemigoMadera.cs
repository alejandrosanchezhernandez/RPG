using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoMadera : MonoBehaviour
{
    [Tooltip("Rango de persecucion del enemigo: Si sales de su rango de vision vuelve a su punto de origen")]
    public float visionRadio;
    [Tooltip("Rango de ataque del enemigo: Se detiene y te lanza tarugos de madera")]
    public float ataqueRadio;
    [Tooltip("Velocidad de persecucion: 0.1 para que el player pueda huir")]
    public float velocidadDeMovimiento;
    [Tooltip("El tarugo que lanza para atacarte")]
    public GameObject piedra;
    [Tooltip("Tiempo entre ataques")]
    public float velocidadDeAtaque = 2f;
    public bool atacar;
    [Tooltip("Vida total del enemigo")]
    public int vidamaxima;
    public int vidaactual;
    private Text vida;
    //public Text vidatotal;
    //public Text vidarestante;

    GameObject player;
    Vector3 posicionInicial;
    Animator animaciones;
    Rigidbody2D rb2d;
    Vector3 target;
    [Tooltip("Animacion de muerte del enemigo")]
    public GameObject arbolcreciendoprefab;
    [Tooltip("Sonido de muerte")]
    public AudioSource estrellitas;

    //[Tooltip("Cuando el enemigo ve al player aparecera su vida en pantalla")]
    //public Canvas canvasvidaenemigo;


    private void Awake()
    {
        //canvasvidaenemigo = GetComponent<Canvas>();
        //canvasvidaenemigo.enabled = false;
    }
    void Start()
    {
        //canvasvidaenemigo = GetComponent<Canvas>();
        //canvasvidaenemigo.enabled = false;
        //vida = GameObject.Find("VidaEnemigo").GetComponent<Text>();
        vida = GetComponentInChildren<Text>();
        vidaactual = vidamaxima;
        player = GameObject.FindGameObjectWithTag("Player");
        posicionInicial = transform.position;
        animaciones = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        vida.text = vidaactual + " / " + vidamaxima;
        target = posicionInicial;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, visionRadio, 1 << LayerMask.NameToLayer("Default"));
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        if(hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                //canvasvidaenemigo.enabled = true;
                target = player.transform.position;
            }
        }
        float distancia = Vector3.Distance(target, transform.position);
        Vector3 direccion = (target - transform.position).normalized;

        if (target != posicionInicial && distancia > ataqueRadio)
        {
            //canvasvidaenemigo.enabled = true;
            Debug.DrawRay(transform.position, forward, Color.green);
            animaciones.SetBool("Walking", true);
            
            
        }

        if (target != posicionInicial && distancia < ataqueRadio)
        {
            //canvasvidaenemigo.enabled = true;
            animaciones.SetFloat("movX", direccion.x);
            animaciones.SetFloat("movY", direccion.y);
            animaciones.Play("EnemigoCaminar", -1, 0);

            if (!atacar)
            {
                //canvasvidaenemigo.enabled = true;
                StartCoroutine(LanzarPiedra(velocidadDeAtaque));
            }
            //InvokeRepeating("LanzarPiedra", 0, 2);
        }
        
        else
        {
            //canvasvidaenemigo.enabled = true;
            rb2d.MovePosition(transform.position + direccion * velocidadDeMovimiento * Time.deltaTime);
            animaciones.speed = 1;
            animaciones.SetFloat("movX", direccion.x);
            animaciones.SetFloat("movY", direccion.y);
        }
        if (target == posicionInicial && distancia < 0.02f)
        {
            //canvasvidaenemigo.enabled = true;
            transform.position = posicionInicial;
            animaciones.SetBool("Walking", false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadio);
        Gizmos.DrawWireSphere(transform.position, ataqueRadio);
    }
    IEnumerator LanzarPiedra(float segundos)
    {
        //canvasvidaenemigo.enabled = true;
        atacar = true;
        //yield return new WaitForSeconds(0.2f);
        //canvasvidaenemigo.enabled = true;
        
        if (target != posicionInicial && piedra != null)
        {
            
            Instantiate(piedra, transform.position, transform.rotation);
            yield return new WaitForSeconds(segundos);
        }
        atacar = false;
        
    }
    public void SiendoAtacado()
    {
        if (--vidaactual <= 0)
        {
            //canvasvidaenemigo.enabled = true;
            estrellitas.Play();
            Instantiate(arbolcreciendoprefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    /*private void OnGUI()
    {
        Vector2 posicion = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(new Rect(posicion.x - 20, Screen.height - posicion.y + 60, 40, 24), vidaactual + "/" + vidamaxima);
    }*/
}
