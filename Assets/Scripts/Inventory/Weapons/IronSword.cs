using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSword : Weapon
{
    //example

    public void init()
    {
        name = "Iron Sword";
        id = 1;
        value = 2;

        damage = 2.0f;
        cooldown = 1.0f;

    }
}
