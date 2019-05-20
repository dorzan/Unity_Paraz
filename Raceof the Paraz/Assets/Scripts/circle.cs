using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle : MonoBehaviour
{

    int count = 0;
    bool flag = false;
    SceneManage sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("GameManager").GetComponent<SceneManage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("paraz"))
            return;
        count++;
        if (count == GameObject.Find("GameManager").GetComponent<PlayerManager>().getNumOfPlayers())
        {
            sceneManager.ChangeScene("Race");
            flag = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("paraz"))
            return;
        if (flag)
        {
            sceneManager.cancleNextScene();
            flag = false;
        }
        count--;
    }
}
