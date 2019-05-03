using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    messageQueue queue;
    public GameObject paraz;

    // Start is called before the first frame update
    void Start()
    {
        queue = new messageQueue();

    }

    // Update is called once per frame
    void Update()
    {
        if (!queue.Isempty())
        {
            Debug.Log("SHITTTTTT");

            string message;
            message = queue.Dequeue();
            Debug.Log(message);
            if (message.Contains("new"))   //message is new#id
                NewParaz(Int32.Parse(message.Substring(3)));
           
        }
    }

    public messageQueue getPlayerManagerQueue()
    {
        return queue;
    }

    public void NewParaz(int id)
    {
        GameObject parazOb = Instantiate(paraz, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        parazOb.GetComponent<AndroidMovement>().setId(id);
        Debug.Log(id);
    }
}
