using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shield : Item
{
    public float defense;

    public Shield()
    {
        type = ItemType.SHIELD;
    }
}
