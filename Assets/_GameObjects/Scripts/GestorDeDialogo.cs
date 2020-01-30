using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestorDeDialogo : MonoBehaviour
{
    //Dialogo y oraciones del dialogo.
	public Dialogo dialogo;
    Queue<string> oraciones;
    //Panel de dialogo y texto.
    public GameObject panelDeDialogo;
    public Text conversacion;
    //Orden de la conversacion.
    string oracionActual;
    public float velocidadEscritura;
    //Sonido de la conversacion.
    AudioSource miAudio;
    public AudioClip sonidoVoz;
    


    void Start()
    {
        oraciones = new Queue<string>();
        miAudio = GetComponent<AudioSource>();
    }
    void ComienzoDialogo()
    {
        oraciones.Clear();

        foreach (string oracion in dialogo.listaDeOraciones)
        {
            oraciones.Enqueue(oracion);
        }

        SiguienteOracionEnPantalla();
    }
    public void SiguienteOracionEnPantalla()
    {
        if(oraciones.Count <= 0)
        {
            conversacion.text = oracionActual;
            return;
        }

        oracionActual = oraciones.Dequeue();
        Debug.Log(oracionActual);
    }

    void Update()
    {
        
    }
}
