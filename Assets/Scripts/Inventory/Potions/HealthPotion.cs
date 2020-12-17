using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{

    //example
    
    public void init()
    {
        name = "Health Potion";
        id = 41;
        value = 10;
    }

    public override void onConsume()
    {
        //player.health += 30;
        //item.remove;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }



}
