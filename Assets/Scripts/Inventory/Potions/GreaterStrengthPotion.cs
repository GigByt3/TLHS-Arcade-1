using UnityEngine;

public class GreaterStrengthPotion : Potion
{
    public GreaterStrengthPotion() : base()
    {
        name = "Greater Strength Potion";
        value = 22;
    }

    public override void onConsume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombatController>().damageModifier += 10;

        //if(player.health > 100) {
        //player.health = 100;
        //}

    }
}
