using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
	[Tooltip("esperar X segundos antes de destruir el tajo")]
	public float waitBeForeDestroy;

	[HideInInspector]
	public Vector2 mov;

	public float speed;


    
    void Update()
    {
        transform.position += new Vector3(mov.x, mov.y, 0) * speed * Time.deltaTime;
        
    }
    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "objeto")
        {
            yield return new WaitForSeconds(waitBeForeDestroy);
            Destroy(gameObject);
        }
        if(col.tag == "Pared")
        {
            Destroy(gameObject);
        }
        else if(col.tag != "Player" && col.tag != "Attacking")
        {
            Destroy(gameObject);

            if (col.tag == "Enemy")
            {
                col.SendMessage("SiendoAtacado");
                Debug.Log("Socorro");
                Destroy(gameObject);
            }
            
        }
        Destroy(gameObject, 2f);
    }
}
