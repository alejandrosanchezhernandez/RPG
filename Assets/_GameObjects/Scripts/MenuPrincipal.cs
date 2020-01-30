using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuPrincipal : MonoBehaviour
{
    // Intento transicion fundido
    public bool start = false;
    public bool isfadein = false;
    public float alpha = 0;
    public float fadetime;

    // Volumen
    public Slider slidervolumen;
    public Slider sliderfx;

    // Luciernagas
    public ParticleSystem luciernagas;


    void CargarEstadoVolumen()
    {
        slidervolumen.value = PlayerPrefs.GetFloat("Volumen", 0.09f);
        sliderfx.value = PlayerPrefs.GetFloat("FxVolumen", 0.09f);
    }
    public void SalvarEstadoVolumen()
    {
        PlayerPrefs.SetFloat("Volumen", slidervolumen.value);
    }


    void Start()
    {
        Time.timeScale = 1;
        luciernagas.Play();
        CargarEstadoVolumen();
    }

    
    public void CargaNivel()
    {
        //SceneManager.LoadScene("Salon");
        //SalvarEstadoVolumen();
        StartCoroutine(Fundido());
        Debug.Log("Pasamos al primernivel");
        
    }
    public void Opciones()
    {
        SceneManager.LoadScene("MenuOpciones");
    }
    public void VolverAtras()
    {
        //SalvarEstadoVolumen();
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void Salir()
    {
        //SalvarEstadoVolumen();
        Application.Quit();
        Debug.Log("Adios, Esperamos que haya disfrutado de la aventura");
    }
    // Fundido de cambio de escena
    private void OnGUI()
    {
        if (!start)
            return;
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        if (isfadein)
        {
            alpha = Mathf.Lerp(alpha, 1.0f, fadetime * Time.deltaTime);
        }
        else
        {
            alpha = Mathf.Lerp(alpha, -0.1f, fadetime * Time.deltaTime);
            if (alpha < 0) start = false;
        }
    }
    void FadeIn()
    {
        start = true;
        isfadein = true;
    }
    void FadeOut()
    {
        isfadein = false;

    }
    private IEnumerator Fundido()
    {
        //SalvarEstadoVolumen();
        FadeIn();
        yield return new WaitForSeconds(fadetime);
        SceneManager.LoadScene("Salon");
        //FadeOut();
        //yield return new WaitForSeconds(fadetime);
        //SceneManager.LoadScene("Salon");

    }
}
