using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : Item
{

    public void superinit()
    {
        type = ItemType.POTION;
    }

    public abstract void onConsume();


}
