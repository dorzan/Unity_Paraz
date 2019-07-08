using System.Collections;
using System.Collections.Generic;
using Toolbox;
using UnityEngine;
using UnityEngine.Tilemaps;


public class tile : MonoBehaviour
{

    public FourDirectionGraph map;
    // Start is called before the first frame update

    private void Awake()
    {
        map = new FourDirectionGraph(gameObject.GetComponent<Tilemap>());

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
