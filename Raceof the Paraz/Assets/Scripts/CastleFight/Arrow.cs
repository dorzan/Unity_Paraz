using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    GameObject target;
    float speed = 15;
    Vector2 direction;
    private Rigidbody2D myRigidbody;
    int damage;
    Unit targetAsUnit;
    // Start is called before the first frame update
    void Start()
    {
        //this.enabled = false;
        direction = new Vector2(1, 1);
        myRigidbody = GetComponent<Rigidbody2D>();
        targetAsUnit = target.GetComponent<ScriptHolder>().getUnitScript() as Unit;
        //gameObject.transform.Rotate(0, 0, 90);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        try
         {
            if (targetAsUnit.IsDead())
                Destroy(gameObject);
            gameObject.transform.rotation = Quaternion.Euler(0, 0,-90 + Mathf.Rad2Deg * (Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x)  )) ;
            direction = target.transform.position - transform.position;
            direction.Normalize();
            myRigidbody.MovePosition((Vector2)transform.position + direction * speed * Time.deltaTime);
        }
        catch (MissingReferenceException e) { Destroy(gameObject); }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target && !collision.isTrigger)
        {
            targetAsUnit.takeDamage(damage);
            Destroy(gameObject);

        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
        
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void setEnabled()
    {
        //this.enabled = true;
    }
}
