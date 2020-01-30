using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmunidadPlayer : MonoBehaviour
{
    public ParticleSystem inmunidad;
    public ParticleSystem venenoOff;
    private const string TAG_INMUNE = "inmune";
    public float tiempoInmunidad = 20;

    private VidaPlayer vidaOff;

    public MovimientoPlayer velocidadmov;



    void Start()
    {
        velocidadmov = GetComponent<MovimientoPlayer>();
        vidaOff = GetComponent<VidaPlayer>();
        OffInmunidad();
    }
    public void OnTriggerEnter2D(Collider2D c1)
    {
        if(c1.tag == "inmune")
        {
            Debug.Log("Aki Que pasa");
            StartCoroutine(EstadoInmune());
        }
    }
    public void OnInmunidad()
    {
        inmunidad.Play();

    }
    public void OffInmunidad()
    {
        inmunidad.Stop();
    }
    public void SoyInmune()
    {
        vidaOff.enabled = false;
    }
    public void YaNoSoyInmune()
    {
        vidaOff.enabled = true;
    }
    public IEnumerator EstadoInmune()
    {
        velocidadmov.speed = 0.4f;
        vidaOff.envenenado = false;
        vidaOff.inmortal = true;
        SoyInmune();
        venenoOff.Stop();
        OnInmunidad();
        yield return new WaitForSeconds(tiempoInmunidad);
        YaNoSoyInmune();
        OffInmunidad();
        vidaOff.inmortal = false;
        velocidadmov.speed = 0.2f;
    }
    
    
}
