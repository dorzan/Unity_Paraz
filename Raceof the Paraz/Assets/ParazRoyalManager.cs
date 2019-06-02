using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParazRoyalManager : MonoBehaviour
{
    public int numOfPlayers = 4;
    public GameObject[] castles;

    public delegate void callback(int id);
    public event callback OnPlayerdefeated;

    // Start is called before the first frame update
    void Start()
    {

          for(int i=0; i < castles.Length; i++)
        {
            Instantiate(castles[i]);
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
