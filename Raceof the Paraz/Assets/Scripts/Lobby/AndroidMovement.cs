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
    //[SerializeField] private AudioClip[] taunts;
    public float speed;
    PlayerManager playermanager;
    bool jump = false;
    messageQueue queue;
    int id = 0;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public GameObject particals;
    bool flag = false;
    bool taunted = false;
    private Vector3 m_Velocity = Vector3.zero;



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
        else
        {
            return;
        }
        int i = 0;
        change = Vector3.zero;
        if (message == "A")
        {
            if (!taunted)
                PlayRandomTauntAndWait();
           return;
        }
        if (message == "B")
        {
            if(!taunted)
                PlayRandomTauntAndWait();
            if (!queue.Isempty())
                message = queue.Dequeue();
            else return;
        }

        while (message[i] != ',') { i++; }
        int power = Int32.Parse(message.Substring(0, i));
        int direction = Int32.Parse(message.Substring(i + 1, message.Length - i - 1));
        change.x = (float)Math.Cos(Math.PI * direction / 180.0) * power / 50;
        change.y = (float)Math.Sin(Math.PI * direction / 180.0) * power / 100;
        UpdateAnimationAndMove();

        //Debug.Log("client message received as: " + message);
        // Debug.Log("client message received as: " + clientMessage.Substring(0, i ));     


        //Sounds
       /* System.Random rnd = new System.Random();
        int month = rnd.Next(1, 800);
        if (month == 1)
            PlayRandomTaunt();*/
    }


    void FixedUpdate()
    {
        //Move Character
        // controller.Move(horizontalMove * Time.fixedDeltaTime * 10, false, jump);
       // Vector3 targetVelocity = new Vector2(change * 10);
        // And then smoothing it out and applying it to the character
        myRigidbody.velocity = Vector3.SmoothDamp(myRigidbody.velocity, change * 10,ref m_Velocity, .05f);
        //myRigidbody.AddForce(change);
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
        Debug.Log("trrigerd");
        animator.SetTrigger("teleport");
        enabled = false;
    }

    void OnCompleteTeleportAnimation()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(particals, transform.position, Quaternion.identity);
        AudioManager.Instace.playSFX("teleport");


    }

    void PlayRandomTaunt()
    {
        AudioManager.Instace.playRandomTaunt();
    }

    void PlayRandomTauntAndWait()
    {
        taunted = true;
        AudioManager.Instace.playRandomTaunt();
        Invoke("setTauntedFalse", 4);
    }

    void setTauntedFalse()
    {
        taunted = false;
    }
}

