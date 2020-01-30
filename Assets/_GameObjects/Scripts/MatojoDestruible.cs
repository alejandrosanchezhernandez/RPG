using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatojoDestruible : MonoBehaviour
{
    public GameObject objetoainstanciar;
    public GameObject animacionrestregon;
    public string destroystate;
    public float timefordisable;
    Animator animacion;

    private const string TAG_ESPADAZO = "espadazo";
    private const string TAG_JUGADOR = "Player";

    void Start()
    {
        animacion = GetComponent<Animator>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == TAG_JUGADOR)
        {
            Instantiate(animacionrestregon, transform.position, transform.rotation);
        }
        if (col.tag == TAG_ESPADAZO)
        {
            animacion.Play(destroystate);
            Instantiate(objetoainstanciar, transform.position, transform.rotation);
            yield return new WaitForSeconds(timefordisable);

            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
        }
        
    }
    void Update()
    {
        AnimatorStateInfo stateInfo = animacion.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(destroystate) && stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
        
    }
    
}
