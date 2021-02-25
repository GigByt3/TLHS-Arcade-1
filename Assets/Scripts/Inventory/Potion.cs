using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : Item
{
    public Potion()
    {
        type = ItemType.POTION;
    }

    public abstract void onConsume();


}
