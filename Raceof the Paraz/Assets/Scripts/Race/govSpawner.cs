using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class govSpawner : MonoBehaviour
{
    public int id;
    public GameObject gildaWheel;
    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        spawnwheel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnwheel()
    {
       
        System.Random rnd = new System.Random();
        // int temp1 = rnd.Next(50,60);
        int temp1 = -50;
        int temp2 = rnd.Next(-10,10);
        if (id == 2)
        {
            temp1 = -temp1;
            temp2 = -temp2;
        }
            gildaWheel = Instantiate(gildaWheel, transform.position, Quaternion.identity);
        //gildaWheel.GetComponent<Rigidbody2D>().AddForce(new Vector2(temp1, temp2));
        gildaWheel.GetComponent<Rigidbody2D>().velocity = (new Vector2(temp1, -temp2));


        if (active)
        {
            Invoke("spawnwheel", 2f);
        }
    }

    
}
