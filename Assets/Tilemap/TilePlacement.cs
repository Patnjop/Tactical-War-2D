using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilePlacement : MonoBehaviour
{

    Tilemap spawn;
    Grid gd;
    public Tile p1Spawn, p2Spawn, terrain, occupied;

    public bool cardInHand = false;

    public GameObject fill;

    void Start()
    {
 
        gd = FindObjectOfType<Grid>();
        spawn = FindObjectOfType<Tilemap>();
    }

    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int posInt = gd.LocalToCell(pos);

            if (spawn.HasTile(posInt))
            {              
                Debug.Log(posInt);
             
                Debug.Log(spawn.GetTile(posInt).name);

                if(spawn.GetTile(posInt).name == "P1SpawnTile")
                {
                    spawn.SetTile(posInt, occupied);
                    print("Spawned");
                }
            }
        }
    }
}
