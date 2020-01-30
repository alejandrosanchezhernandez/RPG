using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorDeNivelesPlay : MonoBehaviour
{
    
    private Animator transicion;



    void Start()
    {
        transicion = GetComponent<Animator>();
    }

    
    
    public void CargaDeNiveles(string nombrenivel)
	{
        //transicion.SetTrigger("salida");
        //SceneManager.LoadScene("Salon");
        StartCoroutine(CambioDeTransicion());
    }
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Gracias, Vuelva Pronto");
    }
    IEnumerator CambioDeTransicion()
    {
        transicion.SetTrigger("salida");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Salon");
    }
}
