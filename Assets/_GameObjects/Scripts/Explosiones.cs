using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosiones : MonoBehaviour
{
  
    private void Update()
    {
        Destruccion();
    }
    public void Destruccion()
    {
        Destroy(gameObject, 1.2f);
    }


}
