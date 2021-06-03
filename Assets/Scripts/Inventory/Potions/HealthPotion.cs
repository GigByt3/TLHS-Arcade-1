using UnityEngine;

public class HealthPotion : Potion
{
    public HealthPotion() : base()
    {
        name = "Health Potion";
        value = 10;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCombatController>().health += 30;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
