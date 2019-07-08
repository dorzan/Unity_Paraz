using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class footshit2 : MonoBehaviour
{
    public Tile tile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int vec = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        Vector3Int vec2 = new Vector3Int();
        for (int i = -2; i < 2; i++)
        {

            for (int j = -2; j < 2; j++)
            {
                vec2.Set(vec.x + i, vec.y + j, vec.z);
                GameObject.Find("Tilemap2").GetComponent<Tilemap>().SetTile(vec2, tile);
            }
        }
    }
}
