using System.Collections;
using System.Collections.Generic;
using Toolbox;
using UnityEngine;
using UnityEngine.Tilemaps;

public class footshit : MonoBehaviour
{
    // Start is called before the first frame update
    LinePath linePath;
    public Tile tile;
    public Sprite sprite;
    void Start()
    {

      //  for (int i =0; i < linePath.nodes.Length; i++)
      //  {
          //  Debug.Log(linePath.nodes[i]);
      //  }
      
      //  Debug.DrawLine(new Vector3 (1,1,2), new Vector3(40, 40,2), Color.cyan, 120f, false);
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3Int vec = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        Vector3Int vec2 = new Vector3Int();

        GameObject.Find("Tilemap2").GetComponent<Tilemap>().RefreshAllTiles();
       // linePath = AStar.FindLinePathClosest(GameObject.Find("Tilemap2").GetComponent<Tilemap>(), transform.position + new Vector3(3,3,0), GameObject.Find("fuck").transform.position);

        linePath = AStar.FindLinePathClosest(GameObject.Find("Tilemap2").GetComponent<Tilemap>(), transform.position , GameObject.Find("fuck").transform.position);

        if (linePath != null)
        {
            linePath.Draw();
            transform.position = Vector3.MoveTowards(transform.position, linePath.nodes[1], 0.2f);

        }
        else
            StartCoroutine(Example());
        

    }


    IEnumerator Example()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
