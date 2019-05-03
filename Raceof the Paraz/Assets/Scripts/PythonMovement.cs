using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class PythonMovement : MonoBehaviour
{
    public CharacterController2D controller;
    bool jump = false;
    messageQueue queue;
    Server server;
    int id = 0;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    public float speed;
    bool flag;


    void Awake()
    {
        server = GameObject.Find("Server").GetComponent<Server>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.Find("Server").GetComponent<tcp>().stream = shit;
        //queue = server.GetClientHandler(id).queue;
        myRigidbody = GetComponent<Rigidbody2D>();
        flag = false;

    }

    public void setId(int id)
    {
        this.id = id;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == false)
        {
            try
            {
                queue = server.GetClientHandler(id).queue;
                flag = true;
            }
            catch (NullReferenceException shit)
            {
                return;
            }
        }
        string message = null;
        if (!queue.Isempty())
            message = queue.Dequeue();
        else return;
        int i = 0;
        //int power = Int32.Parse(message);
        if (message == "up")
        {
            change.y = 1;
            change.x = 0;
        }
        if (message == "down")
        {
            change.y = -1;
            change.x = 0;
        }
        if (message == "right")
        {
            change.y = 0;
            change.x = 1;
        }
        if (message == "left")
        {
            change.y = 0;
            change.x = -1;
        }

        Debug.Log("client message received as: " + message);
        // Debug.Log("client message received as: " + clientMessage.Substring(0, i ));     
    }


    void FixedUpdate()
    {
        //Move Character
        // controller.Move(horizontalMove * Time.fixedDeltaTime * 10, false, jump);
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
        change = Vector3.zero;

        // jump = false;
    }
}