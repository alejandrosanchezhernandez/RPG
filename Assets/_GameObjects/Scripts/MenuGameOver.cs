using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuGameOver : MonoBehaviour
{
    Canvas gameoverxd;



    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void VolverAJugar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Salon");
    }
}
