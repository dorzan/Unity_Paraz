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
    public float speed;
    public GameObject particals;
    bool jump = false;
    messageQueue queue;
    PlayerManager playermanager;
    int id ;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    bool flag = false;



    void Awake()
    {
        playermanager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.Find("Server").GetComponent<tcp>().stream = shit;
        //queue = server.GetClientHandler(id).queue;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        myRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

    }

    public void setId(int id)
    {
        this.id = id;
        queue = playermanager.getPlayerUdpQueue(id);
        flag = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!flag)
            return;
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

        UpdateAnimationAndMove();

        //Sounds
        System.Random rnd = new System.Random();
        int month = rnd.Next(1, 800);
        if (month == 1)
            PlayRandomTaunt();
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

    public void onSceneChange()
    {
        animator.SetTrigger("teleport");
        enabled = false;
    }

    void OnCompleteTeleportAnimation()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(particals, transform.position, Quaternion.identity);
        AudioManager.Instace.playSFX("teleport");
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Llama")
        {
            change.x += (-change.x) * 25;
            change.y += (-change.y) * 25;
        }
        return;
    }

    void PlayRandomTaunt()
    {
        AudioManager.Instace.playRandomTaunt();
    }

    void playExpliosion()
    {
        AudioManager.Instace.playSFX("boom");
    }


}

























