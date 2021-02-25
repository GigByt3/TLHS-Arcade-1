using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSword : Weapon
{
    public IronSword() : base()
    {
        name = "Iron Sword";
        value = 2;

        damage = 2.0f;
        cooldown = 1.0f;
    }
}
