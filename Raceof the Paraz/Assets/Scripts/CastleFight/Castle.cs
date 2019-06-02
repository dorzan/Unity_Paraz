using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, Unit
{

    public int castleId;
    public GameObject footmanPrefab;
    public int HP_MAX = 100;
    int HP;
    bool isDead = false;
    Color[] colors;
    public float spawnRate = 5;


    // Start is called before the first frame update
    void Start()
    {
        HP = HP_MAX;
        colors = new Color[4] { Color.yellow, Color.blue, Color.green, Color.red };
      
       // footman.GetComponent<SpriteRenderer>().color = colors[castleId];
        spawnFootman();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnFootman()
    {
        GameObject footman = Instantiate(footmanPrefab, transform.Find("SpawnPos").transform.position, Quaternion.identity) as GameObject;
        footman.GetComponent<SpriteRenderer>().color = colors[castleId];
        footman.transform.parent = this.transform;
        Invoke("spawnFootman", 100f / spawnRate);
    }

    public void takeDamage(int damage)
    {
        HP -= damage;
        transform.Find("Canvas").transform.Find("Simple Bar").transform.GetChild(0).GetComponent<SimpleHealthBar>().UpdateBar(HP, HP_MAX);
        if (HP < 1)
        {
            onPlayerLost();
        }
    }


    void onPlayerLost()
    {
        isDead = true;
        Debug.Log("player" + castleId + "has lost");
        CancelInvoke();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        GameObject.Find("ParazRoyalManager").GetComponent<ParazRoyalManager>().playerLost(castleId);
        Destroy(gameObject);


    }

    public bool IsDead()
    {
        return isDead;
    }

    public void onMidArrival() { }
}
