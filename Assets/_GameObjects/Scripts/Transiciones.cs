using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transiciones : MonoBehaviour
{
    public Animator transicion;

    void Start()
    {
        transicion = GetComponent<Animator>();
        
    }

    void Update()
    {
        
    }
    public void CargarNivel()
    {
        SceneManager.LoadScene("Salon");
        
    }
}
