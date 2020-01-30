using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
	public float TiempoDeEsperaParaAtacar;
	Animator anim;
	Coroutine manager;
	public static bool cargado;
    
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }
    public void AuraStart()
    {
        manager = StartCoroutine(Manager());
        anim.Play("AuraIdle");
    }
    public void AuraStop()
    {
        StopCoroutine(manager);
        anim.Play("AuraIdle");
        cargado = false;
    }

    public IEnumerator Manager()
    {
        yield return new WaitForSeconds(TiempoDeEsperaParaAtacar);
        anim.Play("AuraPlay");
        cargado = true;
    }
    public bool EstaCargado()
    {
        return cargado;
    }

}
