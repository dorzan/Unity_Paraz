using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0, 75, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (pos.y > 0)
        {
            pos.y = pos.y - 0.40f;
            transform.position = pos;
        }

    }
}
