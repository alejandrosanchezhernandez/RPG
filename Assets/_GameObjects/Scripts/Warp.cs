using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warp : MonoBehaviour
{
    public GameObject target;
    public GameObject mapaDestino;

    //TRANSICION DE ESCENA.

        //Esto dice si empieza o no la transicion.
    public bool start = false;
    public bool isfadein = false;
    public float alpha = 0;
    public float fadetime = 1f;
    GameObject area;
    public AudioSource puerta;


    private void Awake()
    {
        area = GameObject.FindGameObjectWithTag("Area");
    }
    public void Start()
    {
        
    }

    
    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        puerta.Play();
        //col.GetComponent<Animator>().enabled = false;
        FadeIn();

        col.GetComponent<Animator>().enabled = false;
        col.GetComponent<MovimientoPlayer>().enabled = false;

        yield return new WaitForSeconds(fadetime);

        FadeOut();
        
        col.GetComponent<Animator>().enabled = true;
        col.GetComponent<MovimientoPlayer>().enabled = true;

        if (col.tag == "Player")
        {
            
            col.transform.position = target.transform.position;
            
            //print("el jugador ha chocado");
        }
        //Camera.main.GetComponent<MainCamera>().SetBound(target);
        StartCoroutine(area.GetComponent<Area>().ShowArea(target.name));

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
            alpha = Mathf.Lerp(alpha, 1.1f, fadetime * Time.deltaTime);
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
    
}
