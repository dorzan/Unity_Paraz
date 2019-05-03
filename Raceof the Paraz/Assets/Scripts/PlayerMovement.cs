using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    messageQueue queue;
    Server server;
    static int id = 0;

    void Awake()
    {
        server = GameObject.Find("Server").GetComponent<Server>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.Find("Server").GetComponent<tcp>().stream = shit;
        queue = server.GetClientHandler(id).queue;
        id++;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
         horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
         if (Input.GetButtonDown("Jump"))
         {
             jump = true;
         }
         /*
        string message =null ;
        if (!queue.Isempty())
            message = tcp.queue.Dequeue();
        int length;
        char c;
        int i = 0;
        // Read incomming stream into byte arrary. 

        if (message != null)
        {
           // Debug.Log(queue);      
            // Debug.Log(clientMessage);
            if (message == "jump")
            {
                jump = true;
            }
            
            else
            {
                i = 0;
                while (message[i] != ',') { i++; }
                int power = Int32.Parse(message.Substring(0, i));
                int direction = Int32.Parse(message.Substring(i + 1, message.Length - i - 1));
                if (direction < 90 || direction > 270)
                    horizontalMove = power/2;
                else
                    horizontalMove = (-1) * power/2;
            }
            // Debug.Log("client message received as: " + clientMessage);
            // Debug.Log("client message received as: " + clientMessage.Substring(0, i ));
        }
       */
    }
    

    void FixedUpdate()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime * 10, false, jump);
        jump = false;
    }
}
