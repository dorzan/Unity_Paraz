using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BlinkingText : MonoBehaviour
{
    TextMeshProUGUI text;
    bool flag = true;

    void Start() 
    {
        text = GetComponent <TextMeshProUGUI>();
        startBlinking();
    }

    // Update is called once per frame
    IEnumerator Blink()
    {
        while (true)
        { 
            switch (flag)
            {
                case true:
                    flag = false;
                    text.text = " ";
                    yield return new WaitForSeconds(0.5f);
                    break;
                case false:
                    flag = true;
                    text.text = "waiting  for  players ";
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void startBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }

    void stopBlinking()
    {
        StopCoroutine("Blink");
    }
}
