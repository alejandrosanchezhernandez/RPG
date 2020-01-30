using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    //public Canvas pausa;
    [Tooltip("Vida total del Jugador")]
	public int vidamaxima;
    [Tooltip("Vida del Jugador despues de recibir un ataque enemigo")]
	public int vidaactual;
    //public GameObject barravida;

    public Canvas gameover;
    public bool reiniciar;
    public bool fin;

    public AudioSource musica;
    public AudioSource findepartida;
    public Rigidbody2D cuerpojugador;

    private Text vida;
    public Text vidarestante;
    public Text vidatotal;
    public int danyoesqueleto;

    public ParticleSystem venenoparticulas;
    public float contadorDeTiempo;
    public float minimoContador;
    public bool inmortal;
    public bool envenenado;
    private const string TAG_INMUNE = "inmune";

    

    

    void Start()
    {
        envenenado = false;
        inmortal = false;
        //velocidadmov = GetComponent<MovimientoPlayer>();
        //venenoparticulas.Stop();
        vida = GameObject.Find("Vida").GetComponent<Text>();
        reiniciar = false;
        fin = false;
        gameover.gameObject.SetActive(false);
        //pausa = GetComponent<Canvas>();
        vidaactual = vidamaxima;
        ParticulasVenenoOff();
        //venenoparticulas.Stop();
    }
    public void Update()
    {
        //Contador();
        vida.text = "HP: " + vidarestante + vidaactual;
    }
    public void Atacado()
    {
        if (--vidaactual <= 0)
        {
            fin = true;
            Time.timeScale = 0;
            gameover.gameObject.SetActive(true);
            musica.Pause();
            findepartida.Play();
            //cuerpojugador.constraints = RigidbodyConstraints2D.FreezeAll;
            //SpriteRenderer.
            //Destroy(gameObject);
        }
        
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "corazon")
        {
            vidaactual = vidamaxima;
        }
        else if(collision.tag == TAG_INMUNE)
        {
            //velocidadmov.speed = 10;
            StopCoroutine("Veneno");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "esqueleto" && !inmortal)
        {
            Atacado();
        }
        else if(collision.gameObject.tag == "esqueletovenenoso" && !inmortal)
        {
            
            Debug.Log("Hay");
            contadorDeTiempo = 0;
            StartCoroutine("Veneno");

        }
        if (collision.gameObject.tag == TAG_INMUNE)
        {
            Debug.Log("Hola?");
            StopCoroutine("Veneno");
        }

    }
    public void ParticulasVenenoOff()
    {
        venenoparticulas.Stop();
    }
    public void ParticulasVenenoOn()
    {
        venenoparticulas.Play();
    }
    public void Contador()
    {
        contadorDeTiempo += Time.deltaTime;

        if (contadorDeTiempo >= minimoContador)
        {
            contadorDeTiempo = 0;
        }
    }
    public IEnumerator Veneno()
    {
        envenenado = true;
        ParticulasVenenoOn();
        Debug.Log("Estoy Envenenado");
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Atacado();
        yield return new WaitForSeconds(1f);
        Debug.Log("Ya no estoy envenenado");
        Atacado();
        envenenado = false;
        ParticulasVenenoOff();
        
        
    }
    
    
}
