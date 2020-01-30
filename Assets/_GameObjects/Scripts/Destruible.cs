using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruible : MonoBehaviour
{
    public GameObject objetoainstanciar;
    public string destroystate;
    public float timefordisable;
    Animator animacion;
    public AudioSource jarron;
    // Start is called before the first frame update
    void Start()
    {
        animacion = GetComponent<Animator>();
    }
    
    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "espadazo")
        {
            jarron.Play();
            animacion.Play(destroystate);
            Instantiate(objetoainstanciar, transform.position, transform.rotation);
            yield return new WaitForSeconds(timefordisable);

            foreach(Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animacion.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.IsName(destroystate) && stateInfo.normalizedTime >= 1)
        {
            Destroy(gameObject);
        }
    }
    
}
