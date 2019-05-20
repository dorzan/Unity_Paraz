using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMovement : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    private Scene scene;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        changeDirection();

    }

    void OnLevelWasLoaded(int level)
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Race")
        {
            Destroy(this);
        }

    
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        myRigidbody.MovePosition(transform.position + change * 13 * Time.deltaTime);
    }

    void changeDirection()
    {
        System.Random rnd = new System.Random();
        int direction = rnd.Next(5);
        switch (direction)
        {
            case 0:
                change.Set(1, 0, 0);
                animator.Play("up");
                break;
            case 1:
                change.Set(-1, 0, 0);
                animator.Play("down");
                break;
            case 2:
                change.Set(0, 1, 0);
                animator.Play("right");
                break;
            case 3:
                change.Set(0, -1, 0);
                animator.Play("left");
                break;
            case 4:
                change.Set(0, 0, 0);
                animator.Play("idle");
                break;
        }
        int randTime = rnd.Next(6);
        Invoke("changeDirection", (float)randTime);
    }

}
