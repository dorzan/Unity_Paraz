using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    // Start is called before the first frame update
    private int sortingOrderBase = 5000;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    bool runOnlyOnce = true;
    Renderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y - offset);
        if (runOnlyOnce)
            Destroy(this);
    }
}
