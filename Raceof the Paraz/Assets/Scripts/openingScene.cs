using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class openingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("change", 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void change()
    {
        SceneManager.LoadScene("Lobby");
    }
}
