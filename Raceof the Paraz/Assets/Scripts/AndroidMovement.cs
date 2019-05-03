using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;



public class AndroidMovement : MonoBehaviour
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
    private Animator animator;



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
        animator = GetComponent<Animator>();
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
        change = Vector3.zero;
        while (message[i] != ',') { i++; }
        int power = Int32.Parse(message.Substring(0, i));
        int direction = Int32.Parse(message.Substring(i + 1, message.Length - i - 1));
        change.x = (float)Math.Cos(Math.PI * direction / 180.0) * power / 50;
        change.y = (float)Math.Sin(Math.PI * direction / 180.0) * power / 100;
        UpdateAnimationAndMove();

        Debug.Log("client message received as: " + message);
        // Debug.Log("client message received as: " + clientMessage.Substring(0, i ));     
    }


    void FixedUpdate()
    {
        //Move Character
        // controller.Move(horizontalMove * Time.fixedDeltaTime * 10, false, jump);
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
        // jump = false;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            animator.SetFloat("speedX", change.x);
            animator.SetFloat("speedY", change.y);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}

