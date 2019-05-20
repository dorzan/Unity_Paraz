using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textOpening : MonoBehaviour
{
    int i;
    [SerializeField] private AudioClip[] dorSpeech;
    [SerializeField] private AudioClip mu;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        Invoke("shit", 3);
        Invoke("shit", 4);
        Invoke("shit", 5);
        Invoke("muf", 6.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void shit()
    {  
        this.transform.GetChild(i).gameObject.SetActive(true);
        AudioManager.Instace.playSFX(dorSpeech[i]);
        i++;
    }

    void muf()
    {
        AudioManager.Instace.playSFX(mu);
    }

}
