using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenShield : Shield
{
    //example

    public override void init()
    {
        name = "Wooden Shield";
        id = 21;
        value = 2;

        defense = 10.0f;
    }
}
