using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UImanager : MonoBehaviour
{
    GameObject textA;
    // Start is called before the first frame update
    void Start()
    {
        textA = GameObject.Find("text1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTextA(string text, bool blink, bool shutdown)
    {
        if (shutdown)
            textA.SetActive(false);
        textA.GetComponent<TextMeshProUGUI>().text = text;
        if (blink == true)
            textA.GetComponent<BlinkingText>().enabled = false;
        else
            textA.GetComponent<BlinkingText>().enabled = true;

    }
}
