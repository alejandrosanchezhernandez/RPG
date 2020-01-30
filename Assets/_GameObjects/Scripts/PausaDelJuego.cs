using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PausaDelJuego : MonoBehaviour
{
    Canvas canvaspausa;
    // Volumen
    public Slider volumenjuego;
    public Slider volumenFx;
    public AudioMixerSnapshot pause;
    public AudioMixerSnapshot unpause;
    

    void CargarVolumenInicial()
    {
        volumenjuego.value = PlayerPrefs.GetFloat("Volumen", 0f);
        volumenFx.value = PlayerPrefs.GetFloat("FxVolumen", 0f);
        
    }
    public void SalvarVolumenJuego()
    {
        PlayerPrefs.SetFloat("Volumen", volumenjuego.value);
        PlayerPrefs.SetFloat("FxVolumen", volumenFx.value);
        //PlayerPrefs.SetFloat("")
    }
    void Start()
    {
        canvaspausa = GetComponent<Canvas>();
        canvaspausa.enabled = false;
        CargarVolumenInicial();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            Pausa();
        }
        
        
    }
    public void Pausa()
    {
        
        canvaspausa.enabled = !canvaspausa.enabled;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        if (!canvaspausa.enabled)
        {
            SalvarVolumenJuego();
            unpause.TransitionTo(1f);
        }
        else
        {
            pause.TransitionTo(1f);
        }
    }
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Hasta pronto");
    }
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void ReanudarJuego()
    {
        canvaspausa.enabled = false;
        Time.timeScale = 1;
    }
    
}
