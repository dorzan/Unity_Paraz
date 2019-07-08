using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;
using UnityEngine.Tilemaps;

public class FootMan  :  Unit
{
    //constants

    public int speed = 5;
    int damage = 15;
    bool isFighting = false;
    bool isTouchedMiddle = false;
    bool isTriggered = false;
    private Animator animator;
    private Rigidbody2D myRigidbody;
    Vector2 direction;
    Vector2 destination;
    Vector2 midPos;
    ParazRoyalManager manager;
    private List<GameObject> currentEnemeys;
    private List<GameObject> engagedEnemeys;
    int numOfTriggers = 0;
    float attackSpeed = 20;




    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentEnemeys = new List<GameObject>();
        engagedEnemeys = new List<GameObject>();
        manager = GameObject.Find("ParazRoyalManager").GetComponent<ParazRoyalManager>();
        myRigidbody = GetComponent<Rigidbody2D>();
        midPos = GameObject.Find("Middle").transform.position;
        direction = new Vector2(0, 0);
        changeDirectionTowards(midPos);
        teamId = transform.GetComponentInParent<Castle>().teamId;

        //delegates
        manager.OnPlayerdefeated += onPlayerDefeted;



    }

    // Update is called once per frame
    void Update()
    {

    }



    void FixedUpdate()
    {
        if (isFighting)
        {
                return;
        }

        LinePath linePath = AStar.FindLinePathClosest(GameObject.Find("PathTilemap").GetComponent<Tilemap>(), transform.position, destination);
        if (linePath != null)
        {
            if (linePath.nodes.Length > 1)
            {
                //linePath.Draw();
                direction = linePath.nodes[1];
            }
        }
        else return;

        if (!isTriggered)
            transform.position = Vector3.MoveTowards(transform.position, direction, 0.2f);

        // myRigidbody.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);

        if (isTriggered)
        {
            changeDirectionTowards(currentEnemeys[0].transform.position);
            transform.position = Vector3.MoveTowards(transform.position, direction, 0.2f);

            // myRigidbody.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
        }
    }


    void changeDirectionTowards(Vector2 dest)
    {
        /*
        destination = dest;
        direction = (dest - (Vector2)transform.position);
        direction.Normalize();
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
        */
        destination = dest;
        //LinePath linePath = AStar.FindLinePathClosest(GameObject.Find("PathTilemap").GetComponent<Tilemap>(), transform.position, dest);
/*
        if (linePath != null)
        {
            linePath.Draw();
            //transform.position = Vector3.MoveTowards(transform.position, linePath.nodes[1], 0.2f);
            //direction = Vector3.MoveTowards(transform.position, linePath.nodes[1], 0.2f);
            destination = linePath.nodes[1];
        }*/
    }




    Vector2 closestCastlePos()
    {
        float min = 6000f;
        float dist;
        Vector2 pos = new Vector2(0, 0);
        for (int i = 0; i < manager.numOfPlayers; i++)
        {
            if (teamId == i)
                continue;
            //Unit castle = manager.castles[i].GetComponent<Castle>();
            if (manager.castles[i] == null)
            {
                continue;
            }
            dist = Vector2.Distance(transform.position, manager.castles[i].transform.position);
            if (dist < min)
            {
                pos = manager.castles[i].transform.position;
                min = dist;
            }
        }
        return pos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit" && !collision.isTrigger)
            if(collision.gameObject.GetComponentInParent<Castle>().teamId != teamId)
            {
                isTriggered = true;
                currentEnemeys.Add(collision.gameObject);
            }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit" && !collision.isTrigger)
            if (collision.gameObject.GetComponentInParent<Castle>().teamId != teamId)
            {
                currentEnemeys.Remove(collision.gameObject);

                if (currentEnemeys.Count == 0)
                {
                    changeDirectionTowards(closestCastlePos());
                    isTriggered = false;
                }
            }
  
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Unit")
            if (collision.gameObject.GetComponentInParent<Castle>().teamId != teamId)
            {
                engagedEnemeys.Add(collision.gameObject);

                if (engagedEnemeys.Count == 1)
                    fight(collision.gameObject);
                return;
            }


        if (collision.gameObject.tag == "Castle")
                if(collision.gameObject.GetComponent<Castle>().teamId != teamId)
                {
                    engagedEnemeys.Add(collision.gameObject);
                        fight(collision.gameObject);
                }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Unit")
            if (collision.gameObject.GetComponentInParent<Castle>().teamId != teamId)
            {
                engagedEnemeys.Remove(collision.gameObject);
                if (engagedEnemeys.Count == 0)
                {
                    animator.SetTrigger("isWalking");
                    isFighting = false;
                }
                return;
            }
        if (collision.gameObject.tag == "Castle")
            if (collision.gameObject.GetComponent<Castle>().teamId != teamId)
            {
                engagedEnemeys.Remove(collision.gameObject);
                if (engagedEnemeys.Count == 0)
                {
                    animator.SetTrigger("isWalking");
                    isFighting = false;
                }
            }
    }


    void fight(GameObject enemy)
    {
        isFighting = true;
        strike();
    }

    void strike()
    {
        animator.ResetTrigger("Strike");
        if (!isFighting)
        {
            return;
        }
        animator.SetTrigger("Strike");

        Invoke("strike", 10f / attackSpeed);
        
    }


    void onDie()
    {
        AudioManager.Instace.playSFX("footmanDeath");
        this.enabled = false;
        isDead = true;
        isFighting = false;
        animator.SetTrigger("Death");
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        Invoke("disable", 10f);
        //   Invoke((MethodInvoker)(() => { this.Text = "Hi"; }));
    }

    void disable()
    {
        Destroy(gameObject);
    }

    public void doDamage()
    {
        if (!isFighting)
        {
            return;
        }
        Unit opponant = engagedEnemeys[0].GetComponent<ScriptHolder>().getUnitScript() as Unit;
        if (opponant.IsDead())
        {
            if (engagedEnemeys.Count == 0)
            {
                isFighting = false;
                animator.SetTrigger("isWalking");
            }
            if (currentEnemeys.Count == 0)
            {
                isTriggered = false;
            }
            return;
        }
        opponant.takeDamage(damage);
    }


    //******* Unit abstract Override implementations*******///
    //*** 
    //*******

    public override void takeDamage(int damage)
    {
        HP -= damage;
        transform.Find("Canvas").transform.Find("Simple Bar").transform.GetChild(0).GetComponent<SimpleHealthBar>().UpdateBar(HP, HP_MAX);
        if (HP < 1)
        {
            onDie();
        }
    }

    public override void onMidArrival()
    {
        Debug.Log("dfgdf");
        isTouchedMiddle = true;
        if (!isFighting && !isTriggered)
            changeDirectionTowards(closestCastlePos());
    }

    public override bool IsDead()
    {
        return isDead;
    }

    public override void onPlayerDefeted(int playerId)
    {
        try
        {
            if (destination == manager.castles_positions[playerId])
                changeDirectionTowards(closestCastlePos());
        }
        catch(MissingReferenceException e) { return; }
            
          // if(currentEnemeys.Contains(manager.castles[id]))
    }



}

