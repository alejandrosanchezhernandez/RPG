using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundidoInicial : MonoBehaviour
{
    // Intento transicion fundido
    public bool start = false;
    public bool isfadein = false;
    public float alpha = 0;
    public float fadetime;




    void Start()
    {
        print("si llego");
        StartCoroutine(Fundido());
        //Invoke("Fundido", 0);
    }
    private void Update()
    {
        //StartCoroutine(Fundido());
    }
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
        FadeOut();
        yield return new WaitForSeconds(fadetime);
        print("de aqui no paso");
        //SceneManager.LoadScene("Salon");
        //FadeOut();
        //yield return new WaitForSeconds(fadetime);
        //SceneManager.LoadScene("Salon");

    }
    /*public void Fundido()
    {
        FadeOut();
    }*/

    
}
