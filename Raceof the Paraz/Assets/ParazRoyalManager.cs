using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParazRoyalManager : MonoBehaviour
{
    public int numOfPlayers = 4;
    public GameObject[] castles;
    public Vector2[] castles_positions;

    public delegate void callback(int id);
    public event callback OnPlayerdefeated;

    // Start is called before the first frame update
    void Start()
    {
        castles_positions = new Vector2[castles.Length];
        for(int i = 0; i < castles.Length; i++)
        {
            castles_positions[i] = castles[i].transform.position;
        }
          for(int i=0; i < castles.Length; i++)
        {
             Instantiate(castles[i]);
            if (!castles[i].activeSelf)
                castles[i] = null;


        }
    }


    public void playerLost(int id)
    {
        castles[id] = null;
        OnPlayerdefeated(id);
    }

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetKey("escape"))
             Application.Quit();    

    }
}
