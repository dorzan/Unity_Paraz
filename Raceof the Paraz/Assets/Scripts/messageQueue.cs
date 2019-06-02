using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messageQueue : MonoBehaviour
{
    Queue queue;
    string clientMessage;
    tcp tcp;
    // Start is called before the first frame update
    void Start()
    {
    }

    public messageQueue()
    {
        queue = new Queue();
    }

    // Update is called once per frame
    public void Insert(string message)
    {

        string messageTemp = message;
        int i = 0, j = 0;
        if (message != null)
        {
            while (i < messageTemp.Length)
            {
                
                if (message[i] == '$')
                {
                    queue.Enqueue(message.Substring(j, i - j));
                    if (i == message.Length)
                        break;
                    i++;
                    j = i;
                }
                i++;
            }
        }
    }

    public string Dequeue()
    {
        string str;
        str =(string) queue.Dequeue();
        return str;
    }

    public bool Isempty()
    {
        return (queue.Count == 0) ;
    }

}

