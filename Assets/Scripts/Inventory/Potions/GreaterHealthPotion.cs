using UnityEngine;

public class GreaterHealthPotion : Potion
{
    public GreaterHealthPotion() : base()
    {
        name = "Greater Health Potion";
        value = 15;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCombatController>().health += 50;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
