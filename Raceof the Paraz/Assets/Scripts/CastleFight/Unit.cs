using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Unit
{
    void takeDamage(int damage); //takes damage and returns true if unit hp is under 1
    void onMidArrival();
    bool IsDead();

}


