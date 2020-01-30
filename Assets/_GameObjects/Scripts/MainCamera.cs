using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MainCamera : MonoBehaviour
{
    Transform targetPlayer;
    //private float xMax, xMIn, yMax, yMin;
    //[SerializeField]
    //private Tilemap tilemap;
    

    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
        //Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);
        //LimiteDeCamara(minTile, maxTile);
    }
    
    void Update()
    {
        transform.position = new Vector3(targetPlayer.position.x, targetPlayer.position.y, transform.position.z);
        //transform.position = new Vector3(Mathf.Clamp(targetPlayer.position.x, xMIn, xMax), Mathf.Clamp(targetPlayer.position.y, yMin, yMax), -20);



    }
    /*public void LimiteDeCamara(Vector3 mintile, Vector3 maxTile)
    {
        Camera cam = Camera.main;
        float height = 2F * cam.orthographicSize;
        float width = height * cam.aspect;

        xMIn = mintile.x + width / 2;
        xMax = maxTile.x - width / 2;
        yMin = mintile.y + height / 2;
        yMax = maxTile.y - height / 2;

    }*/
    
    
    
}
