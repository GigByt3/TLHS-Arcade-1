using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    public float damage;
    public float cooldown;


    public void superinit()
    {
        type = ItemType.COMBAT;
    }




}
