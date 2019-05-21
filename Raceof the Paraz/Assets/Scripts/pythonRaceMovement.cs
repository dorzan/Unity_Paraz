using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pythonRaceMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float speed;
    public GameObject particals;
    bool jump = false;
    messageQueue queue;
    PlayerManager playermanager;
    int id;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Vector3 tizinabe;
    private Animator animator;
    float horizontalMove = 0f;
    tcp server;
    SpriteRenderer spriteRenderer;
    bool flag = false;




    void Awake()
    {
        server = GameObject.Find("Server").GetComponent<tcp>();
        controller = GetComponent<CharacterController2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.Find("Server").GetComponent<tcp>().stream = shit;
        //queue = server.GetClientHandler(id).queue;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        queue = server.queue;
        tizinabe.x = 400;
        tizinabe.y = 400;
        // myRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

    }

    public void setId(int id)
    {
        this.id = id;
        queue = playermanager.getPlayerTcpQueue(id);
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
        if (message == "up")
        {
            jump = true;
        }
        if (message == "left")
        {
            horizontalMove = -1;

        }
        if (message == "right")
        {
            horizontalMove = 1;   
        }
        if (message == "down")
        {
            horizontalMove = 0;
        }
        UpdateAnimationAndMove();
    }


    void FixedUpdate()
    {
        //Move Character
        // controller.Move(horizontalMove * Time.fixedDeltaTime * 10, false, jump);
        controller.Move(horizontalMove * 150 * Time.deltaTime, false, jump);
      //  horizontalMove = 0;
        jump = false;
        // jump = false;
    }

    void UpdateAnimationAndMove()
    {       
            animator.SetFloat("speedX", horizontalMove);
            animator.SetBool("isMoving", true);
        
      //  else
       // {
        //    animator.SetBool("isMoving", false);
       // }
    }

    public void onSceneChange()
    {
        animator.SetTrigger("teleport");
        enabled = false;
    }

    void onDeath()
    {
        Instantiate(particals, transform.position, Quaternion.identity);
        transform.position = tizinabe;
        AudioManager.Instace.playSFX("teleport", 0.2f);
        Invoke("Spawn", 2f);
    }



    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            onDeath();
        }
        if (collision.gameObject.tag == "finish")
        {
            GameObject.Find("raceManager").GetComponent<RaceManager>().onWin(gameObject);
        }
    }
    
    void Spawn()
    {
        transform.position = GameObject.Find("SpawnPos").transform.position;
        myRigidbody.velocity = new Vector2(0, 0);
        Instantiate(particals, transform.position, Quaternion.identity);
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
