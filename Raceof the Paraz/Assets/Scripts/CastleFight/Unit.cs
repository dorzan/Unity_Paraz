using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour 
{
    public int HP_MAX;
    public int HP;
    public bool isDead = false;
    public int teamId;

    void Start()
    {

        

    }


    public abstract void onPlayerDefeted(int playerId);
    public abstract void takeDamage(int damage); //takes damage and returns true if unit hp is under 1
    public abstract void onMidArrival();
    public abstract bool IsDead();

}


