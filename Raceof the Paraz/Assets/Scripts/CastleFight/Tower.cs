using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField]
    private GameObject arrow_prefab;
    int teamId;
    private List<GameObject> targetsInRange;
    int damage= 1;
    float attackSpeed = 20;
    bool hasTargetInRange = false;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        teamId = transform.GetComponentInParent<Castle>().castleId;
        targetsInRange = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit" && !(collision.isTrigger))
         if (collision.gameObject.GetComponentInParent<Castle>().castleId != teamId)
        {
            hasTargetInRange = true;
            targetsInRange.Add(collision.gameObject);
            if (targetsInRange.Count == 1)
            {
                fire();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Unit")
            if (collision.gameObject.GetComponentInParent<Castle>().castleId != teamId)
            {
                targetsInRange.Remove(collision.gameObject);
                if (targetsInRange.Count == 0)
                {
                    hasTargetInRange = false;
                }
            }
    }

    void fire()
    {
        if (!hasTargetInRange)
            return;
        GameObject arrow = Instantiate(arrow_prefab, transform.position, Quaternion.identity) as GameObject;
        arrow.GetComponent<Arrow>().setTarget(targetsInRange[0]);
        arrow.GetComponent<Arrow>().setDamage(damage);
        arrow.GetComponent<Arrow>().setEnabled();
        Invoke("fire", 1);
    }
}
